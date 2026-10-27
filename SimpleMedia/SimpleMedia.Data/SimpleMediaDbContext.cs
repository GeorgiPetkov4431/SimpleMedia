using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleMedia.Data.Models;

namespace SimpleMedia.Data
{
    public class SimpleMediaDbContext : IdentityDbContext
    {
        public SimpleMediaDbContext(DbContextOptions<SimpleMediaDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Post> Posts { get; set; } = null!;

        public virtual DbSet<Comment> Comments { get; set; } = null!;

        public virtual DbSet<PostLike> PostLikes { get; set; } = null!;

        public virtual DbSet<Tag> Tags { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SimpleMediaDbContext).Assembly);
        }
    }
}
