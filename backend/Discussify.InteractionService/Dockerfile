FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5003 7003

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy .csproj file in order to optimize cache
COPY ./Discussify.InteractionService/Discussify.InteractionService.csproj ./Discussify.InteractionService/
RUN dotnet restore ./Discussify.InteractionService/Discussify.InteractionService.csproj

COPY . .
WORKDIR /src/Discussify.InteractionService
RUN dotnet build --no-restore

FROM base AS final
WORKDIR /src/Discussify.InteractionService
COPY --from=build /src/Discussify.InteractionService .

ENTRYPOINT ["dotnet", "run", "--urls", "http://*:5003;https://*:7003", "--environment", "Development"]
