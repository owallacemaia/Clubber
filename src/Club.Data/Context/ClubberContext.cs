using Club.Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Club.Data.Context
{
    public class ClubberContext : DbContext
    {
        public ClubberContext(DbContextOptions<ClubberContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostFeed> PostsFeed { get; set; }
        public DbSet<Seguidor> Seguidores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Integrante> Integrantes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClubberContext).Assembly);

            foreach (var relationship in modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(e => 
                e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }
    }
}
