FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["CarRental.sln", "."]
COPY ["Applications/CarRentalPlatform/CarRentalPlatform.csproj", "Applications/CarRentalPlatform/"]
COPY ["Shared/G2_Shared_Infrastructure/G2_Shared_Infrastructure.csproj", "Shared/G2_Shared_Infrastructure/"]
COPY ["Services/G2CarRentalCustomers/G2CarRentalCustomers.csproj", "Services/G2CarRentalCustomers/"]
COPY ["Services/G2Maintenance.WebAPI/G2Maintenance.WebAPI.csproj", "Services/G2Maintenance.WebAPI/"]
COPY ["Services/G2VehicleInventory/G2VehicleInventory.WebAPI/G2VehicleInventory.WebAPI.csproj", "Services/G2VehicleInventory/G2VehicleInventory.WebAPI/"]
COPY ["Services/G2VehicleInventory/G2VehicleInventory.Application/G2VehicleInventory.Application.csproj", "Services/G2VehicleInventory/G2VehicleInventory.Application/"]
COPY ["Services/G2VehicleInventory/G2VehicleInventory.Domain/G2VehicleInventory.Domain.csproj", "Services/G2VehicleInventory/G2VehicleInventory.Domain/"]
COPY ["Services/G2VehicleInventory/G2VehicleInventory.Infrastructure/G2VehicleInventory.Infrastructure.csproj", "Services/G2VehicleInventory/G2VehicleInventory.Infrastructure/"]

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