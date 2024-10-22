FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001 7001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy .csproj file in order to optimize cache
COPY ./Discussify.PostService/Discussify.PostService.csproj ./Discussify.PostService/
RUN dotnet restore ./Discussify.PostService/Discussify.PostService.csproj

COPY . .
WORKDIR /src/Discussify.PostService
RUN dotnet build --no-restore

FROM base AS final
WORKDIR /src/Discussify.PostService
COPY --from=build /src/Discussify.PostService .

ENTRYPOINT ["dotnet", "run", "--urls", "http://*:5001;https://*:7001", "--environment", "Development"]
