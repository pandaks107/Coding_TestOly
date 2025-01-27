# Base runtime image
FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Coding_TestOly.csproj", "."]
RUN dotnet restore "./Coding_TestOly.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./Coding_TestOly.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Coding_TestOly.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Ensure the `/log` directory exists (mapped to C:\junk)
RUN mkdir -p /log

# Generate an output file in `/log` on container start
CMD ["sh", "-c", "echo 'This is a test output file.' > /log/output.txt && dotnet Coding_TestOly.dll"]
