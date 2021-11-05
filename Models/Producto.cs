using System;
using System.ComponentModel.DataAnnotations;

namespace apiBar.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public int Cantidad { get; set; }
        public Int64 Precio { get; set; }
    }
}
