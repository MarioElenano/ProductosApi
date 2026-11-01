# ProductosApi

Backend en ASP.NET Core (.NET 10) con CRUD de productos usando SQLite y Swagger.

## Cómo ejecutarlo
1. Ejecutar `dotnet run`.
2. Abrir `http://localhost:<puerto>/swagger`.

La base de datos (`productos.db`) y la tabla se crean solas al iniciar.

## Endpoints
| Método | Ruta | Descripción |
|---|---|---|
| GET | /api/productos | Consulta todos |
| GET | /api/productos/{id} | Consulta por Id |
| GET | /api/productos/buscar?nombre= | Buscar por nombre |
| POST | /api/productos | Insertar |
| PUT | /api/productos/{id} | Actualizar |
| DELETE | /api/productos/{id} | Eliminar |
