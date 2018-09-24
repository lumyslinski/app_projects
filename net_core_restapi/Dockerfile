FROM microsoft/dotnet:2.1-sdk AS build-env
MAINTAINER Luke Myslinski "lumyslinski@gmail.com"
WORKDIR /app
EXPOSE 5000

WORKDIR /src
COPY ["./src/RestApp/RestApp.csproj", "RestApp/"]
RUN dotnet restore "RestApp/RestApp.csproj"
COPY ./src .
WORKDIR "/src/RestApp"
RUN dotnet build "RestApp.csproj" -c Release -o /app
RUN dotnet publish "RestApp.csproj" -c Release -o /app /p:PublishWithAspNetCoreTargetManifest="false"

FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app .
ENTRYPOINT ["dotnet", "RestApp.dll"]
