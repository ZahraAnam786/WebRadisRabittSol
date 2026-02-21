# ---- Base runtime image ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# ---- Build stage ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only csproj for caching
COPY *.csproj ./  # now Dockerfile is inside context, just copy csproj
RUN dotnet restore

# Copy all project files
COPY . .

WORKDIR /src
RUN dotnet publish -c Release -o /app

# ---- Final stage ----
FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "WebRadisRabittPro.dll"]
