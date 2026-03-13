# API de Marcas de Autos

API REST en ASP.NET Core 9 con Entity Framework Core y PostgreSQL.

La aplicacion expone un endpoint para consultar marcas de autos, aplica migraciones al iniciar y siembra datos iniciales de referencia.

## Requisitos

- Docker Desktop
- .NET 9 SDK

## Levantar con Docker Compose

Esta es la forma recomendada de ejecutar el proyecto completo.

```powershell
docker compose up -d --build
```

Servicios expuestos:

- API: `http://localhost:8080`
- PostgreSQL: `localhost:5432`

Endpoint principal:

- `GET http://localhost:8080/api/marcasautos`

Para detener el entorno:

```powershell
docker compose down
```

Para detenerlo y borrar tambien los datos persistidos de PostgreSQL:

```powershell
docker compose down -v
```

## Ejecutar localmente

Si quieres correr la API fuera de Docker pero seguir usando PostgreSQL en contenedor:

1. Levanta solo la base de datos:

```powershell
docker compose up -d postgres
```

2. Restaura herramientas locales:

```powershell
dotnet tool restore
```

3. Ejecuta la API:

```powershell
dotnet run --project .\Autos.Api
```

Por defecto, en desarrollo la API queda disponible en:

- `http://localhost:5073/api/marcasautos`

## Pruebas

Ejecutar pruebas:

```powershell
dotnet test test-dotnet.slnx
```

Medir cobertura:

```powershell
dotnet test test-dotnet.slnx --settings coverlet.runsettings --collect:"XPlat Code Coverage" --results-directory .\TestResults
```

## Notas de funcionamiento

- La API ejecuta `MigrateAsync()` al iniciar.
- El seed inicial carga estas marcas si no existen:
  - `Toyota`
  - `Tesla`
  - `Porsche`
- El endpoint principal devuelve `total` y `data`.
