# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["SiakWebApps.csproj","./"] 
RUN dotnet restore "SiakWebApps.csproj"

# Copy everything else and build
COPY . .
RUN dotnet publish "SiakWebApps.csproj" -c Release -o /app/publish

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Install curl for healthcheck and netcat for the wait script

COPY --from=build /app/publish .
# COPY wait-for-it.sh .
# RUN chmod +x ./wait-for-it.sh

# Expose port
EXPOSE 8155

# Set environment variables
# ENV ASPNETCORE_URLS=http://+:8080
# ENV ASPNETCORE_ENVIRONMENT=Production
ENV DOTNET_RUNNING_IN_CONTAINER=true \
    DOTNET_USE_POLLING_FILE_WATCHER=true \
    DOTNET_HOST_PATH=/usr/share/dotnet/dotnet

ENTRYPOINT ["dotnet", "SiakWebApps.dll"]
