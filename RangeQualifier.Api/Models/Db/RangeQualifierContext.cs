using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RangeQualifier.Api.Models.Db
{
    public partial class RangeQualifierContext : DbContext
    {
        public RangeQualifierContext()
        {
        }

        public RangeQualifierContext(DbContextOptions<RangeQualifierContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BowType> BowType { get; set; }
        public virtual DbSet<Range> Range { get; set; }
        public virtual DbSet<RangeQualification> RangeQualification { get; set; }
        public virtual DbSet<User> User { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=STEPHENOLAH\\SQLEXPRESS;Database=RangeQualifier;Trusted_Connection=True;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BowType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Range>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<RangeQualification>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.SignedDate).HasColumnType("date");

                entity.HasOne(d => d.BowType)
                    .WithMany(p => p.RangeQualification)
                    .HasForeignKey(d => d.BowTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RangeQualification_BowType");

                entity.HasOne(d => d.Range)
                    .WithMany(p => p.RangeQualification)
                    .HasForeignKey(d => d.RangeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RangeQualification_Range");

                entity.HasOne(d => d.SignedBy)
                    .WithMany(p => p.RangeQualificationSignedBy)
                    .HasForeignKey(d => d.SignedById)
                    .HasConstraintName("FK_RangeQualification_User1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RangeQualificationUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RangeQualification_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
