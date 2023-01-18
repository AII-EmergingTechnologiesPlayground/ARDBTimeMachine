using DbTimeMachine.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTimeMachine.API
{
    public class DbTimeMachineContext : DbContext
    {
        public DbTimeMachineContext(DbContextOptions<DbTimeMachineContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Server=localhost;User=root;Password=;Database=DbTimeMachine", ServerVersion.Parse("10.4.24-mariadb"));
            }
            
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Account>().HasKey(x => x.Id);
        }
    }
}
