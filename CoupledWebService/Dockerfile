FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CoupledServicesApi/CoupledServicesApi.csproj", "CoupledServicesApi/"]
RUN dotnet restore "CoupledServicesApi/CoupledServicesApi.csproj"
COPY . .
WORKDIR "/src/CoupledServicesApi"
RUN dotnet build "CoupledServicesApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoupledServicesApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoupledServicesApi.dll"]