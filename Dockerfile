# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project files
COPY ["src/MarcasApi/MarcasApi.csproj", "src/MarcasApi/"]
RUN dotnet restore "src/MarcasApi/MarcasApi.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/src/MarcasApi"
RUN dotnet build "MarcasApi.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "MarcasApi.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MarcasApi.dll"]
