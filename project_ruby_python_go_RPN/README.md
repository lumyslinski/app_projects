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
# Project
![Architecture](architecture.png?raw=true "Architecture")
### Requirements to run this project:
- linux (tested on ubuntu) and 8080 unused port
- installed chrome
- installed [docker](https://docs.docker.com/install/linux/docker-ce/ubuntu/#set-up-the-repository)
- installed curl
- installed ab ( Apache HTTP server benchmarking tool: ``` sudo apt-get install apache2-utils ```)

### Problems:
- sometimes docker stucks with this error:
  >Error response from daemon: driver failed programming external connectivity on endpoint - 
  ``` docker stop $(docker ps -a -q); docker rm $(docker ps -a -q); docker volume rm $(docker volume ls -qf dangling=true) ```

### Structure:
- **Api_python**: rest api that serves a method rpn/<string:requestHashedData>, requestHashedData is a string with ascii codes for each input character
```
 example: http://127.0.0.1:8080/rpn/5332493250324332523242324332513245
 5332493250324332523242324332513245 is a hash for 5 1 2 + 4 * + 3 -
```
- **Endpoint_ruby**: is an interface endpoint in Ruby which loads a file text with calculations (more in About). You can pass argument with full path of input data to main.rb 
```
/usr/bin/ruby "/app/Endpoint_ruby/app/main.rb" <filePathWithInputData>
```
- **Worker_go**: is a worker in GO to resolve RPN method via Apache Kafka. It writes result to cache, so the rest api will load firstly cached data and then try to call worker via Kafka
- **Dockerfile**: file for running in docker
- **Logs**: logs directory with log.text file for requests and responses
- **CacheResults**: cache directory for cached requests and responses
- **Run.sh: it runs app in docker and app test, so it is a main start for the project**
- **RunApp.sh**: it runs app in docker
- **RunEndpoint.sh**: it runs endpoint with default file
- **RunTests.sh**: script with tests and loading log file

# Tests
There are two types of tests in this project:
1. Unit test to check working functionality in specified languages
2. Stress test to check 20 multiple requests at one time. I used ab (Apache HTTP server benchmarking tool)
```
Benchmarking 127.0.0.1 (be patient).....done

Server Software:        nginx/1.14.0
Server Hostname:        127.0.0.1
Server Port:            8080

Document Path:          /rpn/5332493250324332523242324332513245
Document Length:        19 bytes

Concurrency Level:      20
Time taken for tests:   2.324 seconds
Complete requests:      50
Failed requests:        0
Total transferred:      11800 bytes
HTML transferred:       950 bytes
Requests per second:    21.52 [#/sec] (mean)
Time per request:       929.582 [ms] (mean)
Time per request:       46.479 [ms] (mean, across all concurrent requests)
Transfer rate:          4.96 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    1   0.7      0       2
Processing:     0  928 1147.3      1    2322
Waiting:        0  928 1147.3      1    2322
Total:          0  929 1147.9      1    2323
WARNING: The median and mean for the initial connection time are not within a normal deviation
        These results are probably not that reliable.

Percentage of the requests served within a certain time (ms)
  50%      1
  66%   2319
  75%   2319
  80%   2321
  90%   2322
  95%   2322
  98%   2323
  99%   2323
 100%   2323 (longest request)
Output of nginx status from http://127.0.0.1:8080/nginx_status
Active connections: 1 
server accepts handled requests
 51 51 51 
Reading: 0 Writing: 1 Waiting: 0 
```
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
    - start writting an interface endpoint in ruby (most difficult to setup environment and ide because of errors):
        - console app was chosen to load files with data and process it to rest api in python
        - start coding classes and tests
        - test whole communication of the system
        - addes some fixes and features, cleaning up the code
    - start building a docker to run everything
        - start rest api in python
        - add everything to start worker go when system starts
        - run ruby endpoint interface???
    - modify architecture diagram with new changes