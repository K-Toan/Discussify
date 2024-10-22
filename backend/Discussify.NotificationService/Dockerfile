FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5005 7005

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy .csproj file in order to optimize cache
COPY ./Discussify.NotificationService/Discussify.NotificationService.csproj ./Discussify.NotificationService/
RUN dotnet restore ./Discussify.NotificationService/Discussify.NotificationService.csproj

COPY . .
WORKDIR /src/Discussify.NotificationService
RUN dotnet build --no-restore

FROM base AS final
WORKDIR /src/Discussify.NotificationService
COPY --from=build /src/Discussify.NotificationService .

ENTRYPOINT ["dotnet", "run", "--urls", "http://*:5005;https://*:7005", "--environment", "Development"]
