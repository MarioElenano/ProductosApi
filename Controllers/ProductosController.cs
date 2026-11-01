using Microsoft.AspNetCore.Mvc;
using ProductosApi.Models;
using ProductosApi.Repositories;

namespace ProductosApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly IProductoRepository _repositorio;

    public ProductosController(IProductoRepository repositorio)
    {
        _repositorio = repositorio;
    }

    // ver todos los productos
    [HttpGet]
    public ActionResult<List<Producto>> ObtenerTodos()
    {
        return Ok(_repositorio.ObtenerTodos());
    }

    // ver un producto por id
    [HttpGet("{id:int}")]
    public ActionResult<Producto> ObtenerPorId(int id)
    {
        var producto = _repositorio.ObtenerPorId(id);
        if (producto == null)
            return NotFound($"No existe el producto con Id {id}");

        return Ok(producto);
    }

    // buscar por nombre
    [HttpGet("buscar")]
    public ActionResult<List<Producto>> Buscar([FromQuery] string nombre)
    {
        return Ok(_repositorio.Buscar(nombre));
    }

    // agregar un producto
    [HttpPost]
    public ActionResult Insertar([FromBody] Producto producto)
    {
        var nuevoId = _repositorio.Insertar(producto);
        producto.Id = nuevoId;

        return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevoId }, producto);
    }

    // actualizar un producto
    [HttpPut("{id:int}")]
    public ActionResult Actualizar(int id, [FromBody] Producto producto)
    {
        // uso el id que viene en la ruta
        producto.Id = id;

        if (!_repositorio.Actualizar(producto))
            return NotFound($"No existe el producto con Id {id}");

        return NoContent();
    }

    // eliminar un producto
    [HttpDelete("{id:int}")]
    public ActionResult Eliminar(int id)
    {
        if (!_repositorio.Eliminar(id))
            return NotFound($"No existe el producto con Id {id}");

        return NoContent();
    }
}
