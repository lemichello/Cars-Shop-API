FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CarsShop.API/CarsShop.API.csproj", "CarsShop.API/"]
COPY ["CarsShop.DAL/CarsShop.DAL.csproj", "CarsShop.DAL/"]
COPY ["CarsShop.DTO/CarsShop.DTO.csproj", "CarsShop.DTO/"]
RUN dotnet restore "CarsShop.API/CarsShop.API.csproj"
COPY . .
WORKDIR "/src/CarsShop.API"
RUN dotnet build "CarsShop.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarsShop.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet CarsShop.API.dll