# ---- Base runtime image ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# ---- Build stage ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only csproj first (better caching)
COPY WebRadisRabittPro/WebRadisRabittPro.csproj ./WebRadisRabittPro/
RUN dotnet restore ./WebRadisRabittPro/WebRadisRabittPro.csproj

# Copy remaining project files
COPY WebRadisRabittPro/. ./WebRadisRabittPro/
WORKDIR /src/WebRadisRabittPro

RUN dotnet publish -c Release -o /app

# ---- Final stage ----
FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "WebRadisRabittPro.dll"]
