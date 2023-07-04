FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["formulaAirline.API/formulaAirline.API.csproj", "formulaAirline.API/"]
RUN dotnet restore "formulaAirline.API/formulaAirline.API.csproj"
COPY . .
WORKDIR "/src/formulaAirline.API"
RUN dotnet build "formulaAirline.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "formulaAirline.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "formulaAirline.API.dll"]