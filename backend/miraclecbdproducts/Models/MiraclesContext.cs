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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

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

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.HasKey(e => e.AutoSyncProduct)
                    .HasName("PK__Setting__4E2DDB0B94EEB60D");
            });

            modelBuilder.Entity<TblAuditLog>(entity =>
            {
                entity.ToTable("tblAuditLog");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            });
        }
    }
}
