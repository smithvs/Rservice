using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RService.Models;

namespace RService.Data
{
    public partial class RServiceContext : DbContext
    {
        public RServiceContext()
        {
        }
        public RServiceContext (DbContextOptions<RServiceContext> options)
            : base(options)
        {
        }

        public DbSet<RService.Models.OfficeType> OfficeType { get; set; } = default!;

        public DbSet<RService.Models.Office>? Office { get; set; }

        public DbSet<RService.Models.Service>? Service { get; set; }

        public DbSet<RService.Models.ServiceOffice>? ServiceOffice { get; set; }

        public DbSet<RService.Models.Specialist>? Specialist { get; set; }

        public DbSet<RService.Models.SpecialistService>? SpecialistService { get; set; }

        public DbSet<RService.Models.Record>? Record { get; set; }

        public DbSet<RService.Models.Client>? Client { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RService.Data;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
