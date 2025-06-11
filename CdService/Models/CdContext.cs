using Microsoft.EntityFrameworkCore;

namespace CdService.Models
{
    public class CdContext : DbContext
    {
        public CdContext(DbContextOptions<CdContext> options) : base(options)
        {
        }
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<Record> Records { get; set; } = null!;
        public DbSet<MusicGenre> MusicGenres { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => e.IdGroup).HasName("PK__Groups__32DFFDB3F74504DB");

                entity.Property(e => e.NameGroup).HasMaxLength(100);

                entity.HasOne(d => d.MusicGenre).WithMany(p => p.Groups)
                    .HasForeignKey(d => d.MusicGenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Groups_MusicalGenres");
            });

            modelBuilder.Entity<MusicGenre>(entity =>
            {
                entity.HasKey(e => e.IdMusicGenre).HasName("PK__MusicalG__C2A4358176EF3AF4");

                entity.Property(e => e.NameMusicGenre).HasMaxLength(100);
            });

            modelBuilder.Entity<Record>(entity =>
            {
                entity.HasKey(e => e.IdRecord).HasName("PK__Records__356CCF9A247285E1");

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.TitleRecord).HasMaxLength(100);

                entity.HasOne(d => d.Group).WithMany(p => p.Records)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Records_Groups");
            });

        }
    }
}
