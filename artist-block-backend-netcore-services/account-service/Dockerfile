# Get Base Image (Full .NET Core SDK)
# build the image using: docker build -f local.Dockerfile -t rolandsall24/haqq-local-env:1.0.0 .
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

LABEL author="Roland Salloum"

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
ENTRYPOINT dotnet "account-service.dll"