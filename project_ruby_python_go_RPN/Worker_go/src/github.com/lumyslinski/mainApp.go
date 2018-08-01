// Example channel-based high-level Apache Kafka consumer
package main

/**
 * Copyright 2016 Confluent Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

import (
	"fmt"
	"github.com/confluentinc/confluent-kafka-go/kafka"
	"os"
	"os/signal"
	"syscall"
	"github.com/lumyslinski/app"
	"log"
	"io"
)


func main() {
	sigchan := make(chan os.Signal, 1)
	signal.Notify(sigchan, syscall.SIGINT, syscall.SIGTERM)
	broker 			:= "127.0.0.1"
	channel         := make(chan app.RpnResultDto)
	c, err := kafka.NewConsumer(&kafka.ConfigMap{
		"bootstrap.servers":               broker,
		"group.id":                        "rpn_group",
		"session.timeout.ms":              6000,
		"go.events.channel.enable":        true,
		"go.application.rebalance.enable": true,
		"default.topic.config":            kafka.ConfigMap{"auto.offset.reset": "earliest"}})

	if err != nil {
		fmt.Fprintf(os.Stderr, "[Consumer] Failed to create consumer: %s\n", err)
		os.Exit(1)
	}

	fmt.Printf("[Consumer] Created Consumer %v\n", c)
	err = c.SubscribeTopics([]string { "request" }, nil)

	run := true
	for run == true {
		select {
		case sig := <-sigchan:
			fmt.Printf("[Consumer] Caught signal %v: terminating\n", sig)
			run = false
		case ev := <-c.Events():
			switch e := ev.(type) {
			case kafka.AssignedPartitions:
				fmt.Fprintf(os.Stderr, "[Consumer] %% %v\n", e)
				c.Assign(e.Partitions)
			case kafka.RevokedPartitions:
				fmt.Fprintf(os.Stderr, "[Consumer] %% %v\n", e)
				c.Unassign()
			case *kafka.Message:
				var messageString = string(e.Value)
				fmt.Printf("[Consumer] %% Message on %s:%s\n",e.TopicPartition, messageString)
				go app.ReversePolishNotation(messageString,true,channel)
				WriteCacheHash(messageString,<-channel)
			case kafka.PartitionEOF:
				fmt.Printf("[Consumer] %% Reached %v\n", e)
			case kafka.Error:
				fmt.Fprintf(os.Stderr, "[Consumer] %% Error: %v\n", e)
				run = false
			}
		}
	}

	fmt.Printf("[Consumer] Closing consumer\n")
	c.Close()
}

func WriteCacheHash(message string,result app.RpnResultDto) {
	dir, err := os.Getwd()
	if err != nil {
		log.Fatal(err)
	}
	var filePath = dir+"/CacheResults/"+message
	file, err := os.Create(filePath)
	if err != nil {
		fmt.Println(err)
		os.Exit(1)
	}
	defer file.Close()
	if _, err := io.WriteString(file,GetFormattedResult(result)); err != nil {
		fmt.Printf("[Consumer] Can not save a file: [%s]",err)
	} else {
		fmt.Printf("[Consumer] File is saved [%s]",filePath)
	}
}

func GetFormattedResult(result app.RpnResultDto) (resultFormat string) {
	return fmt.Sprintf( "%s, %s",result.ResultValue,result.ResultTime)
}