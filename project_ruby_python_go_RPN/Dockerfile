FROM alpine:3.8
MAINTAINER Luke Myslinski "lumyslinski@gmail.com"

RUN apk add --update \
    ca-certificates \
    musl \
    curl \
    build-base \
    python3>3.6.5 \
    python3-dev \
    bash \
    sudo \
    gcc \
    libc-dev \
    linux-headers \
    openrc>0.35.5 \
    supervisor>3.3.4 \
    git \
    go

RUN pip3 install --upgrade pip setuptools
#install librdkafka
WORKDIR /home
RUN git clone https://github.com/edenhill/librdkafka.git
WORKDIR /home/librdkafka
RUN /home/librdkafka/configure
RUN make
RUN make install
#install required python3 packages
COPY ./app/Api_python/conf/requirements.txt        /home/app/Api_python/conf/requirements.txt
RUN sudo pip3 install --requirement                /home/app/Api_python/conf/requirements.txt
#nginx
RUN addgroup -g 1000 -S www-data \
 && adduser -u 1000 -D -S -G www-data www-data
RUN mkdir -p /run/nginx && mkdir /tmp/nginx && mkdir /tmp/nginx/cache && mkdir /home/Logs && mkdir /home/CacheResults
RUN apk add nginx>1.14
RUN sudo rm -f /etc/nginx/sites-available/*
RUN sudo mv /etc/nginx/nginx.conf /etc/nginx/nginx.conf.orig
COPY ./app/Api_python/conf/nginx.conf /etc/nginx/nginx.conf
#check nginx configuration files:
RUN sudo nginx -t
#uwsgi
COPY ./app/Api_python/conf/wsgi.ini /home/app/Api_python/conf/wsgi.ini
RUN apk fetch openjdk8
RUN apk add openjdk8
#Kafka, Zookeeper and other needed things
RUN mkdir /opt && mkdir /opt/kafka && mkdir /tmp/kafka-logs
RUN wget -q http://ftp.man.poznan.pl/apache/kafka/2.0.0/kafka_2.11-2.0.0.tgz && \
    tar -xvf kafka_2.11-2.0.0.tgz -C /opt/kafka && \
    cp -r /opt/kafka/kafka_2.11-2.0.0/* /opt/kafka/ && \
    rm -r /opt/kafka/kafka_2.11-2.0.0
#GO LANG
ENV GOPATH /home/app/Worker_go
ENV PATH $GOPATH/bin:/usr/local/go/bin:$PATH
RUN mkdir -p "$GOPATH/src" "$GOPATH/bin" && chmod -R 777 "$GOPATH" && chmod -R 777 "/home/Logs" && chmod -R 777 "/home/CacheResults"
#startup
COPY ./app/Api_python/conf/supervisord.conf /etc/supervisord.conf
#app
COPY ./app/ /home/app/
WORKDIR /home/app/Worker_go/src/github.com/lumyslinski/
#build worker
RUN go build -i mainApp.go
# rest api via nginx connected with uWsgi
EXPOSE 8080
#kafka
EXPOSE 9092
CMD ["supervisord"]