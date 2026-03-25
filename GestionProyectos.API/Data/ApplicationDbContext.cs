using GestionProyectos.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionProyectos.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<AsignacionProyecto> AsignacionesProyectos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la relación entre Usuario y Proyecto a través de AsignacionProyecto
            modelBuilder.Entity<AsignacionProyecto>()
                .HasOne(ap => ap.Usuario)
                .WithMany(u => u.Asignaciones)
                .HasForeignKey(ap => ap.UsuarioId);

            modelBuilder.Entity<AsignacionProyecto>()
                .HasOne(ap => ap.Proyecto)
                .WithMany(p => p.Asignaciones)
                .HasForeignKey(ap => ap.ProyectoId);
        }
    }
}