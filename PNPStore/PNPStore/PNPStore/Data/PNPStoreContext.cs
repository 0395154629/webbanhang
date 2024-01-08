
using Microsoft.EntityFrameworkCore;
using PNPStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PNPStore.Data
{
    public class PNPStoreContext : IdentityDbContext<Accounts>
    {
        public PNPStoreContext(DbContextOptions<PNPStoreContext> options)
            : base(options)
        {
        }

        public DbSet<PNPStore.Models.Products> Products { get; set; }
        public DbSet<PNPStore.Models.Carts> Carts { get; set; }
        public DbSet<PNPStore.Models.Categorys> Categorys { get; set; }
        public DbSet<PNPStore.Models.Orders> Orders { get; set; }
        public DbSet<PNPStore.Models.Accounts> Accounts { get; set; }
    }
}
