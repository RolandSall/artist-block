# Artist Block


This application is part of the final project of the Software Engineering class at the Lebanese American University. The application aims to apply software engineering practices by implementing a software solution for a specific topic. <br>
In this project, we created **Artist Block**, an AI-powered platform that connects people interested in buying artistic works with aspiring artists around the world. Clients can use the tools offered by the platform to produce AI-generated painting designs, which they will then share with painters interested to collaborate with them. Clients can check out the painters’ profiles and previous works before engaging in any agreement. Moreover, painters can also use the platform to sell their original works. 


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

The gan service runs on [Google Colab](https://github.com/anisdismail/AI-Art-Generator-API). No need to install any dependencies just run the block and you will have a result similar to
```
ngrok: ....
```
The latter means that your FastAPI application is exposed on this specific IP and Port.

## Samples 
This service allows the other services to interact with the GAN model and provide it with text and the desired image size to generate paintings.

![image](https://github.com/anisdismail/AI-Art-Generator-API/blob/main/images/progress_img.PNG)


**Demo can be provided upon Request**

_NOTE: Some of the logic (UI,AI,BE services) might be hidden to current viewers. for full access contact any of the project admins_

# Acknowledgments

- [Roland Salloum](https://www.linkedin.com/in/roland-salloum-09687b188/)
- [Robin Karaa](https://www.linkedin.com/in/robin-karaa-a6ab51232/)
- [Anis Ismail](https://www.linkedin.com/in/anisdismail)



