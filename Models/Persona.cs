using System;
using System.ComponentModel.DataAnnotations;

namespace apiBar.Models
{
    public class Persona
    {
        [Key]
        public Int64 Documento { get; set; }
        public int Rol { get; set; }
        public string Nombre { get; set; }
        public Int64 Telefono { get; set; }
        public string Correo { get; set; }
    }
}
