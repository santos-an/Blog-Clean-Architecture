using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    { new Guid("0c12c646-30df-4595-9fe4-b59611161072"), "Content 1", new DateTime(2022, 11, 16, 14, 26, 59, 678, DateTimeKind.Local).AddTicks(7714), "Post 1" },
                    { new Guid("6502aadc-0400-4236-abe6-e0a0fa46cc08"), "Content 2", new DateTime(2022, 11, 16, 14, 26, 59, 681, DateTimeKind.Local).AddTicks(7142), "Post 2" },
                    { new Guid("6f15f67b-32df-4b06-b034-17a96896406a"), "Content 5", new DateTime(2022, 11, 16, 14, 26, 59, 681, DateTimeKind.Local).AddTicks(7168), "Post 5" },
                    { new Guid("a4c9eb18-4809-4290-b473-b4d3a3677b46"), "Content 4", new DateTime(2022, 11, 16, 14, 26, 59, 681, DateTimeKind.Local).AddTicks(7166), "Post 4" },
                    { new Guid("bd5fa502-61e9-4638-b7b5-6fa89797c6e5"), "Content 3", new DateTime(2022, 11, 16, 14, 26, 59, 681, DateTimeKind.Local).AddTicks(7163), "Post 3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("0c12c646-30df-4595-9fe4-b59611161072"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("6502aadc-0400-4236-abe6-e0a0fa46cc08"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("6f15f67b-32df-4b06-b034-17a96896406a"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("a4c9eb18-4809-4290-b473-b4d3a3677b46"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("bd5fa502-61e9-4638-b7b5-6fa89797c6e5"));

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
        }
    }
}
