#!/usr/bin/env python
#
# Copyright 2016 Confluent Inc.
#
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
#
# http://www.apache.org/licenses/LICENSE-2.0
#
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
#

#
# Example Kafka Producer.
# Reads lines from stdin and sends to Kafka.
#

from confluent_kafka.cimpl import KafkaError
from confluent_kafka import Consumer,Producer
import sys
import time


def test_hashMethod():
    input = "3 4 +"
    hashResult = ""
    for elem in input:
        code = ord(elem)
        hashResult += str(code)
    return hashResult

if __name__ == '__main__':
    start_time = time.time()
    # Producer configuration
    # See https://github.com/edenhill/librdkafka/blob/master/CONFIGURATION.md
    broker = '127.0.0.1'
    topic_request = "request"
    conf = {'bootstrap.servers': broker}
    # Create Producer instance
    p = Producer(**conf)

    # Optional per-message delivery callback (triggered by poll() or flush())
    # when a message has been successfully delivered or permanently
    # failed delivery (after retries).
    def delivery_callback(err, msg):
        if err:
            sys.stderr.write('[Producer] %% Message failed delivery: %s\n' % err)
        else:
            sys.stderr.write('[Producer] %% Message delivered to %s [%d] @ %o\n' % (msg.topic(), msg.partition(), msg.offset()))


    try:
        # Serve delivery callback queue.
        # NOTE: Since produce() is an asynchronous API this poll() call
        #       will most likely not serve the delivery callback for the
        #       last produce()d message.
        p.poll(0)
        # Produce line (without newline)
        p.produce(topic_request, test_hashMethod().encode('utf-8'), callback=delivery_callback)
        sys.stderr.write('[Producer] %% Message sent ?!\n')

    except BufferError as e:
        sys.stderr.write('[Producer] %% Local producer queue is full (%d messages awaiting delivery): try again\n' % len(p))

    # Wait until all messages have been delivered
    sys.stderr.write('[Producer] %% Waiting for %d deliveries\n' % len(p))
    p.flush()