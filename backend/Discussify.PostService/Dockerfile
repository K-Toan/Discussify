# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files and restore dependencies
COPY ./*.csproj ./
RUN dotnet restore

# Copy all files and build project
COPY . ./
RUN dotnet publish -c Release -o /app /p:UseAppHost=false

# Stage 2: Final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Expose necessary ports
EXPOSE 5001

# Copy build artifacts
COPY --from=build /app ./

# Start the application
ENTRYPOINT ["dotnet", "Discussify.PostService.dll"]
