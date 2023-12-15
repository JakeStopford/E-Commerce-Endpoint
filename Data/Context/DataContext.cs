using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
            
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectsV13;Database=E-Commerce-Endpoint;Trusted_Connection=true;TrustServerCertificate=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderEntity>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<ProductOrderEntity>()
                .HasKey(po => new { po.OrderId, po.ProductId });

            modelBuilder.Entity<ProductOrderEntity>()
                .HasOne(po => po.Order)
                .WithMany(o => o.ProductOrders)
                .HasForeignKey(po => po.OrderId);

            modelBuilder.Entity<ProductOrderEntity>()
                .HasOne(po => po.Product)
                .WithMany()
                .HasForeignKey(po => po.ProductId);

            modelBuilder.Entity<OrderEntity>()
                .HasOne(o => o.ShippingAddress)
                .WithMany()
                .HasForeignKey(o => o.ShippingAddressId);
        }


        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<ProductOrderEntity> ProductOrders { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<ShippingAddressEntity> ShippingAddresses { get; set; }
    }
}
