# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy solution and project files
COPY InsightLog.sln .
COPY src/InsightLog.API/*.csproj src/InsightLog.API/
COPY src/InsightLog.Application/*.csproj src/InsightLog.Application/
COPY src/InsightLog.Domain/*.csproj src/InsightLog.Domain/
COPY src/InsightLog.Infrastructure/*.csproj src/InsightLog.Infrastructure/
COPY src/InsightLog.Tests.Unit/*.csproj src/InsightLog.Tests.Unit/
COPY src/InsightLog.ServiceDefaults/*.csproj src/InsightLog.ServiceDefaults/
COPY src/InsightLog.AppHost/*.csproj src/InsightLog.AppHost/

# Explicitly restore from the .sln file
RUN dotnet restore InsightLog.sln

# Copy all source files
COPY src/ ./src/

# Publish the API project
WORKDIR /app/src/InsightLog.API
RUN dotnet publish -c Release -o /app/out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "InsightLog.API.dll", "--urls", "http://0.0.0.0:80"]
