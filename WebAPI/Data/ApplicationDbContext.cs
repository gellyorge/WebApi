using Microsoft.EntityFrameworkCore;
using WebAPI.Model;  // Ajuste o namespace conforme necessário

namespace WebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; } 
    }
}
