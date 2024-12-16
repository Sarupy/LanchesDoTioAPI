using LanchesDoTioAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LanchesDoTioAPI.Data
{
    public class LanchesContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public LanchesContext(DbContextOptions<LanchesContext> options) : base(options)
        {
        }
        public DbSet<LanchesDoTioAPI.Models.Meal> Meal { get; set; } = default!;
        public DbSet<LanchesDoTioAPI.Models.Order> Order { get; set; } = default!;

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //	//builder.Entity<Contato>().HasKey(m => m.Id);
        //	base.OnModelCreating(builder);
        //}
    }
}
