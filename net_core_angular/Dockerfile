FROM microsoft/dotnet:2.1-sdk AS build

# set up node
# Setup NodeJs
RUN apt-get update && \
    apt-get install -y wget && \
    apt-get install -y gnupg2 && \
    wget -qO- https://deb.nodesource.com/setup_8.x | bash - && \
    apt-get install -y build-essential nodejs
# End setup

WORKDIR /app
# Enable detection of running in a container
ENV ENV ASPNETCORE_URLS=http://+:5000 \
    DOTNET_RUNNING_IN_CONTAINER=true \
    DOTNET_CLI_TELEMETRY_OPTOUT=1

COPY ./src /src

WORKDIR /src
RUN dotnet restore "ImageApp/ImageApp.csproj"
RUN dotnet restore "ImageApp.Data/ImageApp.Data.csproj"

WORKDIR /src/ImageApp
RUN dotnet build "ImageApp.csproj" -c Release -o /app

WORKDIR /src/ImageApp.Data
RUN dotnet build "ImageApp.Data.csproj" -c Release -o /app

WORKDIR /src/ImageApp
RUN dotnet publish "ImageApp.csproj" -c Release -o /app

FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "ImageApp.dll"]
EXPOSE 5000