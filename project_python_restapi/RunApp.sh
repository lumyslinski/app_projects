#!/bin/bash
# Just run docker from this directory with this command:
echo "Starting docker!"
docker build -t restapi . && docker run --name alpine_restapi -p 8080:8080 restapi

