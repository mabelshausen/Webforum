﻿// <auto-generated />
using System;
using Forum.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Forum.Web.Migrations
{
    [DbContext(typeof(ForumContext))]
    [Migration("20190601184431_SeedData3")]
    partial class SeedData3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Forum.Web.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<Guid>("ThemeId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("ThemeId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111111"),
                            Description = "Put an interesting description here.",
                            IsDeleted = false,
                            ThemeId = new Guid("11111111-1111-1111-1111-111111111111"),
                            Title = "Star Wars"
                        },
                        new
                        {
                            Id = new Guid("22222222-2222-2222-2222-222222222222"),
                            Description = "Put an interesting description here.",
                            IsDeleted = false,
                            ThemeId = new Guid("11111111-1111-1111-1111-111111111111"),
                            Title = "Avengers"
                        },
                        new
                        {
                            Id = new Guid("33333333-3333-3333-3333-333333333333"),
                            Description = "Put an interesting description here.",
                            IsDeleted = false,
                            ThemeId = new Guid("22222222-2222-2222-2222-222222222222"),
                            Title = "Skyrim"
                        });
                });

            modelBuilder.Entity("Forum.Web.Entities.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("DateTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<Guid>("PostId");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");

                    b.HasData(
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111111"),
                            Content = "No one's favorite Star Wars movie is Attack of the Clones.",
                            DateTime = new DateTime(2017, 8, 25, 15, 41, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            PostId = new Guid("11111111-1111-1111-1111-111111111111"),
                            UserId = new Guid("22222222-2222-2222-2222-222222222222")
                        },
                        new
                        {
                            Id = new Guid("22222222-2222-2222-2222-222222222222"),
                            Content = "Git gud.",
                            DateTime = new DateTime(2011, 12, 12, 9, 13, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            PostId = new Guid("22222222-2222-2222-2222-222222222222"),
                            UserId = new Guid("33333333-3333-3333-3333-333333333333")
                        });
                });

            modelBuilder.Entity("Forum.Web.Entities.LikedComments", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("CommentId");

                    b.HasKey("UserId", "CommentId");

                    b.HasIndex("CommentId");

                    b.ToTable("LikedComments");
                });

            modelBuilder.Entity("Forum.Web.Entities.LikedPosts", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("PostId");

                    b.HasKey("UserId", "PostId");

                    b.HasIndex("PostId");

                    b.ToTable("LikedPosts");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("11111111-1111-1111-1111-111111111111"),
                            PostId = new Guid("11111111-1111-1111-1111-111111111111")
                        },
                        new
                        {
                            UserId = new Guid("22222222-2222-2222-2222-222222222222"),
                            PostId = new Guid("11111111-1111-1111-1111-111111111111")
                        });
                });

            modelBuilder.Entity("Forum.Web.Entities.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CategoryId");

                    b.Property<string>("Content");

                    b.Property<DateTime>("DateTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111111"),
                            CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                            Content = "Mine is Attack of the Clones.",
                            DateTime = new DateTime(2017, 8, 25, 14, 32, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Title = "What is your favorite Star Wars movie?",
                            UserId = new Guid("11111111-1111-1111-1111-111111111111")
                        },
                        new
                        {
                            Id = new Guid("22222222-2222-2222-2222-222222222222"),
                            CategoryId = new Guid("33333333-3333-3333-3333-333333333333"),
                            Content = "I just started the game and I'm completely lost. Does anyone have any tips for me?",
                            DateTime = new DateTime(2011, 12, 11, 17, 3, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Title = "Tips for a beginner",
                            UserId = new Guid("44444444-4444-4444-4444-444444444444")
                        });
                });

            modelBuilder.Entity("Forum.Web.Entities.Theme", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Themes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111111"),
                            Description = "Put an interesting description here.",
                            IsDeleted = false,
                            Title = "Movies"
                        },
                        new
                        {
                            Id = new Guid("22222222-2222-2222-2222-222222222222"),
                            Description = "Put an interesting description here.",
                            IsDeleted = false,
                            Title = "Games"
                        });
                });

            modelBuilder.Entity("Forum.Web.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsAdmin");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111111"),
                            IsAdmin = false,
                            IsDeleted = false,
                            Password = "X03MO1qnZdYdgyfeuILPmQ==",
                            Username = "TmpUser1"
                        },
                        new
                        {
                            Id = new Guid("22222222-2222-2222-2222-222222222222"),
                            IsAdmin = false,
                            IsDeleted = false,
                            Password = "X03MO1qnZdYdgyfeuILPmQ==",
                            Username = "TmpUser2"
                        },
                        new
                        {
                            Id = new Guid("33333333-3333-3333-3333-333333333333"),
                            IsAdmin = false,
                            IsDeleted = false,
                            Password = "X03MO1qnZdYdgyfeuILPmQ==",
                            Username = "TmpUser3"
                        },
                        new
                        {
                            Id = new Guid("44444444-4444-4444-4444-444444444444"),
                            IsAdmin = false,
                            IsDeleted = false,
                            Password = "X03MO1qnZdYdgyfeuILPmQ==",
                            Username = "TmpUser4"
                        },
                        new
                        {
                            Id = new Guid("55555555-5555-5555-5555-555555555555"),
                            IsAdmin = false,
                            IsDeleted = false,
                            Password = "X03MO1qnZdYdgyfeuILPmQ==",
                            Username = "Admin"
                        });
                });

            modelBuilder.Entity("Forum.Web.Entities.Category", b =>
                {
                    b.HasOne("Forum.Web.Entities.Theme", "Theme")
                        .WithMany()
                        .HasForeignKey("ThemeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Forum.Web.Entities.Comment", b =>
                {
                    b.HasOne("Forum.Web.Entities.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Forum.Web.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Forum.Web.Entities.LikedComments", b =>
                {
                    b.HasOne("Forum.Web.Entities.Comment", "Comment")
                        .WithMany("LikedComments")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Forum.Web.Entities.User", "User")
                        .WithMany("LikedComments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Forum.Web.Entities.LikedPosts", b =>
                {
                    b.HasOne("Forum.Web.Entities.Post", "Post")
                        .WithMany("LikedPosts")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Forum.Web.Entities.User", "User")
                        .WithMany("LikedPosts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Forum.Web.Entities.Post", b =>
                {
                    b.HasOne("Forum.Web.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Forum.Web.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
