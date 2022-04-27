using Microsoft.EntityFrameworkCore;
using SimpleSchool.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSchool.DAL
{
    internal class AppDbContext : DbContext
    {
        public DbSet<Building> Building { get; set; }
        public DbSet<Room> Room { get; set; }

        public AppDbContext() : base()
        {

        }

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(SettingsManager.GetConnectionString());
        }
    }
}
