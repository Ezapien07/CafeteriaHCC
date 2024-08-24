#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
# Usar una imagen compatible con ARM si estás en una arquitectura ARM
FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim-arm64v8 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim-arm64v8 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CafeteriaHCC.csproj", "CafeteriaHCC/"]
RUN dotnet restore "./CafeteriaHCC/CafeteriaHCC.csproj"
COPY . .
WORKDIR "/src/CafeteriaHCC"
RUN dotnet build "./CafeteriaHCC.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN mkdir -p /app/publish
RUN dotnet publish "./CafeteriaHCC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CafeteriaHCC.dll"]
