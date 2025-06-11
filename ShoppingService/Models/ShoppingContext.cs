using Microsoft.EntityFrameworkCore;

namespace ShoppingService.Models
{
    public class ShoppingContext : DbContext
    {
        public ShoppingContext(DbContextOptions<ShoppingContext> options)
            : base(options)
        {
        }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Cart's primary key
            modelBuilder.Entity<Cart>()
                .HasKey(c => c.IdCart);

            modelBuilder.Entity<Cart>()
                    .Property(c => c.TotalPrice)
                    .HasPrecision(18, 2);

            // Configure the primary key for Order
            modelBuilder.Entity<Order>()
                    .HasKey(o => o.IdOrder);

            modelBuilder.Entity<Order>()
                    .Property(o => o.Total)
                    .HasPrecision(18, 2);

            // Configure the relationship between Orders and Cart
            modelBuilder.Entity<Order>()
                    .HasOne(o => o.Cart)
                    .WithMany()
                    .HasForeignKey(o => o.CartId)
                    .OnDelete(DeleteBehavior.NoAction); // Switch to NoAction to avoid cycles

            // Configure the CartDetail primary key
            modelBuilder.Entity<CartDetail>()
                    .HasKey(cd => cd.IdCartDetail);

            // Setting up a one-to-many relationship: Cart - CartDetail
            modelBuilder.Entity<CartDetail>()
                    .HasOne(cd => cd.Cart)
                    .WithMany(c => c.CartDetails)
                    .HasForeignKey(cd => cd.CartId)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartDetail>()
                    .Property(c => c.Price)
                    .HasPrecision(18, 2);

            // Configure the primary key for OrderDetail
            modelBuilder.Entity<OrderDetail>()
                        .HasKey(cd => cd.IdOrderDetail);

            // Setting up a one-to-many relationship: Order - OrderDetail
            modelBuilder.Entity<OrderDetail>()
                        .HasOne(cd => cd.Order)
                        .WithMany(c => c.OrderDetails)
                        .HasForeignKey(cd => cd.OrderId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                        .Property(c => c.Price)
                        .HasPrecision(18, 2);
        }
    }

}
