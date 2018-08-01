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
from confluent_kafka import Producer
from flask_restful import Resource
from FileCacheLoader import FileCacheLoader
import sys


class ProducerKafka(Resource):

    def get(self, requestData):
        rpnCacheResult = FileCacheLoader.loadFileCache(requestData)
        if len(rpnCacheResult) > 0:
            return rpnCacheResult
        else:
            broker = '127.0.0.1'
            topic_request = "request"
            conf = {'bootstrap.servers': broker}
            p = Producer(**conf)

            try:
                p.poll(0)
                p.produce(topic_request, requestData.encode('utf-8'))

            except BufferError as e:
                return "[Producer] %% Local producer queue is full (%d messages awaiting delivery): try again".format(len(p))
            except:
                return "Unexpected error [{0}]".format(sys.exc_info()[0])
            p.flush()
            # check again
            fileCacheLoader = FileCacheLoader(requestData)
            fileCacheLoader.start()
            fileCacheLoader.join()
            rpnCacheResult = fileCacheLoader.result
            if len(rpnCacheResult) > 0:
                return rpnCacheResult
            else:
                return "try again..."



# def delivery_callback(self, err, msg):
#    if err:
#        sys.stderr.write('[Producer] %% Message failed delivery: %s\n' % err)
#    else:
#        sys.stderr.write(
#            '[Producer] %% Message delivered to %s [%d] @ %o\n' % (msg.topic(), msg.partition(), msg.offset()))
