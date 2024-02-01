
using AppDataWorker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.Intrinsics.X86;
using static System.Net.Mime.MediaTypeNames;

namespace AppDataWorker.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = EINSTEIN; Initial Catalog = AppDB; User ID = sa; Password = Uabo9674; Connect Timeout = 30; Encrypt = False; Trust Server Certificate = False; Application Intent = ReadWrite; Multi Subnet Failover = False");
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Apteka> Apteks { get; set; }
        public DbSet<Operating_mode> operating_Modes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Analog> Analogs { get; set; }
    }
}
