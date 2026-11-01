using Microsoft.Data.Sqlite;

namespace ProductosApi.Database;

public static class BaseDatos
{
    // crea la tabla si no existe y agrega productos de ejemplo
    public static void Crear(string cadenaConexion)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();

        var crearTabla = @"CREATE TABLE IF NOT EXISTS Productos (
                               Id          INTEGER PRIMARY KEY AUTOINCREMENT,
                               Nombre      TEXT NOT NULL,
                               Descripcion TEXT,
                               Precio      REAL NOT NULL,
                               Stock       INTEGER NOT NULL DEFAULT 0
                           )";
        using var comando = new SqliteCommand(crearTabla, conexion);
        comando.ExecuteNonQuery();

        // si la tabla está vacía, meto 3 productos
        using var contar = new SqliteCommand("SELECT COUNT(*) FROM Productos", conexion);
        var total = Convert.ToInt32(contar.ExecuteScalar());

        if (total == 0)
        {
            var ejemplos = @"INSERT INTO Productos (Nombre, Descripcion, Precio, Stock) VALUES
                             ('Cuaderno profesional', 'Cuaderno de 100 hojas', 45.50, 30),
                             ('Lápiz HB', 'Lápiz de grafito', 6.00, 100),
                             ('Pegamento blanco', 'Frasco de 250 ml', 22.00, 15)";
            using var insertar = new SqliteCommand(ejemplos, conexion);
            insertar.ExecuteNonQuery();
        }
    }
}
