# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/microservices/PostMicroservice/PostMicroservice.csproj", "microservices/PostMicroservice/"]
COPY src/middlewares middlewares/
RUN dotnet restore "microservices/PostMicroservice/PostMicroservice.csproj"

WORKDIR "/src/microservices/PostMicroservice"
COPY src/microservices/PostMicroservice .
RUN dotnet build "PostMicroservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PostMicroservice.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PostMicroservice.dll"]