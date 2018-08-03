#!/bin/bash
# Just run docker from this directory with this command:
while true; do
    COUNT=$(docker ps -a | grep "alpine_rpn" | wc -l)
    if (($COUNT > 0)); then
        break
    fi
    echo 'Checking if docker container "alpine_rpn" is running. Hit CTRL+C'
    echo "Check for running applications for 8080"
    netstat -tulpn | grep :8080
    sleep 5
done

echo 'Container restapi exists. Running end-to-end test with 20 multiple requests at one time'
ab -n 50 -c 20 http://127.0.0.1:8080/rpn/5332493250324332523242324332513245
echo 'Output of nginx status from http://127.0.0.1:8080/nginx_status'
curl "http://127.0.0.1:8080/nginx_status"
echo 'Stress test finished. Running chrome to test api in 10 seconds'
sleep 10
google-chrome "http://127.0.0.1:8080/"
docker exec -it alpine_rpn tail -f /home/Logs/log.txt