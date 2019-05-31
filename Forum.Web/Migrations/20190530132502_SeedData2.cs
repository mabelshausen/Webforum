using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Forum.Web.Migrations
{
    public partial class SeedData2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "LikedPosts",
                columns: new[] { "UserId", "PostId" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.InsertData(
                table: "LikedPosts",
                columns: new[] { "UserId", "PostId" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("11111111-1111-1111-1111-111111111111") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LikedPosts",
                keyColumns: new[] { "UserId", "PostId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.DeleteData(
                table: "LikedPosts",
                keyColumns: new[] { "UserId", "PostId" },
                keyValues: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("11111111-1111-1111-1111-111111111111") });
        }
    }
}
