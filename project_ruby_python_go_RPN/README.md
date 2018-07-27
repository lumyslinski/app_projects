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
    - Api will be in Python (flask+uwsgi+nginx)
    - Worker in GO (because it is the fastest and has concurrency)
    - Endpoint in Ruby
2. Start implementing worker in GO
    - write a RPN algorithm
    - write tests
    - write [http queue worker](http://nesv.github.io/golang/2014/02/25/worker-queues-in-go.html)
