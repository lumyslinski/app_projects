# KeywordsAnalyzer
## Specification
Simple desktop or web application with following features:
1) graphic user interface with ability to type the url to analyze the keywords
2) downloading a site from the typed url
3) read keywords from the downloaded site
4) show the occurrences of each found keyword

## Requirements
- installed windows system
- installed .net 4.7.0
- installed visual studio 2018 with wpf (desktop) development

## Structure of project
In this section I will describe the structure of the project:
- **WebAnalyzer** is main WPF project based on MVVM light, run it to test the application
- **WebAnalyzer.Concrete** is a container of models and modules to proccess a site
- **WebAnalyzer.Contracts** is a container of interfaces
- **WebAnalyzer.Tests** is a test project which load Test.html (based on wp.pl site) and check for the result keywords from proccessor modules