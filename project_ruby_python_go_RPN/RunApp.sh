#!/usr/bin/env bash
echo "Starting docker!"
docker volume rm alpine_rpn
docker build --no-cache -t rpn . && docker -D run --name alpine_rpn -p 8080:8080 rpn