FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5002 7002

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy .csproj file in order to optimize cache
COPY ./Discussify.CommentService/CommentService.csproj ./Discussify.CommentService/
RUN dotnet restore ./Discussify.CommentService/CommentService.csproj

COPY . .
WORKDIR /src/Discussify.CommentService
RUN dotnet build --no-restore

FROM base AS final
WORKDIR /src/Discussify.CommentService
COPY --from=build /src/Discussify.CommentService .

ENTRYPOINT ["dotnet", "run", "--urls", "http://*:5002;https://*:7002", "--environment", "Development"]
