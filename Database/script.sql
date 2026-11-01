-- tabla de productos (SQLite)
-- la API ya la crea sola al iniciar, este script es solo de referencia
CREATE TABLE IF NOT EXISTS Productos (
    Id          INTEGER PRIMARY KEY AUTOINCREMENT,
    Nombre      TEXT NOT NULL,
    Descripcion TEXT,
    Precio      REAL NOT NULL,
    Stock       INTEGER NOT NULL DEFAULT 0
);

-- productos de ejemplo
INSERT INTO Productos (Nombre, Descripcion, Precio, Stock) VALUES
('Cuaderno profesional', 'Cuaderno de 100 hojas', 45.50, 30),
('Lápiz HB', 'Lápiz de grafito', 6.00, 100),
('Pegamento blanco', 'Frasco de 250 ml', 22.00, 15);
