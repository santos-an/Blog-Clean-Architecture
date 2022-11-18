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
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1200)", maxLength: 1200, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "CreationDate", "Title" },
                values: new object[,]
                {
                    { new Guid("04a6be26-0eeb-49e1-92c9-97b15fba1066"), "Content 5", new DateTime(2022, 11, 18, 13, 33, 36, 582, DateTimeKind.Local).AddTicks(8201), "Post 5" },
                    { new Guid("2224e45a-138f-4ccc-981f-181104027f46"), "Content 3", new DateTime(2022, 11, 18, 13, 33, 36, 582, DateTimeKind.Local).AddTicks(8186), "Post 3" },
                    { new Guid("3a152e48-b365-4114-9ca3-25afa4c1a76d"), "Content 1", new DateTime(2022, 11, 18, 13, 33, 36, 580, DateTimeKind.Local).AddTicks(1118), "Post 1" },
                    { new Guid("5a6dfb41-08e3-49da-97b6-a94e63a93bc6"), "Content 4", new DateTime(2022, 11, 18, 13, 33, 36, 582, DateTimeKind.Local).AddTicks(8198), "Post 4" },
                    { new Guid("b455c60e-08ba-4d21-8fdd-a1d9fcdbbcde"), "Content 2", new DateTime(2022, 11, 18, 13, 33, 36, 582, DateTimeKind.Local).AddTicks(8167), "Post 2" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Author", "Content", "CreationDate", "PostId" },
                values: new object[,]
                {
                    { new Guid("0007c4a2-ce35-4bce-a9b1-2c7fef8bc698"), "Author 5", "Comment 1 about post 3", new DateTime(2022, 11, 18, 13, 33, 36, 583, DateTimeKind.Local).AddTicks(425), new Guid("2224e45a-138f-4ccc-981f-181104027f46") },
                    { new Guid("2befe4a4-02e8-4a4a-a09a-0e35a6c23440"), "Author 6", "Comment 2 about post 3", new DateTime(2022, 11, 18, 13, 33, 36, 583, DateTimeKind.Local).AddTicks(430), new Guid("2224e45a-138f-4ccc-981f-181104027f46") },
                    { new Guid("37a7f095-b237-4204-89ed-363787a0cfcb"), "Author 1", "Comment 1 about post 1", new DateTime(2022, 11, 18, 13, 33, 36, 583, DateTimeKind.Local).AddTicks(149), new Guid("3a152e48-b365-4114-9ca3-25afa4c1a76d") },
                    { new Guid("5c747e1f-d2b5-4ae9-951d-40fd3b5b12b0"), "Author 3", "Comment 1 about post 2", new DateTime(2022, 11, 18, 13, 33, 36, 583, DateTimeKind.Local).AddTicks(419), new Guid("b455c60e-08ba-4d21-8fdd-a1d9fcdbbcde") },
                    { new Guid("aa85ee15-ba73-4d93-8f09-94ba0d7b0d5a"), "Author 2", "Comment 2 about post 1", new DateTime(2022, 11, 18, 13, 33, 36, 583, DateTimeKind.Local).AddTicks(410), new Guid("3a152e48-b365-4114-9ca3-25afa4c1a76d") },
                    { new Guid("e4c9f408-db16-4085-9818-4a70e11806d0"), "Author 4", "Comment 2 about post 2", new DateTime(2022, 11, 18, 13, 33, 36, 583, DateTimeKind.Local).AddTicks(422), new Guid("b455c60e-08ba-4d21-8fdd-a1d9fcdbbcde") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");
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
