using GestaoConsultasUVV.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoConsultasUVV.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Estas propriedades representam as tabelas no banco de dados
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
    }
}