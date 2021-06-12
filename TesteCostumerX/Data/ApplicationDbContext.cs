
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TesteCostumerX.Models;

namespace TesteCostumerX.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Contato> Contato { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
