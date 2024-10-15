using Microsoft.EntityFrameworkCore;

namespace SpendSmart.Models
{
    public class SpendSmartDbContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; } //All info is saved into context here 

        public SpendSmartDbContext(DbContextOptions<SpendSmartDbContext> options) : base(options)
        {

        }
    }
}
