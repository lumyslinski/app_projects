# About
The aim of this task is to create a simple Instagram-like image hosting application that
additionally performs computer vision analysis via the Google Vision API and provides a
visual report on the uploaded photos showing what information was found.

Visually appealing modern UI (use some available solutions)
- Dashboard with a list of uploaded photos (tile and/or list)
- Adding/deleting photos
- Photo details view that includes:
    - Enlarged view of the selected photo
    - Simple visual presentation of computer vision results returned by the Google Vision API

# Project
![Demo flow](demo.gif?raw=true "Demo flow")
### Requirements to run this project:
- net core 2.1 with angular 6.1.4 and local SQLite database
- due to problems with loading secrets on windows and linux platforms author switched to loading specific googleApiSettings.json file

# References/sources:
- This project is based on the https://github.com/bleenco/ngx-uploader with original lightbox https://lokeshdhakar.com/projects/lightbox2/
- I also used a style from https://cloud.google.com/vision/docs/drag-and-drop
- To create request and response models I used http://json2csharp.com/ 

## Run in Docker
Running in docker do not work yet because it does not support ms sql localdb. Resolved this by using SQLite.
```
docker stop nc2_imageapp
docker rm nc2_imageapp
docker build -t imageapp .
docker run --name nc2_imageapp -p 80:80 imageapp:latest
```

# TO DO
- More tests and mocks
- More separated components in angular