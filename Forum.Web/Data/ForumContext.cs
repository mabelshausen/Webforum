using System;
using Forum.Web.Data;
using Forum.Web.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Forum.Web
{
    public partial class ForumContext : DbContext
    {
        public ForumContext(DbContextOptions<ForumContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LikedPosts>()
                .ToTable("LikedPosts")
                .HasKey(lp => new { lp.UserId, lp.PostId });

            modelBuilder.Entity<LikedPosts>()
                .HasOne(lp => lp.User)
                .WithMany(u => u.LikedPosts)
                .HasForeignKey(lp => lp.UserId);

            modelBuilder.Entity<LikedPosts>()
                .HasOne(lp => lp.Post)
                .WithMany(p => p.LikedPosts)
                .HasForeignKey(lp => lp.PostId);

            modelBuilder.Entity<LikedComments>()
                .ToTable("LikedComments")
                .HasKey(lc => new { lc.UserId, lc.CommentId });

            modelBuilder.Entity<LikedComments>()
                .HasOne(lc => lc.User)
                .WithMany(u => u.LikedComments)
                .HasForeignKey(lc => lc.UserId);

            modelBuilder.Entity<LikedComments>()
                .HasOne(lc => lc.Comment)
                .WithMany(c => c.LikedComments)
                .HasForeignKey(lc => lc.CommentId);

            DataSeeder.Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
