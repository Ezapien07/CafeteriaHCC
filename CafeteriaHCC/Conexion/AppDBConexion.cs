using CafeteriaHCC.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeteriaHCC.Conexion
{
    public class AppDBConexion : DbContext 
    {
        public AppDBConexion(DbContextOptions<AppDBConexion> options)
           : base(options)
        {
        }
        public DbSet<Almacen> Almacenes { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Mesas> Mesas { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<CatEstatusOrden> CatEstatusOrdenes { get; set; }
        public DbSet<OrdenDetalle> OrdenDetalles { get; set; }  
    }
}
