using Microsoft.EntityFrameworkCore;

namespace UserService.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Email).HasName("PK__Users__A9D10535B2F51717");
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Password).HasMaxLength(500);
                entity.Property(e => e.Role).HasMaxLength(50);
            });
        }
    }
   
}
