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
	          supervisor>3.3.4

RUN pip3 install --upgrade pip setuptools
#install required python3 packages
COPY ./conf/requirements.txt        /home/app/requirements.txt
RUN sudo pip3 install --requirement /home/app/requirements.txt
#nginx
RUN addgroup -g 1000 -S www-data \
 && adduser -u 1000 -D -S -G www-data www-data
RUN mkdir -p /run/nginx
RUN apk add nginx>1.14 && mkdir /tmp/nginx
RUN sudo rm -f /etc/nginx/sites-available/*
RUN sudo mv /etc/nginx/nginx.conf /etc/nginx/nginx.conf.orig
COPY ./conf/nginx.conf /etc/nginx/nginx.conf
#check nginx configuration files:
RUN sudo nginx -t
#uwsgi
COPY ./conf/wsgi.ini /home/app/wsgi.ini
#startup
COPY ./conf/supervisord.conf /etc/supervisord.conf
#app
COPY ./app/ /home/app
EXPOSE 8080
CMD ["supervisord"]