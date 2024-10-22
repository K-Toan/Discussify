FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
EXPOSE 8080
EXPOSE 8081

# copy .csproj file
COPY ./*.csproj ./
RUN dotnet restore

COPY . .

# build project
RUN dotnet publish -c Release -o /app

# create final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Discussify.IdentityService.dll"]