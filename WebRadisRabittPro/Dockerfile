# ---- Base runtime image ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# ---- Build stage ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only csproj first (better caching)
COPY WebRadisRabbitPro/WebRadisRabbitPro.csproj ./WebRadisRabbitPro/
RUN dotnet restore ./WebRadisRabbitPro/WebRadisRabbitPro.csproj

# Copy remaining project files
COPY WebRadisRabbitPro/. ./WebRadisRabbitPro/
WORKDIR /src/WebRadisRabbitPro

RUN dotnet publish -c Release -o /app

# ---- Final stage ----
FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "WebRadisRabbitPro.dll"]
