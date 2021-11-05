using System;
using System.ComponentModel.DataAnnotations;

namespace apiBar.Models
{
    public class Login
    {
        [Key]
        public Int64 Id { get; set; }
        public Int64 Documento { get; set; }
        public string Contrasena { get; set; }
        public string Token { get; set; }
        public DateTime Fecha_ingreso { get; set; }
        public DateTime Fecha_actualizacion { get; set; }
    }
}
