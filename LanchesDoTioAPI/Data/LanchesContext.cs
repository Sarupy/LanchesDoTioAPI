using LanchesDoTioAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LanchesDoTioAPI.Data
{
    public class LanchesContext : DbContext
    {

        public LanchesContext(DbContextOptions<LanchesContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customer { get; set; } = default!;
        public DbSet<Meal> Meal { get; set; } = default!;
        public DbSet<Order> Order { get; set; } = default!;
    }
}
