using account_service.Models;
using Microsoft.EntityFrameworkCore;

namespace account_service.Repository;

public class ArtistBlockDbContext: DbContext {
        public ArtistBlockDbContext(DbContextOptions<ArtistBlockDbContext> options): base(options) { }
        public DbSet<Painter> Painters { get; set; }
        public DbSet<RegisteredUser> RegisteredUsers { get; set; }
        public DbSet<AuthUser> AuthUsers { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<PainterSpeciality> PainterSpecialities { get; set; }
        public DbSet<GanGeneratedImage> GanGeneratedImages { get; set; }
        public DbSet<Painting> Paintings { get; set; }
        public DbSet<PaintingReview> PaintingReview { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
}