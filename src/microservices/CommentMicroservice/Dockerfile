# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/microservices/CommentMicroservice/CommentMicroservice.csproj", "microservices/CommentMicroservice/"]
COPY src/middlewares middlewares/
RUN dotnet restore "microservices/CommentMicroservice/CommentMicroservice.csproj"

WORKDIR "/src/microservices/CommentMicroservice"
COPY src/microservices/CommentMicroservice .
RUN dotnet build "CommentMicroservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CommentMicroservice.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CommentMicroservice.dll"]