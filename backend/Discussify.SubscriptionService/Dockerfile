FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5004 7004

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy .csproj file in order to optimize cache
COPY ./Discussify.SubscriptionService/Discussify.SubscriptionService.csproj ./Discussify.SubscriptionService/
RUN dotnet restore ./Discussify.SubscriptionService/Discussify.SubscriptionService.csproj

COPY . .
WORKDIR /src/Discussify.SubscriptionService
RUN dotnet build --no-restore

FROM base AS final
WORKDIR /src/Discussify.SubscriptionService
COPY --from=build /src/Discussify.SubscriptionService .

ENTRYPOINT ["dotnet", "run", "--urls", "http://*:5004;https://*:7004", "--environment", "Development"]
