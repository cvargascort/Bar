using apiBar.Logic;
using apiBar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiBar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private Conexion dbConexion;
        //public LoginLogic loginLogic;

        public LoginController()
        {
            dbConexion = Conectar.Crear();
        }
        
        [HttpGet]
        [Route("PruebaGet")]
        public ActionResult MetodoGet()
        {
            return Ok("Alguna respuesta exitosa");
        }

        [HttpPost]
        [Route("Registrar")]
        public ActionResult Registrar([FromBody] Persona persona)
        {
            if (ModelState.IsValid)
            {
                dbConexion.Persona.Add(persona);
                dbConexion.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("GenerarContrasena")]
        public ActionResult GenerarContrasena([FromBody] Login login)
        {
            var persona = dbConexion.Persona.SingleOrDefault(x => x.Documento == login.Documento);

            if (persona != null)
            {
                login.Fecha_ingreso = DateTime.Now;
                login.Fecha_actualizacion = DateTime.Now;                
                login.Token = "0";

                LoginLogic loginLogic = new LoginLogic();                                
                login.Contrasena = loginLogic.Encrypt(login.Contrasena);

                if (ModelState.IsValid)
                {
                    dbConexion.Login.Add(login);
                    dbConexion.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login([FromBody] Login login)
        {
            LoginLogic loginLogic = new LoginLogic();
            var personaLogin = dbConexion.Login.SingleOrDefault(x => x.Documento == login.Documento);

            if (personaLogin != null && personaLogin.Contrasena == loginLogic.Encrypt(login.Contrasena))
            {
                personaLogin.Fecha_actualizacion = DateTime.Now;
                personaLogin.Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

                if (ModelState.IsValid)
                {                                        
                    dbConexion.SaveChanges();
                    return Ok(new { Token = personaLogin.Token });
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [Route("Logout")]
        public ActionResult Logout([FromBody] Login login)
        {
            LoginLogic loginLogic = new LoginLogic();            
            var personaLogin = dbConexion.Login.SingleOrDefault(x => x.Documento == login.Documento);

            if (personaLogin != null)
            {
                personaLogin.Fecha_actualizacion = DateTime.Now;
                personaLogin.Token = "0";

                dbConexion.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
