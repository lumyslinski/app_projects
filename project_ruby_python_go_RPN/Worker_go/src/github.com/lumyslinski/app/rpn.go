package app

import (
	"strings"
	"fmt"
	"unicode"
	"strconv"
	"math"
	"time"
	)

type Stack struct{ data []float64 }

func (s Stack)  Empty() bool     { return len(s.data) == 0 }
func (s Stack)  Peek()  float64  { return s.data[len(s.data)-1] }
func (s Stack)  Len()   int      { return len(s.data) }
func (s *Stack) Put(i   float64) { s.data = append(s.data, i) }
func (s *Stack) Pop()   float64  {
	d := s.data[len(s.data)-1]
	s.data = s.data[:len(s.data)-1]
	return d
}


type RpnResultDto struct {
	ResultValue string
	ResultTime  string
}

const ErrorConvertingOperand = "Error while converting number from ascii code"
const ErrorDividingZero 	 = "Can not divide by 0"

func ReversePolishNotation(inputData string, isInputConvertedToASCII bool, c chan RpnResultDto) {
	start := time.Now()
	var splitted []string
	if isInputConvertedToASCII {
		splitted = strings.Split(inputData,"32")
	} else {
		splitted = strings.Split(inputData," ")
	}
	var stackOperand Stack
	var errorMessage = ""
	for _,splittedElement := range splitted {
		var characterCode  = '!'
		var characterValue int
		if isInputConvertedToASCII {
			characterValue, _ = strconv.Atoi(splittedElement)
			characterCode     = rune(characterValue)
		} else {
			characterCode     = ([]rune(splittedElement))[0]
			characterValue    = int(characterCode)
		}
		if characterCode == '!' {
			continue
		}
		if unicode.IsDigit(characterCode) {
			var valueFromAscii int
			var valueFromAsciiError error
			if isInputConvertedToASCII {
				valueFromAscii, valueFromAsciiError = strconv.Atoi(string(characterCode))
			} else {
				valueFromAscii, valueFromAsciiError = strconv.Atoi(splittedElement)
			}
			if valueFromAsciiError == nil {
				stackOperand.Put(float64(valueFromAscii))
			} else {
				errorMessage = fmt.Sprintf("%s, error: %s [char:%s]",ErrorConvertingOperand,valueFromAsciiError,splittedElement)
			}
		} else {
			var operator = characterValue
			var operand  = 0.0
			var operand2 = stackOperand.Pop()
			var operand1 = stackOperand.Pop()
			switch operator {
				case 42: operand = operand1 * operand2
				case 43: operand = operand1 + operand2
				case 45: operand = operand1 - operand2
				case 94: operand = math.Pow(operand1,operand2)
				case 47,92: if operand2 != 0 { operand = operand1 / operand2 } else { errorMessage = ErrorDividingZero }
			}
			stackOperand.Put(operand)
		}
	}
	var elapsedSeconds = time.Since(start).Seconds()
	var elapsedTime    = fmt.Sprintf("%.6f",elapsedSeconds)
	var result           RpnResultDto
	if len(errorMessage) > 0 {
		result = RpnResultDto{ResultValue:errorMessage, ResultTime:elapsedTime}
	} else {
		result = RpnResultDto{ResultValue:fmt.Sprintf("%.3f",stackOperand.Pop()), ResultTime:elapsedTime}
	}
	c <- result
}