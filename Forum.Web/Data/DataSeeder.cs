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
                new User { Id = Guid.Parse("1"), Username = "TmpUser1", Password = "password" },
                new User { Id = Guid.Parse("2"), Username = "TmpUser2", Password = "password" },
                new User { Id = Guid.Parse("3"), Username = "TmpUser3", Password = "password" },
                new User { Id = Guid.Parse("4"), Username = "TmpUser4", Password = "password" });

            modelBuilder.Entity<Theme>().HasData(
                new Theme { Id = Guid.Parse("1"), Title = "Movies",
                    Description = "Put an interesting description here." },
                new Theme { Id = Guid.Parse("2"), Title = "Games",
                    Description = "Put an interesting description here." });

            modelBuilder.Entity<Category>().HasData(
                new
                {
                    Id = Guid.Parse("1"),
                    ThemeId = Guid.Parse("1"),
                    Title = "Star Wars",
                    Description = "Put an interesting description here."
                },
                new
                {
                    Id = Guid.Parse("2"),
                    ThemeId = Guid.Parse("1"),
                    Title = "Avengers",
                    Description = "Put an interesting description here."
                },
                new
                {
                    Id = Guid.Parse("3"),
                    ThemeId = Guid.Parse("2"),
                    Title = "Skyrim",
                    Description = "Put an interesting description here."
                });

            modelBuilder.Entity<Post>().HasData(
                new
                {
                    Id = Guid.Parse("1"),
                    UserId = Guid.Parse("1"),
                    CategoryId = Guid.Parse("1"),
                    Title = "What is your favorite Star Wars movie?",
                    Content = "Mine is Attack of the Clones.",
                    DateTime = DateTime.ParseExact("25/8/2017 14:32", "dd/MM/yyyy mm:ss", CultureInfo.InvariantCulture)
                },
                new
                {
                    Id = Guid.Parse("2"),
                    UserId = Guid.Parse("4"),
                    CategoryId = Guid.Parse("2"),
                    Title = "Tips for a beginner",
                    Content = "I just started the game and I'm completely lost. Does anyone have any tips for me?",
                    DateTime = DateTime.ParseExact("11/12/2011 17:03", "dd/MM/yyyy mm:ss", CultureInfo.InvariantCulture)
                });

            modelBuilder.Entity<Comment>().HasData(
                new
                {
                    Id = Guid.Parse("1"),
                    PostId = Guid.Parse("1"),
                    UserId = Guid.Parse("2"),
                    Content = "No one's favorite Star Wars movie is Attack of the Clones.",
                    DateTime = DateTime.ParseExact("25/8/2017 15:41", "dd/MM/yyyy mm:ss", CultureInfo.InvariantCulture)
                },
                new
                {
                    Id = Guid.Parse("2"),
                    PostId = Guid.Parse("2"),
                    UserId = Guid.Parse("3"),
                    Content = "Git gud.",
                    DateTime = DateTime.ParseExact("12/12/2011 09:13", "dd/MM/yyyy mm:ss", CultureInfo.InvariantCulture)
                });
        }
    }
}
