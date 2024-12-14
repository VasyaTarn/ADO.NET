using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_hw.Data
{
    internal class Data : DbContext
    {
        public DbSet<Entity.Department> Departments { get; set; }
        public DbSet<Entity.Manager> Managers { get; set; }
        public Data() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=ado_db;Integrated Security=True"
                );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entity.Manager>() 
                .HasOne(m => m.MainDep) 
                .WithMany() 
                .HasForeignKey(m => m.IdMainDep) 
                .HasPrincipalKey(d => d.Id);

            modelBuilder.Entity<Entity.Manager>() 
                .HasOne(m => m.SecDep) 
                .WithMany() 
                .HasForeignKey(m => m.IdSecDep) 
                .HasPrincipalKey(d => d.Id); 

            modelBuilder.Entity<Entity.Manager>() 
                .HasOne(m => m.Chef) 
                .WithMany() 
                .HasForeignKey(m => m.IdChief) 
                .HasPrincipalKey(m => m.Id);

            modelBuilder.Entity<Entity.Manager>().HasIndex(m => m.Login).IsUnique();

            modelBuilder.Entity<Entity.Department>().HasMany(d => d.SecManagers).WithOne().HasForeignKey(m => m.IdSecDep);
        }
    }
}
