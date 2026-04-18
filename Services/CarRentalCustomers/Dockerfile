FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["CarRental.sln", "."]
COPY ["Services/CarRentalCustomers/CarRentalCustomers.csproj", "Services/CarRentalCustomers/"]
COPY ["Shared/GS_Shared_Infrastructure/GS_Shared_Infrastructure.csproj", "Shared/GS_Shared_Infrastructure/"]

RUN dotnet restore "Services/CarRentalCustomers/CarRentalCustomers.csproj"

COPY . .
WORKDIR "/src/Services/CarRentalCustomers"
RUN dotnet publish "CarRentalCustomers.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "CarRentalCustomers.dll"]