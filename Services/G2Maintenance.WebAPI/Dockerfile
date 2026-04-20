FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["CarRental.sln", "."]
COPY ["Services/G2Maintenance.WebAPI/G2Maintenance.WebAPI.csproj", "Services/G2Maintenance.WebAPI/"]
COPY ["Shared/G2_Shared_Infrastructure/G2_Shared_Infrastructure.csproj", "Shared/G2_Shared_Infrastructure/"]

RUN dotnet restore "Services/G2Maintenance.WebAPI/G2Maintenance.WebAPI.csproj"

COPY . .
WORKDIR "/src/Services/G2Maintenance.WebAPI"
RUN dotnet publish "G2Maintenance.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "G2Maintenance.WebAPI.dll"]