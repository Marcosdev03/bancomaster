FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY BancoDigital.sln ./
COPY src/BancoDigital.Api/BancoDigital.Api.csproj src/BancoDigital.Api/
RUN dotnet restore src/BancoDigital.Api/BancoDigital.Api.csproj

COPY src/BancoDigital.Api/ src/BancoDigital.Api/
RUN dotnet publish src/BancoDigital.Api/BancoDigital.Api.csproj \
    --configuration Release \
    --output /app/publish \
    --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "BancoDigital.Api.dll"]
