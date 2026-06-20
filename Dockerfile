FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ToCarAPI.csproj", "."]
RUN dotnet restore "./ToCarAPI.csproj"
COPY . .
RUN dotnet publish "./ToCarAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
COPY ["ToCarApi.db", "/app/data/ToCarApi.db"]
ENTRYPOINT ["dotnet", "ToCarAPI.dll"]