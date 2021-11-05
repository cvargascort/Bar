using Microsoft.EntityFrameworkCore;

namespace apiBar.Models
{
    class Conexion : DbContext
    {
        public Conexion(DbContextOptions<Conexion> options) : base(options)
        {

        }

        public DbSet<Persona> Persona { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<Producto> Producto { get; set; }
    }

    class Conectar
    {
        private const string cadenaConexion = "server=localhost;port=3306;database=bar;userid=root;pwd=";
        public static Conexion Crear()
        {
            var constructor = new DbContextOptionsBuilder<Conexion>();            
            constructor.UseMySQL(cadenaConexion);
            var cn = new Conexion(constructor.Options);
            return cn;
        }
    }
}
