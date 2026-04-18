FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["CarRental.sln", "."]
COPY ["API_Gateway/API_Gateway.csproj", "API_Gateway/"]
COPY ["Shared/GS_Shared_Infrastructure/GS_Shared_Infrastructure.csproj", "Shared/GS_Shared_Infrastructure/"]

RUN dotnet restore "API_Gateway/API_Gateway.csproj"

COPY . .
WORKDIR "/src/API_Gateway"
RUN dotnet publish "API_Gateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "API_Gateway.dll"]