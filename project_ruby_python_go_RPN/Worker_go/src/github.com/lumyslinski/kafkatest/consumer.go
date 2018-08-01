package kafkatest

import (
	"github.com/confluentinc/confluent-kafka-go/kafka"
	"fmt"
	"os"
)

const MIN_COMMIT_COUNT = 2

func TestConsumer() {

	c, err := kafka.NewConsumer(&kafka.ConfigMap{
		"bootstrap.servers": "127.0.0.1:9092",
		"group.id":          "myGroup",
		"auto.offset.reset": "earliest",
		"go.application.rebalance.enable": true})

	if err != nil {
		panic(err)
	}

	c.SubscribeTopics([]string{"myTopic", "^aRegex.*[Tt]opic"}, nil)

	run := true
	msg_count := 0

	for run == true {
		ev := c.Poll(0)
		switch e := ev.(type) {
		case kafka.AssignedPartitions:
			fmt.Fprintf(os.Stderr, "%% %v\n", e)
			c.Assign(e.Partitions)
		case kafka.RevokedPartitions:
			fmt.Fprintf(os.Stderr, "%% %v\n", e)
			c.Unassign()
		case *kafka.Message:
			msg_count += 1
			if msg_count % MIN_COMMIT_COUNT == 0 {
				c.Commit()
			}

			fmt.Printf("[Consumer] %% Message on %s:\\%s\n",
				e.TopicPartition, string(e.Value))

		case kafka.PartitionEOF:
			fmt.Printf("[Consumer] %% Reached %v\n", e)
		case kafka.Error:
			fmt.Fprintf(os.Stderr, "[Consumer] %% Error: %v\n", e)
			run = false
		}
	}

	c.Close()
}
