using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DataBaseLayer
{
    public partial class TestDataBaseContext : DbContext
    {
        public TestDataBaseContext()
        {
        }

        public TestDataBaseContext(DbContextOptions<TestDataBaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("Profile");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.HasOne(d => d.Account)
                    .WithOne(p => p.Profile)
                    .HasForeignKey<Profile>(d => d.Id)
                    .HasConstraintName("FK_Profile_Account");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Profiles)
                    .HasForeignKey(d => d.IdDepartment)
                    .HasConstraintName("FK_Profile_Department");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
