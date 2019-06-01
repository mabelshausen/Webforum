using Forum.Web.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Web.Data
{
    public class DataSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Username = "TmpUser1", Password = PasswordHasher.Hashing("password") },
                new User { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Username = "TmpUser2", Password = PasswordHasher.Hashing("password") },
                new User { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Username = "TmpUser3", Password = PasswordHasher.Hashing("password") },
                new User { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Username = "TmpUser4", Password = PasswordHasher.Hashing("password") },
                new User { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    Username = "Admin", Password = PasswordHasher.Hashing("password"), IsAdmin = true});

            modelBuilder.Entity<Theme>().HasData(
                new Theme { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Title = "Movies",
                    Description = "Put an interesting description here." },
                new Theme { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Title = "Games",
                    Description = "Put an interesting description here." });

            modelBuilder.Entity<Category>().HasData(
                new
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    ThemeId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Title = "Star Wars",
                    Description = "Put an interesting description here.",
                    IsDeleted = false
                },
                new
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    ThemeId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Title = "Avengers",
                    Description = "Put an interesting description here.",
                    IsDeleted = false
                },
                new
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    ThemeId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Title = "Skyrim",
                    Description = "Put an interesting description here.",
                    IsDeleted = false
                });

            modelBuilder.Entity<Post>().HasData(
                new
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    UserId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Title = "What is your favorite Star Wars movie?",
                    Content = "Mine is Attack of the Clones.",
                    DateTime = DateTime.ParseExact("25/08/2017 14:32", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                    IsDeleted = false
                },
                new
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    UserId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    CategoryId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Title = "Tips for a beginner",
                    Content = "I just started the game and I'm completely lost. Does anyone have any tips for me?",
                    DateTime = DateTime.ParseExact("11/12/2011 17:03", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                    IsDeleted = false
                });

            modelBuilder.Entity<Comment>().HasData(
                new
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    PostId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    UserId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Content = "No one's favorite Star Wars movie is Attack of the Clones.",
                    DateTime = DateTime.ParseExact("25/08/2017 15:41", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                    IsDeleted = false
                },
                new
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    PostId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    UserId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Content = "Git gud.",
                    DateTime = DateTime.ParseExact("12/12/2011 09:13", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                    IsDeleted = false
                });

            modelBuilder.Entity<LikedPosts>().HasData(
                new LikedPosts { UserId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    PostId = Guid.Parse("11111111-1111-1111-1111-111111111111") },
                new LikedPosts { UserId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    PostId = Guid.Parse("11111111-1111-1111-1111-111111111111")
                });
        }
    }
}
