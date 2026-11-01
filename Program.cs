using ProductosApi.Database;
using ProductosApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// saco la conexión del appsettings
var cadenaConexion = builder.Configuration.GetConnectionString("Conexion")
    ?? throw new InvalidOperationException("Falta la cadena de conexión 'Conexion'");

// crea la base de datos y la tabla si no existen
BaseDatos.Crear(cadenaConexion);

// registro el repositorio para usarlo en el controlador
builder.Services.AddScoped<IProductoRepository>(_ => new ProductoRepository(cadenaConexion));

builder.Services.AddControllers();

// swagger para probar
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
