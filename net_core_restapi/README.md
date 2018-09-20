# RestApp - ASP.NET Core 2.0 Server

This is a REST API for managing Star Wars characters

## Run

Linux/OS X:

```
sh build.sh
```

Windows:

```
build.bat
```

## Run in Docker

```
cd src/RestApp
docker build -t restapp .
docker run -p 5000:5000 restapp
```
