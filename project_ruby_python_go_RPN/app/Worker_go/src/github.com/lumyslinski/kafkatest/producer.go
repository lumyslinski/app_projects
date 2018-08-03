package kafkatest

import (
	"github.com/confluentinc/confluent-kafka-go/kafka"
	"fmt"
	)

func TestProducer() {
	p, err := kafka.NewProducer(&kafka.ConfigMap{"bootstrap.servers": "127.0.0.1:9092", "group.id": "myGroup"})
	if err != nil {
		panic(err)
	}

	defer p.Close()

	// Delivery report handler for produced messages
	go func() {
		for e := range p.Events() {
			switch ev := e.(type) {
			case *kafka.Message:
				if ev.TopicPartition.Error != nil {
					fmt.Printf("[Producer] Delivery failed: %v\n", ev.TopicPartition)
				} else {
					fmt.Printf("[Producer] Delivered message to %v\n", ev.TopicPartition)
				}
			}
		}
	}()

	// Produce messages to topic (asynchronously)
	topic := "myTopic"
	for _, word := range []string{"Welcome", "to", "the", "Confluent", "Kafka", "Golang", "client", "2 3 5 7 8 9 + 8 7 / 4 5 - 9 7 *"} {
		p.Produce(&kafka.Message{
			TopicPartition: kafka.TopicPartition{Topic: &topic, Partition: kafka.PartitionAny},
			Value:          []byte(word),
		}, nil)
	}

	// Wait for message deliveries
	p.Flush(15 * 1000)
}
