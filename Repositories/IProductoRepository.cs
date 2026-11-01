using ProductosApi.Models;

namespace ProductosApi.Repositories;

// los métodos que debe tener el repositorio
public interface IProductoRepository
{
    List<Producto> ObtenerTodos();
    Producto? ObtenerPorId(int id);
    int Insertar(Producto producto);
    bool Actualizar(Producto producto);
    bool Eliminar(int id);
    List<Producto> Buscar(string texto);
}
