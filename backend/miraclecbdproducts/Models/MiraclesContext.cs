using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MiraclecBDProducts.Models
{
    public partial class MiraclesContext : DbContext
    {
        public MiraclesContext()
        {
        }

        public MiraclesContext(DbContextOptions<MiraclesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MappingOrder> MappingOrder { get; set; }
        public virtual DbSet<Setting> Setting { get; set; }
        public virtual DbSet<TblAuditLog> TblAuditLog { get; set; }
        public virtual DbSet<TblMcompany> TblMcompany { get; set; }
        public virtual DbSet<TblUser> TblUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=sql5045.site4now.net;Integrated Security=False;Database=DB_9A9CCA_shopify;User ID=DB_9A9CCA_shopify_admin;Password=Vbn*34295;MultipleActiveResultSets=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<MappingOrder>(entity =>
            {
                entity.HasKey(e => new { e.ShopifyId, e.MiraclesId });

                entity.Property(e => e.ShopifyId).HasColumnName("ShopifyID");

                entity.Property(e => e.MiraclesId).HasColumnName("MiraclesID");
            });

            modelBuilder.Entity<TblAuditLog>(entity =>
            {
                entity.ToTable("tblAuditLog");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblMcompany>(entity =>
            {
                entity.Property(e => e.ContactPerson)
                    .HasColumnName("Contact_Person")
                    .HasMaxLength(50);

                entity.Property(e => e.CurrentPassword)
                    .HasColumnName("Current_password")
                    .HasMaxLength(50);

                entity.Property(e => e.EmailAddress)
                    .HasColumnName("Email_Address")
                    .HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("Phone_Number")
                    .HasMaxLength(15);

                entity.Property(e => e.ShopUrl)
                    .HasColumnName("Shop_Url")
                    .HasMaxLength(100);

                entity.Property(e => e.Username).HasMaxLength(50);
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.UserName);

                entity.ToTable("tblUser");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Password).HasMaxLength(150);

                entity.Property(e => e.Token).HasMaxLength(500);
            });
        }
    }
}
