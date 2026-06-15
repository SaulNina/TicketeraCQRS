# Estructura base para compilar el proyecto usando el SDK de .NET 9
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar la solución y restaurar dependencias
COPY ["TicketeraCQRS.sln", "./"]
COPY ["Ticketera.API/Ticketera.API.csproj", "Ticketera.API/"]
COPY ["Ticketera.Application/Ticketera.Application.csproj", "Ticketera.Application/"]
COPY ["Ticketera.Domain/Ticketera.Domain.csproj", "Ticketera.Domain/"]
COPY ["Ticketera.Infrastructure/Ticketera.Infrastructure.csproj", "Ticketera.Infrastructure/"]
RUN dotnet restore

# Copiar todo el código y compilar en modo Release
COPY . .
WORKDIR "/src/Ticketera.API"
RUN dotnet publish -c Release -o /app/published

# Etapa final: Correr la aplicación usando el Runtime liviano de .NET 9
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/published .

# Forzar el puerto correcto que configuramos en Render
ENV ASPNETCORE_URLS=http://0.0.0.0:10000

ENTRYPOINT ["dotnet", "Ticketera.API.dll"]
