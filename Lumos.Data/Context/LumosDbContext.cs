using Lumos.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumos.Data.Context
{
    public class LumosDbContext : DbContext
    {
        public LumosDbContext()
        {
            
        }
        public LumosDbContext(DbContextOptions<LumosDbContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Station> Stations { get; set; }
        public DbSet<InverterData> InverterDatas { get; set; }
    }
}
