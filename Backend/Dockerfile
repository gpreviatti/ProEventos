FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build

COPY . .
RUN dotnet restore "./src/ProEventos.API/ProEventos.API.csproj" 

FROM build AS publish
RUN dotnet publish "./src/ProEventos.API/ProEventos.API.csproj" -c Release -o /app/publish

RUN date
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProEventos.API.dll"]
