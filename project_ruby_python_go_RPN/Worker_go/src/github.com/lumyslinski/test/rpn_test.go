package test

import (
	"testing"
	"fmt"
	"github.com/lumyslinski/app"
)

func TestReversePolishNotationNumbers(t *testing.T) {
	var input1 = "5 1 2 + 4 * + 3 -"
	var input2 = "3 4 + 6 0 /"
	var input3 = "5332493250324332523242324332513245"
	c := make(chan app.RpnResultDto)
	go app.ReversePolishNotation(input1,false,c)
	r1 := <-c
	CheckResult("14.000",r1,t)
	go app.ReversePolishNotation(input2,false,c)
	r2 := <-c
	CheckResult(app.ErrorDividingZero,r2,t)
	go app.ReversePolishNotation(input3,true,c)
	r3 := <-c
	CheckResult("14.000",r3,t)
}

func CheckResult(expectedValue string, r app.RpnResultDto, t *testing.T) {
	if r.ResultValue != expectedValue {
		t.Error(fmt.Sprintf( "Value [%s] is wrong! Expected value is [%s]",r.ResultValue,expectedValue))
	} else {
		t.Logf(fmt.Sprintf( "Value [%s] is correct!",r.ResultValue))
	}
}