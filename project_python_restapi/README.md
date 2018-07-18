Just run docker from this directory with this command:
docker build -t restapi . && docker run -p 8080:8080 restapi

Your rest api will be hosted at http://127.0.0.1:8080/
You can run nginx status at http://127.0.0.1:8080/nginx_status