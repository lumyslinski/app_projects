package test

import (
	"testing"
	"strconv"
	"fmt"
	"os"
	"io"
	"log"
)

func TestHash(t *testing.T) {
	var input = "5 1 2 + 4 * + 3 -"
	var expected = "5332493250324332523242324332513245"
	var hashResult = ""
	for _,v := range input {
		var asciiDecimalCode = int(v)
		hashResult += strconv.Itoa(asciiDecimalCode)
	}
	if hashResult != expected {
		t.Error(fmt.Sprintf( "Value [%s] is wrong! Expected value is [%s]",hashResult,expected))
	} else {
		t.Logf(fmt.Sprintf( "Value [%s] is correct!",hashResult))
	}
}

func TestWriteCacheHash(t *testing.T) {
	dir, err := os.Getwd()
	if err != nil {
		log.Fatal(err)
	}
	fmt.Println(dir)

	file, err := os.Create(dir+"/../../../../../../CacheResults/5332493250324332523242324332513245.test")
	if err != nil {
		fmt.Println(err)
		os.Exit(1)
	}
	defer file.Close()
	if _, err := io.WriteString(file, "14"); err != nil {
		t.Error(fmt.Sprintf( "Can not save a file: [%s]",err))
	} else {
		t.Logf(fmt.Sprintf( "File is saved"))
	}
}
