using examen.Models;
using Microsoft.EntityFrameworkCore;

namespace examen.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario>? Usuarios { get; set; }
        public DbSet<Equipo>? Equipos { get; set; }
        public DbSet<Tecnico>? Tecnicos { get; set; }
        public DbSet<Reparacion>? Reparaciones { get; set; }
        public DbSet<DetalleReparacion>? DetallesReparacion { get; set; }
        public DbSet<Asignacion>? Asignaciones { get; set; }
    }
}
