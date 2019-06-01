using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Forum.Web.Migrations
{
    public partial class SeedData3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IsAdmin", "IsDeleted", "Password", "Username" },
                values: new object[] { new Guid("55555555-5555-5555-5555-555555555555"), false, false, "X03MO1qnZdYdgyfeuILPmQ==", "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));
        }
    }
}
