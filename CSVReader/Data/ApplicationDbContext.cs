using CSVReader.Models;
using Microsoft.EntityFrameworkCore;

namespace CSVReader.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext( DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Persons");
                entity.Property(p => p.Name).IsRequired().HasMaxLength(256);
                entity.Property(p => p.Phone).IsRequired().HasMaxLength(32);
                entity.Property(p => p.Salary).IsRequired().HasMaxLength(16);
                entity.Property(p => p.IsMarried).IsRequired();
                entity.Property(p => p.DateOfBirth).IsRequired();
            });

        }
        public DbSet<Person> Persons { get; set; }

    }
}
