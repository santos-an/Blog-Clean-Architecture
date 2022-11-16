using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("1931e43a-843b-426f-b8fb-dd26504f345f"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("29754764-e588-422d-b401-4e80c20c6da0"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("30102fec-d70a-43f9-b531-67d23b4bf7e2"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("83aefff7-8863-4c3a-8059-7b3d79512851"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("bd8b9194-f31d-4fd6-b438-a5e2187ebf5e"));

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "CreationDate", "Title" },
                values: new object[,]
                {
                    { new Guid("105b2918-beaa-4805-8c17-f2933176d548"), "Content 2", new DateTime(2022, 11, 16, 14, 23, 50, 115, DateTimeKind.Local).AddTicks(9726), "Title 2" },
                    { new Guid("4039784d-e731-4576-9307-1e42640c078f"), "Content 1", new DateTime(2022, 11, 16, 14, 23, 50, 112, DateTimeKind.Local).AddTicks(8928), "Title 1" },
                    { new Guid("5d715555-cd59-4ed4-9e07-5b15ee958611"), "Content 4", new DateTime(2022, 11, 16, 14, 23, 50, 115, DateTimeKind.Local).AddTicks(9751), "Title 4" },
                    { new Guid("d2df2b9a-45ed-40fe-bc96-1181552fb21f"), "Content 5", new DateTime(2022, 11, 16, 14, 23, 50, 115, DateTimeKind.Local).AddTicks(9754), "Title 5" },
                    { new Guid("d8ac5fb4-9877-42d8-8548-fde274e129d8"), "Content 3", new DateTime(2022, 11, 16, 14, 23, 50, 115, DateTimeKind.Local).AddTicks(9748), "Title 3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_PostId",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("105b2918-beaa-4805-8c17-f2933176d548"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("4039784d-e731-4576-9307-1e42640c078f"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("5d715555-cd59-4ed4-9e07-5b15ee958611"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("d2df2b9a-45ed-40fe-bc96-1181552fb21f"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("d8ac5fb4-9877-42d8-8548-fde274e129d8"));

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
    }
}
