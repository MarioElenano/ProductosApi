using Microsoft.Data.Sqlite;
using ProductosApi.Models;

namespace ProductosApi.Repositories;

public class ProductoRepository : IProductoRepository
{
    private readonly string _cadenaConexion;

    public ProductoRepository(string cadenaConexion)
    {
        _cadenaConexion = cadenaConexion;
    }

    // conexión a la base de datos SQLite
    private SqliteConnection CrearConexion()
    {
        return new SqliteConnection(_cadenaConexion);
    }

    // trae todos los productos
    public List<Producto> ObtenerTodos()
    {
        var lista = new List<Producto>();

        using var conexion = CrearConexion();
        conexion.Open();

        var sql = "SELECT Id, Nombre, Descripcion, Precio, Stock FROM Productos";
        using var comando = new SqliteCommand(sql, conexion);
        using var lector = comando.ExecuteReader();

        // leo cada fila y la agrego a la lista
        while (lector.Read())
        {
            lista.Add(Leer(lector));
        }

        return lista;
    }

    // trae un producto por su id
    public Producto? ObtenerPorId(int id)
    {
        using var conexion = CrearConexion();
        conexion.Open();

        var sql = "SELECT Id, Nombre, Descripcion, Precio, Stock FROM Productos WHERE Id = @Id";
        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.AddWithValue("@Id", id);

        using var lector = comando.ExecuteReader();

        // si no lo encuentra regresa null
        return lector.Read() ? Leer(lector) : null;
    }

    // guarda un producto nuevo
    public int Insertar(Producto producto)
    {
        using var conexion = CrearConexion();
        conexion.Open();

        var sql = @"INSERT INTO Productos (Nombre, Descripcion, Precio, Stock)
                    VALUES (@Nombre, @Descripcion, @Precio, @Stock);
                    SELECT last_insert_rowid();";
        using var comando = new SqliteCommand(sql, conexion);
        AgregarParametros(comando, producto);

        // regresa el id que se creó (last_insert_rowid)
        return Convert.ToInt32(comando.ExecuteScalar());
    }

    // cambia los datos de un producto
    public bool Actualizar(Producto producto)
    {
        using var conexion = CrearConexion();
        conexion.Open();

        var sql = @"UPDATE Productos
                    SET Nombre = @Nombre, Descripcion = @Descripcion,
                        Precio = @Precio, Stock = @Stock
                    WHERE Id = @Id";
        using var comando = new SqliteCommand(sql, conexion);
        AgregarParametros(comando, producto);
        comando.Parameters.AddWithValue("@Id", producto.Id);

        // true si sí cambió una fila
        return comando.ExecuteNonQuery() > 0;
    }

    // borra un producto
    public bool Eliminar(int id)
    {
        using var conexion = CrearConexion();
        conexion.Open();

        var sql = "DELETE FROM Productos WHERE Id = @Id";
        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.AddWithValue("@Id", id);

        return comando.ExecuteNonQuery() > 0;
    }

    // busca productos por nombre
    public List<Producto> Buscar(string texto)
    {
        var lista = new List<Producto>();

        using var conexion = CrearConexion();
        conexion.Open();

        var sql = @"SELECT Id, Nombre, Descripcion, Precio, Stock
                    FROM Productos
                    WHERE Nombre LIKE @Texto";
        using var comando = new SqliteCommand(sql, conexion);

        // los % sirven para buscar el texto en cualquier parte del nombre
        comando.Parameters.AddWithValue("@Texto", "%" + texto + "%");

        using var lector = comando.ExecuteReader();
        while (lector.Read())
        {
            lista.Add(Leer(lector));
        }

        return lista;
    }

    // convierte una fila en un Producto
    private static Producto Leer(SqliteDataReader lector)
    {
        return new Producto
        {
            Id = lector.GetInt32(0),
            Nombre = lector.GetString(1),
            Descripcion = lector.IsDBNull(2) ? null : lector.GetString(2),
            Precio = lector.GetDecimal(3),
            Stock = lector.GetInt32(4)
        };
    }

    // pone los datos del producto en el comando
    private static void AgregarParametros(SqliteCommand comando, Producto producto)
    {
        comando.Parameters.AddWithValue("@Nombre", producto.Nombre);
        comando.Parameters.AddWithValue("@Descripcion", (object?)producto.Descripcion ?? DBNull.Value);
        comando.Parameters.AddWithValue("@Precio", producto.Precio);
        comando.Parameters.AddWithValue("@Stock", producto.Stock);
    }
}
