##########
# STAGE 1
##########
FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /app

# Copy files
COPY . .

# Run restore and publish
RUN dotnet restore "./src/ProEventos.API/ProEventos.API.csproj"  --force --no-cache \
 && dotnet publish "./src/ProEventos.API/ProEventos.API.csproj" -c Release -o /app/publish

###########
# STAGE 2
###########
FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine
ENV TZ=America/Sao_Paulo
WORKDIR /app

RUN apk add tzdata \
 && ln -sf /usr/share/zoneinfo/${TZ} /etc/localtime \
 && echo "${TZ}" > /etc/timezone

COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "ProEventos.API.dll"]
