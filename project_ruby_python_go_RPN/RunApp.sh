#!/usr/bin/env bash
echo "Removing old image alpine_rpn"
docker rm alpine_rpn
echo "Starting docker!"
docker build --no-cache -t rpn . && docker -D run --name alpine_rpn -p 8080:8080 rpn
read