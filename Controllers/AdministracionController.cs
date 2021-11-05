using apiBar.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace apiBar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdministracionController : Controller
    {
        private Conexion dbConexion;        
        List<Persona> ListaPersonas = new List<Persona>();        

        public AdministracionController()
        {
            dbConexion = Conectar.Crear();
        }

        [HttpGet]
        [Route("ConsultarUsuarios/{documento}")]
        public ActionResult MetodoGet(Int64 documento)
        {
            var persona = dbConexion.Persona.SingleOrDefault(x => x.Documento == documento);

            if (persona != null)
            {
                if (persona.Rol == 1)
                {
                    return Ok(dbConexion.Persona.ToArray());
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
