using apiBar.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiBar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : Controller
    {
        private Conexion dbConexion;
        List<Persona> ListaPersonas = new List<Persona>();

        public ProductoController()
        {
            dbConexion = Conectar.Crear();
        }

        [HttpGet]
        [Route("ConsultarProductos/{documento}")]
        public ActionResult ConsultarProductos(Int64 documento)
        {
            var persona = dbConexion.Persona.SingleOrDefault(x => x.Documento == documento);

            if (persona != null)
            {
                if (persona.Rol == 1 || persona.Rol == 2)
                {

                    return Ok(dbConexion.Producto.Where(x => x.Cantidad > 0).OrderBy(x => x.Precio).ToArray());
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("RegistrarProducto")]
        public ActionResult RegistrarProducto([FromBody] Producto producto)
        {
            if (ModelState.IsValid)
            {
                dbConexion.Producto.Add(producto);
                dbConexion.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("ConsultarProductosVencer/{documento}")]
        public ActionResult ConsultarProductosVencer(Int64 documento)
        {
            var persona = dbConexion.Persona.SingleOrDefault(x => x.Documento == documento);

            if (persona != null)
            {
                if (persona.Rol == 1 || persona.Rol == 2)
                {

                    return Ok(dbConexion.Producto.Where(x => x.Cantidad < 6).OrderBy(x => x.Cantidad).ToArray());
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("ConsultarTipoProducto/{Tipo}/{documento}")]
        public ActionResult ConsultarTipoProducto(string Tipo, Int64 documento)
        {
            var persona = dbConexion.Persona.SingleOrDefault(x => x.Documento == documento);

            if (persona != null)
            {
                if (persona.Rol == 1 || persona.Rol == 2)
                {

                    return Ok(dbConexion.Producto.Where(x => x.Cantidad > 0 && x.Tipo == Tipo).OrderBy(x => x.Cantidad).ToArray());
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}
