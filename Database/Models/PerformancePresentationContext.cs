using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Database.Models
{
    public partial class PerformancePresentationContext : DbContext
    {
        public virtual DbSet<Websites> Websites { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Config.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Websites>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.IsDone)
                    .IsRequired()
                    .HasColumnType("bit");
            });
        }
    }
}