FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["CarRental.sln", "."]
COPY ["Applications/CarRentalPlatform/CarRentalPlatform.csproj", "Applications/CarRentalPlatform/"]
COPY ["Shared/GS_Shared_Infrastructure/GS_Shared_Infrastructure.csproj", "Shared/GS_Shared_Infrastructure/"]
COPY ["Services/CarRentalCustomers/CarRentalCustomers.csproj", "Services/CarRentalCustomers/"]
COPY ["Services/Maintenance.WebAPI/Maintenance.WebAPI.csproj", "Services/Maintenance.WebAPI/"]
COPY ["Services/VehicleInventory/VehicleInventory.WebAPI/VehicleInventory.WebAPI.csproj", "Services/VehicleInventory/VehicleInventory.WebAPI/"]
COPY ["Services/VehicleInventory/VehicleInventory.Application/VehicleInventory.Application.csproj", "Services/VehicleInventory/VehicleInventory.Application/"]
COPY ["Services/VehicleInventory/VehicleInventory.Domain/VehicleInventory.Domain.csproj", "Services/VehicleInventory/VehicleInventory.Domain/"]
COPY ["Services/VehicleInventory/VehicleInventory.Infrastructure/VehicleInventory.Infrastructure.csproj", "Services/VehicleInventory/VehicleInventory.Infrastructure/"]

RUN dotnet restore "Applications/CarRentalPlatform/CarRentalPlatform.csproj"

COPY . .
WORKDIR "/src/Applications/CarRentalPlatform"
RUN dotnet publish "CarRentalPlatform.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "CarRentalPlatform.dll"]