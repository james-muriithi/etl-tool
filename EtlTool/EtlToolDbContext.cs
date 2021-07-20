using Microsoft.EntityFrameworkCore;

namespace EtlTool
{
    public class EtlToolDbContext : DbContext
    {
        private readonly string _connectionString;

        public EtlToolDbContext()
        {
            this._connectionString = "server=localhost;port=3306;database=etl_tool;uid=root;password=";
        }

        public EtlToolDbContext(string connectionString)
        {
            this._connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }
}
