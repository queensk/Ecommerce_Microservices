# Use .NET Core runtime image as base image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

# Copy source code and other files to /app directory in the image
COPY . /app

# Set working directory to /app
WORKDIR /app

# Restore dependencies and build application
RUN dotnet restore
RUN dotnet build

# Set entry point for application
ENTRYPOINT ["dotnet", "run"]
