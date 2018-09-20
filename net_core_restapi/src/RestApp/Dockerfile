FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 50695
EXPOSE 44395

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["RestApp/RestApp.csproj", "RestApp/"]
RUN dotnet restore "RestApp/RestApp.csproj"
COPY . .
WORKDIR "/src/RestApp"
RUN dotnet build "RestApp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "RestApp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RestApp.dll"]