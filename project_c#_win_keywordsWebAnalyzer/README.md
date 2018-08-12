# KeywordsAnalyzer
## Specification
Simple desktop or web application with following features:
1) graphic user interface with ability to type the url to analyze the keywords
2) downloading a site from the typed url
3) read keywords from the downloaded site
4) show the occurrences of each found keyword

## Structure of project
In this section I will describe the structure of the project:
- **WebAnalyzer** is main WPF project based on MVVM light, run it to test the application
- **WebAnalyzer.Concrete** is a container of models
- **WebAnalyzer.Contracts** is a container of interfaces
- **webAnalyzer.Core** is a core project which includes the modules to proccess a site
- **WebAnalyzer.Tests** is a test project which load Test.html (based on wp.pl site) and check for the result keywords from proccessor modules