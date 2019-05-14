using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Forum.Web.Migrations
{
    public partial class SeedData1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Themes",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Put an interesting description here.", "Movies" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Put an interesting description here.", "Games" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Username" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "password", "TmpUser1" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "password", "TmpUser2" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "password", "TmpUser3" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "password", "TmpUser4" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "ThemeId", "Title" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "Put an interesting description here.", new Guid("11111111-1111-1111-1111-111111111111"), "Star Wars" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "ThemeId", "Title" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), "Put an interesting description here.", new Guid("11111111-1111-1111-1111-111111111111"), "Avengers" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "ThemeId", "Title" },
                values: new object[] { new Guid("33333333-3333-3333-3333-333333333333"), "Put an interesting description here.", new Guid("22222222-2222-2222-2222-222222222222"), "Skyrim" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CategoryId", "Content", "DateTime", "Title", "UserId" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "Mine is Attack of the Clones.", new DateTime(2017, 8, 25, 14, 32, 0, 0, DateTimeKind.Unspecified), "What is your favorite Star Wars movie?", new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CategoryId", "Content", "DateTime", "Title", "UserId" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("22222222-2222-2222-2222-222222222222"), "I just started the game and I'm completely lost. Does anyone have any tips for me?", new DateTime(2011, 12, 11, 17, 3, 0, 0, DateTimeKind.Unspecified), "Tips for a beginner", new Guid("44444444-4444-4444-4444-444444444444") });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "DateTime", "PostId", "UserId" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "No one's favorite Star Wars movie is Attack of the Clones.", new DateTime(2017, 8, 25, 15, 41, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("22222222-2222-2222-2222-222222222222") });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "DateTime", "PostId", "UserId" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), "Git gud.", new DateTime(2011, 12, 12, 9, 13, 0, 0, DateTimeKind.Unspecified), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("33333333-3333-3333-3333-333333333333") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Themes",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Themes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));
        }
    }
}
