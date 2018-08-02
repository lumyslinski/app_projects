# About
Create 3 small applications which will mutually evaluating [Reverse Polish Notation](https://en.wikipedia.org/wiki/Reverse_Polish_notation) expressions
and log the input and time it took to calculate it.
Architecture:
1. Endpoint interface for gathering input data (stdin) and output results (stdout) (language Ruby)
2. API for getting requests, sending to the worker and logging input and timings. Logs should be
saved to a file. (language Python)
3. Worker responsible for the process of calculation (language GO)

Input:
```
N - number of input expressions to solve
X1 - RPN expression to evaluate, separated by \n
X2
Xn
```
Output:
```
Y1, TIME1 - pair consisting of result and time taken from input to get back the result (round trip)
Y2, TIME2
Yn, TIMEn
```
# Example
Input:
```
2
3 4 +
5 1 2 + 4 * + 3 -
```
Output:
```
7, 0.235
14, 0.280
```
# Instructions

# Tests

# Steps

1. Make an architecture model
    - Api will be in Python:
        - api will receive requests via flask (using uwsgi+nginx to handle multiple requests)
        - api will send requests to worker via apache kafka
    - Worker in GO (because it is the fastest and has concurrency):
        - worker will read input data and send result via apache kafka
    - Endpoint in Ruby
        - endpoint will load local files and send bytes requests of json to api
         
2. Start implementing worker in GO
    - write a RPN algorithm
    - write tests
    - write [http queue worker](http://nesv.github.io/golang/2014/02/25/worker-queues-in-go.html)

3. Start implementing communication layer:
    - dive deep into Apache Kafka (distributed streaming platform) which has got many clients for many languages including ruby, python and GO and is elastic, highly scalable, fault-tolerant on mac,linux,windows
        - [installation and configuration](https://devops.profitbricks.com/tutorials/install-and-configure-apache-kafka-on-ubuntu-1604-1/)
        - requirements:
            - physical: kafka_2.11-2.0.0, java8
            - non functional: [there is a maximum length of message which is 1MB](https://www.cloudera.com/documentation/kafka/latest/topics/kafka_performance.html)
    - clients: [ruby](https://github.com/appsignal/rdkafka-ruby), [python](https://github.com/confluentinc/confluent-kafka-python), [GO](https://github.com/confluentinc/confluent-kafka-go)
    - start example code in GO lang (producer and consumer)
    - analyze the way of executing producer in rest api ([there is a rest client for kafka](https://github.com/confluentinc/kafka-rest))
    - analyze the way of getting result from producer - the result should be stored in unique topic
        - now this led me to reanalyzed the way of sending requests, if we can make a simple hash function for RNP (for example replace spaces with s) then nginx can cache the result by this hash as a key and will not execute GO worker
        - getting things together, endpoint in ruby will send a request to rest api in python, then rest api will send a request to worker and this will
    - implemented a hash function (convert each character to decimal ASCII) to be a key for RPN request
        - wrote test functions in Python and GO
    - start writting a communication bridge between Python and GO:
        - first step is to make an communication test with the cache function
        - next step is to implement a rest api that will firstly read a result from cache directory. If there is no file then application will send a producer request to kafka to process RPN and write result into cached file which rest api will read
        - test communication flow with various data