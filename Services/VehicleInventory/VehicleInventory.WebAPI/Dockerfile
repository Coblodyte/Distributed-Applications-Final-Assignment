FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["CarRental.sln", "."]
COPY ["Services/VehicleInventory/VehicleInventory.WebAPI/VehicleInventory.WebAPI.csproj", "Services/VehicleInventory/VehicleInventory.WebAPI/"]
COPY ["Services/VehicleInventory/VehicleInventory.Application/VehicleInventory.Application.csproj", "Services/VehicleInventory/VehicleInventory.Application/"]
COPY ["Services/VehicleInventory/VehicleInventory.Domain/VehicleInventory.Domain.csproj", "Services/VehicleInventory/VehicleInventory.Domain/"]
COPY ["Services/VehicleInventory/VehicleInventory.Infrastructure/VehicleInventory.Infrastructure.csproj", "Services/VehicleInventory/VehicleInventory.Infrastructure/"]
COPY ["Shared/GS_Shared_Infrastructure/GS_Shared_Infrastructure.csproj", "Shared/GS_Shared_Infrastructure/"]

RUN dotnet restore "Services/VehicleInventory/VehicleInventory.WebAPI/VehicleInventory.WebAPI.csproj"

COPY . .
WORKDIR "/src/Services/VehicleInventory/VehicleInventory.WebAPI"
RUN dotnet publish "VehicleInventory.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "VehicleInventory.WebAPI.dll"]