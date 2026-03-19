using GestionProyectos.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionProyectos.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {            
        }

        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<Documento> Documentos { get; set; }
    }
}