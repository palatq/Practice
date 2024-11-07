using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<WebApplication1.Models.Equipment> Equipments { get; set; } = default!;
        public DbSet<Adjustment> Adjustments { get; set; }
        public DbSet<WebApplication1.Models.Worker> Worker { get; set; } = default!;
    }

}
