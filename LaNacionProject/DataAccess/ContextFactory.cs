using LaNacionProject.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace LaNacionProject.DataAccess
{
    public class ContextFactory : DbContext
    {
        public ContextFactory(DbContextOptions<ContextFactory> options) : base(options)  {  }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contact>().HasIndex(c => c.Email).IsUnique();
            modelBuilder.Entity<PhoneNumber>().HasIndex(p => p.Number).IsUnique();

            modelBuilder.Entity<Contact>()
                .HasOne(c => c.Address)
                .WithOne()
                .HasForeignKey<Address>(a => a.ContactId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Contact>()
                .HasMany(c => c.PhoneNumber)
                .WithOne()
                .HasForeignKey(p => p.ContactId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
    }
}
