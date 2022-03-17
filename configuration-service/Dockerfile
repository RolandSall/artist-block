FROM openjdk:11
VOLUME /tmp
ADD  target/config_service.jar config_service.jar
EXPOSE 8080
ENTRYPOINT ["java","-jar","/config_service.jar"]