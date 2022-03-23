# Get Base Image (Full .NET Core SDK)
# build the image using: docker build -f staging.Dockerfile -t rolandsall24/haqq-staging-env:1.0.0
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

LABEL author="Roland Salloum"

WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT Staging

# Copy csproj and restore
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Generate runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

COPY --from=build-env /app/out .

# Locally
# ENTRYPOINT export ASPNETCORE_ENVIRONMENT=Staging && exec dotnet "Haqq-Backend.dll"

# For Postgress

ENV ASPNETCORE_ENVIRONMENT=Staging
CMD ASPNETCORE_URLS=http://*:$PORT && dotnet account-service.dll