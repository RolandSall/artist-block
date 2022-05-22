# artist-block.


This is application is part of the software engineering class. The application objective is to apply software engineering practices by implementing a software solution for a specific topic.


# Application Architecture

![image](https://user-images.githubusercontent.com/45897168/167974117-8f6938e3-6133-479f-8924-b298111efdbe.png)

The software solution follow a microservice approach using common patterns such as gateway, discovery and configuration service. The application also consist of two standalone react application that each serve a puprpose


# Running The React Application

To run the front-end application, simply navigate to the desired directory. There you will a find another README.md file that will help you install the dependencies

# Running The Backend Services

## Spring Boot Services
Each of the backend services is equiped with a docker file. To run the services run the following commands

```
docker build -t <image-name> .
docker run <image-name>
```
Or you could simply run a spring boot application from command file by exectuing the jar file or with the help of an [IDE](https://www.jetbrains.com/help/idea/spring-boot.html)

## .NET service

First you to have [.NET CORE 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) installed. After running installing the SDK, you could Rider to run the application easily by selecting of
the two environements
```
Staging envrionment --> Linked To Staging DB (if you have access to the credentials)
Local envrionment --> Linked To dev DB
```
You could also run the application using docker.

## GAN service

The gan service runs on [Google Colab](https://colab.research.google.com/drive/1Ee1g8QbZ6nCtN56c4-rdEVexkC8tBuV0#scrollTo=80lSTTCevP_s). No need to instal any dependencies just run the block and you will a result similir to
```
ngrok:....
```
The latter means that your FastAPI application is exposed on this specific IP and Port



