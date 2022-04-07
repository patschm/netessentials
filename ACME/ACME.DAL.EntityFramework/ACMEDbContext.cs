using ACME.Entities;
using Microsoft.EntityFrameworkCore;

namespace ACME.DAL.EntityFramework
{
    public class ACMEDbContext : DbContext
    {
        public ACMEDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Person>? People { get; set; }
        public DbSet<Hobby>? Hobbies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Hobby>().Property(h=>h.Description).HasMaxLength(255);
            modelBuilder.Entity<PersonHobby>()
                .HasKey(ph => new { ph.PersonId, ph.HobbyId});
        }
    }
}