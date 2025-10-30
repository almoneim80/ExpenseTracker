using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.Models
{
    public class ExpenseTrackerDbContext:DbContext
    {
        public ExpenseTrackerDbContext(DbContextOptions options):base(options) 
        { 

        }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
