FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["VinylShop.API/VinylShop.API.csproj", "VinylShop.API/"]
COPY ["VinylShop.Core/VinylShop.Core.csproj", "VinylShop.Core/"]
COPY ["VinylShop.DataAccess/VinylShop.DataAccess.csproj", "VinylShop.DataAccess/"]
COPY ["VinylShop.Application/VinylShop.Application.csproj", "VinylShop.Application/"]
COPY ["VinylShop.Infrastructure/VinylShop.Infrastructure.csproj", "VinylShop.Infrastructure/"]
RUN dotnet restore "VinylShop.API/VinylShop.API.csproj"
COPY . .
WORKDIR "/src/VinylShop.API"
RUN dotnet build "VinylShop.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "VinylShop.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VinylShop.API.dll"]
