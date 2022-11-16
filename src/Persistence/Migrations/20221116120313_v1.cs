using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(1200)", maxLength: 1200, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "CreationDate", "Title" },
                values: new object[,]
                {
                    { new Guid("1931e43a-843b-426f-b8fb-dd26504f345f"), "Content 4", new DateTime(2022, 11, 16, 12, 3, 13, 440, DateTimeKind.Local).AddTicks(7295), "Title 4" },
                    { new Guid("29754764-e588-422d-b401-4e80c20c6da0"), "Content 1", new DateTime(2022, 11, 16, 12, 3, 13, 437, DateTimeKind.Local).AddTicks(8818), "Title 1" },
                    { new Guid("30102fec-d70a-43f9-b531-67d23b4bf7e2"), "Content 5", new DateTime(2022, 11, 16, 12, 3, 13, 440, DateTimeKind.Local).AddTicks(7297), "Title 5" },
                    { new Guid("83aefff7-8863-4c3a-8059-7b3d79512851"), "Content 2", new DateTime(2022, 11, 16, 12, 3, 13, 440, DateTimeKind.Local).AddTicks(7260), "Title 2" },
                    { new Guid("bd8b9194-f31d-4fd6-b438-a5e2187ebf5e"), "Content 3", new DateTime(2022, 11, 16, 12, 3, 13, 440, DateTimeKind.Local).AddTicks(7291), "Title 3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
