# About
Instagram-like image hosting application that additionally performs computer vision analysis via the Google Vision API and provides a visual report on the uploaded photos showing what information was found. Visually appealing modern UI with:
- dashboard with a list of uploaded photos (tile and/or list)
- adding/deleting photos
- photo details view that includes:
- enlarged view of the selected photo
- simple visual presentation of computer vision results returned by the Google Vision API

# Project
![Demo flow](demo.gif?raw=true "Demo flow")
### Requirements to run this project:
- net core 2.1 with angular 6.1.4 and local SQLite database
- due to problems with loading secrets on windows and linux platforms author switched to loading specific googleApiSettings.json file

### File googleApiSettings.json
The format of this file should look like:
```
{
  "GoogleVisionApiSection": {
    "RestUrl": "https://vision.googleapis.com/v1/images:annotate?key={0}",
    "RestToken": "SECRET_TOKEN_FOR_GOOGLEVISION"
  }
}
```
Remember to set up this file in main directory (src/ImageApp/) before You proceed with docker. 

# References/sources:
- This project is based on the https://github.com/bleenco/ngx-uploader with original lightbox https://lokeshdhakar.com/projects/lightbox2/
- I also used a style from https://cloud.google.com/vision/docs/drag-and-drop
- To create request and response models I used http://json2csharp.com/ 

## Run in Docker
Running in docker ON WINDOWS do not work - there is no communication between net core and angular. I tested it on ubuntu 18.04 and everything is working very good.
```
docker stop nc2_imageapp
docker rm nc2_imageapp
docker build -t imageapp .
docker run --name nc2_imageapp -p 5000:5000 imageapp:latest
```

# TO DO
- Tests
- More separated components in angular