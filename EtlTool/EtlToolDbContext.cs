using System.Data.Entity;

namespace EtlTool
{
    public class EtlToolDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
    }
}
