using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace EtlTool
{
    public class EtlToolDbContext : DbContext
    {
        private readonly string _connectionString;

        public EtlToolDbContext()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            this._connectionString = configuration.GetConnectionString("DefaultConnection");
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
