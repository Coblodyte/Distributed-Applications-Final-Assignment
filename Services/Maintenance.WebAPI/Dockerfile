FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["CarRental.sln", "."]
COPY ["Services/Maintenance.WebAPI/Maintenance.WebAPI.csproj", "Services/Maintenance.WebAPI/"]
COPY ["Shared/GS_Shared_Infrastructure/GS_Shared_Infrastructure.csproj", "Shared/GS_Shared_Infrastructure/"]

RUN dotnet restore "Services/Maintenance.WebAPI/Maintenance.WebAPI.csproj"

COPY . .
WORKDIR "/src/Services/Maintenance.WebAPI"
RUN dotnet publish "Maintenance.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "Maintenance.WebAPI.dll"]