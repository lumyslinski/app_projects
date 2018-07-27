package test

import (
	"testing"
	"github.com/lumyslinski/app"
	"fmt"
)

func TestReversePolishNotationNumbers(t *testing.T) {
	var input1 = "5 1 2 + 4 * + 3 -"
	var input2 = "3 4 + 6 0 /"
	c := make(chan app.RpnResultDto)
	go app.ReversePolishNotation(input1,c)
	go app.ReversePolishNotation(input2,c)
	r2, r1 := <-c, <-c
	CheckResult("14.000",r1,t)
	CheckResult(app.ErrorDividingZero,r2,t)
}



func CheckResult(expectedValue string, r app.RpnResultDto, t *testing.T) {
	if r.ResultValue != expectedValue {
		t.Error(fmt.Sprintf( "Value [%s] is wrong! Expected value is [%s]",r.ResultValue,expectedValue))
	} else {
		t.Logf(fmt.Sprintf( "Value [%s] is correct!",r.ResultValue))
	}
}