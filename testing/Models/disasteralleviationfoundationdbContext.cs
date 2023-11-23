using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace testing.Models
{
    public partial class disasteralleviationfoundationdbContext : DbContext
    {
        public disasteralleviationfoundationdbContext()
        {
        }

        public disasteralleviationfoundationdbContext(DbContextOptions<disasteralleviationfoundationdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActiveDisaster> ActiveDisasters { get; set; }
        public virtual DbSet<AllocationOfGood> AllocationOfGoods { get; set; }
        public virtual DbSet<AllocationOfMoney> AllocationOfMoneys { get; set; }
        public virtual DbSet<AuthorisedUser> AuthorisedUsers { get; set; }
        public virtual DbSet<DonationOfGood> DonationOfGoods { get; set; }
        public virtual DbSet<DonationOfGoodsCategory> DonationOfGoodsCategories { get; set; }
        public virtual DbSet<DonationsOfMoney> DonationsOfMoneys { get; set; }
        public virtual DbSet<PurchasesOfGood> PurchasesOfGoods { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:disasteralleviationfoundation-serverst.database.windows.net,1433;Initial Catalog=disasteralleviationfoundation-db;Persist Security Info=False;User ID=st10127986;Password=Cardcook14;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ActiveDisaster>(entity =>
            {
                entity.HasKey(e => e.DisasterId)
                    .HasName("PK__ActiveDi__B487740E8CFF9131");

                entity.Property(e => e.DisasterId).ValueGeneratedNever();

                entity.Property(e => e.DisasterAidTypeWanted)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DisasterDescription)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.DisasterEndDate).HasColumnType("datetime");

                entity.Property(e => e.DisasterLocation)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DisasterStartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<AllocationOfGood>(entity =>
            {
                entity.HasKey(e => e.AllocationId)
                    .HasName("PK__Allocati__B3C6D64BEF0FE6A7");

                entity.Property(e => e.AllocationId).ValueGeneratedNever();

                entity.Property(e => e.DateOfAllocation).HasColumnType("datetime");

                entity.Property(e => e.DonationOfGoodsCategoryId).HasColumnName("DonationOfGoodsCategoryID");

                entity.HasOne(d => d.Disaster)
                    .WithMany(p => p.AllocationOfGoods)
                    .HasForeignKey(d => d.DisasterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Allocatio__Disas__72C60C4A");

                entity.HasOne(d => d.DonationOfGoodsCategory)
                    .WithMany(p => p.AllocationOfGoods)
                    .HasForeignKey(d => d.DonationOfGoodsCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Allocatio__Donat__71D1E811");
            });

            modelBuilder.Entity<AllocationOfMoney>(entity =>
            {
                entity.HasKey(e => e.AllocationId)
                    .HasName("PK__Allocati__B3C6D64BBEB23B01");

                entity.ToTable("AllocationOfMoney");

                entity.Property(e => e.AllocationId).ValueGeneratedNever();

                entity.Property(e => e.DateOfAllocation).HasColumnType("datetime");

                entity.HasOne(d => d.Disaster)
                    .WithMany(p => p.AllocationOfMoneys)
                    .HasForeignKey(d => d.DisasterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Allocatio__Disas__75A278F5");
            });

            modelBuilder.Entity<AuthorisedUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Authoris__CB9A1CFF3EF736A0");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("userId");

                entity.Property(e => e.UserNames)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("user_names");

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("user_password");
            });

            modelBuilder.Entity<DonationOfGood>(entity =>
            {
                entity.HasKey(e => e.DonationId)
                    .HasName("PK__Donation__C5082EFB3256748D");

                entity.Property(e => e.DonationId).ValueGeneratedNever();

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.DonationDate).HasColumnType("datetime");

                entity.Property(e => e.DonationDescription)
                    .IsRequired()
                    .HasMaxLength(800);

                entity.Property(e => e.DonationDonor)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.DonationOfGoods)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DonationO__categ__628FA481");
            });

            modelBuilder.Entity<DonationOfGoodsCategory>(entity =>
            {
                entity.HasKey(e => e.DonationCategoryId)
                    .HasName("PK__Donation__4EA6D65E62F14EE5");

                entity.ToTable("DonationOfGoodsCategory");

                entity.Property(e => e.DonationCategoryId).ValueGeneratedNever();

                entity.Property(e => e.DonationCategoryName).HasMaxLength(50);
            });

            modelBuilder.Entity<DonationsOfMoney>(entity =>
            {
                entity.HasKey(e => e.DonationId)
                    .HasName("PK__Donation__F7F4F4135D8AF1F5");

                entity.ToTable("DonationsOfMoney");

                entity.Property(e => e.DonationId)
                    .ValueGeneratedNever()
                    .HasColumnName("donationId");

                entity.Property(e => e.DonatedDate).HasColumnType("date");

                entity.Property(e => e.DonationDonor)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<PurchasesOfGood>(entity =>
            {
                entity.HasKey(e => e.PurchaseId)
                    .HasName("PK__Purchase__6B0A6BBE0038B372");

                entity.Property(e => e.PurchaseId).ValueGeneratedNever();

                entity.Property(e => e.DateOfPurchase).HasColumnType("datetime");

                entity.Property(e => e.DonationOfGoodsCategoryId).HasColumnName("DonationOfGoodsCategoryID");

                entity.HasOne(d => d.Disaster)
                    .WithMany(p => p.PurchasesOfGoods)
                    .HasForeignKey(d => d.DisasterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Purchases__Disas__6D0D32F4");

                entity.HasOne(d => d.DonationOfGoodsCategory)
                    .WithMany(p => p.PurchasesOfGoods)
                    .HasForeignKey(d => d.DonationOfGoodsCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Purchases__Donat__6C190EBB");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
