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
- net core 2.1 with angular 6.1.4 and local MS SQL database
- [set url and secret token for google vision api](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-2.1&tabs=windows)

Define an app secret consisting of a key and its value. The secret is associated with the project's UserSecretsId value. For example, run the following command from the directory in which the .csproj file exists:
```
dotnet user-secrets set "GoogleVisionApiSection:RestToken" "TOKEN" 
```
```
dotnet user-secrets set "GoogleVisionApiSection:RestUrl" "https://vision.googleapis.com/v1/images:annotate?key={0}" 
```
The Secret Manager tool can be used from other directories too. Use the --project option to supply the file system path at which the .csproj file exists or specify the main project name in src directory. For example:

```
dotnet user-secrets set "GoogleVisionApiSection:RestToken" "TOKEN" --project "ImageApp"
```

# References/sources:
- This project is based on the https://github.com/bleenco/ngx-uploader with original lightbox https://lokeshdhakar.com/projects/lightbox2/
- I also used a style from https://cloud.google.com/vision/docs/drag-and-drop
- To create request and response models I used http://json2csharp.com/ 

# TO DO
- More tests and mocks
- More separated components in angular
- Add docker to run everything in release version