# Marcas API

Esta pequeña solución muestra cómo utilizar en el backend:
- **C#**: Lenguaje de programación .NET
- **Entity Framework Core**: ORM para .NET con PostgreSQL
- **XUnit**: Framework de pruebas unitarias
- **Docker**: Contenedorización de la aplicación
- **PostgreSQL**: Base de datos relacional

## Estructura del Proyecto

```
marcas-api/
├── src/
│   └── MarcasApi/           # Proyecto API principal
│       ├── Controllers/     # Controladores de la API
│       ├── Data/           # DbContext y configuración de EF
│       └── Models/         # Modelos de dominio
├── tests/
│   └── MarcasApi.Tests/    # Proyecto de pruebas unitarias con XUnit
├── Dockerfile              # Definición del contenedor Docker
└── docker-compose.yml      # Orquestación de servicios
```

## Características

- API RESTful para gestión de marcas (brands)
- CRUD completo (Create, Read, Update, Delete)
- Entity Framework Core con PostgreSQL
- Pruebas unitarias con XUnit
- Contenedorización con Docker
- Base de datos PostgreSQL en contenedor

## Requisitos Previos

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Git](https://git-scm.com/)

## Instalación y Ejecución

### Opción 1: Con Docker Compose (Recomendado)

1. Clone el repositorio:
```bash
git clone https://github.com/fernando-gallegos/marcas-api.git
cd marcas-api
```

2. Ejecute los servicios con Docker Compose:
```bash
docker-compose up -d
```

La API estará disponible en: `http://localhost:8080`

### Opción 2: Ejecución Local

1. Clone el repositorio:
```bash
git clone https://github.com/fernando-gallegos/marcas-api.git
cd marcas-api
```

2. Inicie PostgreSQL (con Docker):
```bash
docker run -d \
  --name postgres-marcas \
  -e POSTGRES_PASSWORD=postgres \
  -e POSTGRES_DB=marcasdb \
  -p 5432:5432 \
  postgres:16
```

3. Restaure las dependencias:
```bash
dotnet restore
```

4. Ejecute las migraciones de Entity Framework:
```bash
cd src/MarcasApi
dotnet ef migrations add InitialCreate
dotnet ef database update
```

5. Ejecute la aplicación:
```bash
dotnet run --project src/MarcasApi
```

La API estará disponible en: `https://localhost:7000` o `http://localhost:5000`

## Ejecutar Pruebas

Para ejecutar las pruebas unitarias con XUnit:

```bash
dotnet test
```

Para ejecutar con cobertura detallada:

```bash
dotnet test --verbosity normal
```

## Endpoints de la API

### Obtener todas las marcas
```http
GET /api/marcas
```

### Obtener una marca por ID
```http
GET /api/marcas/{id}
```

### Crear una nueva marca
```http
POST /api/marcas
Content-Type: application/json

{
  "nombre": "Nombre de la Marca",
  "descripcion": "Descripción opcional"
}
```

### Actualizar una marca
```http
PUT /api/marcas/{id}
Content-Type: application/json

{
  "id": 1,
  "nombre": "Nombre Actualizado",
  "descripcion": "Descripción actualizada"
}
```

### Eliminar una marca
```http
DELETE /api/marcas/{id}
```

## Ejemplos con cURL

### Crear una marca:
```bash
curl -X POST http://localhost:8080/api/marcas \
  -H "Content-Type: application/json" \
  -d '{"nombre":"Nike","descripcion":"Marca deportiva"}'
```

### Obtener todas las marcas:
```bash
curl http://localhost:8080/api/marcas
```

### Obtener una marca específica:
```bash
curl http://localhost:8080/api/marcas/1
```

## Tecnologías Utilizadas

- **C# 12**: Lenguaje de programación
- **.NET 9.0**: Framework de desarrollo
- **ASP.NET Core**: Framework web
- **Entity Framework Core 9.0**: ORM
- **Npgsql.EntityFrameworkCore.PostgreSQL**: Proveedor EF Core para PostgreSQL
- **XUnit**: Framework de testing
- **Moq**: Librería de mocking para tests
- **PostgreSQL 16**: Base de datos
- **Docker**: Plataforma de contenedorización

## Comandos Útiles de Docker

### Ver logs de la API:
```bash
docker-compose logs -f api
```

### Ver logs de PostgreSQL:
```bash
docker-compose logs -f postgres
```

### Detener los servicios:
```bash
docker-compose down
```

### Detener y eliminar volúmenes (limpieza completa):
```bash
docker-compose down -v
```

### Reconstruir la imagen:
```bash
docker-compose up -d --build
```

## Entity Framework Core - Migraciones

Si necesita crear migraciones manualmente:

```bash
# Crear una nueva migración
dotnet ef migrations add NombreDeLaMigracion --project src/MarcasApi

# Aplicar migraciones a la base de datos
dotnet ef database update --project src/MarcasApi

# Revertir la última migración
dotnet ef migrations remove --project src/MarcasApi
```

## Licencia

Este proyecto es de código abierto y está disponible bajo la licencia MIT.