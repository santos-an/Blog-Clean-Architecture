using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Comments",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Comments",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "CreationDate", "Title" },
                values: new object[,]
                {
                    { new Guid("052fd34a-1073-4f8c-acbc-0186334ae88c"), "Content 4", new DateTime(2022, 11, 16, 14, 34, 57, 522, DateTimeKind.Local).AddTicks(3187), "Post 4" },
                    { new Guid("18461bb0-d8a4-425a-97d7-cc0338462c1c"), "Content 1", new DateTime(2022, 11, 16, 14, 34, 57, 519, DateTimeKind.Local).AddTicks(4875), "Post 1" },
                    { new Guid("2ce103ca-87f2-4b7b-b51d-aadb2d203ed7"), "Content 2", new DateTime(2022, 11, 16, 14, 34, 57, 522, DateTimeKind.Local).AddTicks(3164), "Post 2" },
                    { new Guid("4775adb5-c2dc-479d-ab20-f1cd24e38769"), "Content 3", new DateTime(2022, 11, 16, 14, 34, 57, 522, DateTimeKind.Local).AddTicks(3184), "Post 3" },
                    { new Guid("71e333ec-a5b5-42ac-a147-c9b6c5daa4c2"), "Content 5", new DateTime(2022, 11, 16, 14, 34, 57, 522, DateTimeKind.Local).AddTicks(3190), "Post 5" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Author", "Content", "CreationDate", "PostId" },
                values: new object[,]
                {
                    { new Guid("114e2d7e-c71c-4a2b-bd5e-9807895a3ea5"), "Author 5", "Comment 1 about post 3", new DateTime(2022, 11, 16, 14, 34, 57, 522, DateTimeKind.Local).AddTicks(5504), new Guid("4775adb5-c2dc-479d-ab20-f1cd24e38769") },
                    { new Guid("8785cc95-5c34-4379-919f-ec6366135de8"), "Author 6", "Comment 2 about post 3", new DateTime(2022, 11, 16, 14, 34, 57, 522, DateTimeKind.Local).AddTicks(5509), new Guid("4775adb5-c2dc-479d-ab20-f1cd24e38769") },
                    { new Guid("8d0cfc25-936b-4217-b3a2-89063f9b788c"), "Author 2", "Comment 2 about post 1", new DateTime(2022, 11, 16, 14, 34, 57, 522, DateTimeKind.Local).AddTicks(5483), new Guid("18461bb0-d8a4-425a-97d7-cc0338462c1c") },
                    { new Guid("8de1b74f-a5d4-4859-96da-51f0e5919118"), "Author 3", "Comment 1 about post 2", new DateTime(2022, 11, 16, 14, 34, 57, 522, DateTimeKind.Local).AddTicks(5492), new Guid("2ce103ca-87f2-4b7b-b51d-aadb2d203ed7") },
                    { new Guid("9791b85b-97a3-43bf-944b-289f4bbd5757"), "Author 4", "Comment 2 about post 2", new DateTime(2022, 11, 16, 14, 34, 57, 522, DateTimeKind.Local).AddTicks(5502), new Guid("2ce103ca-87f2-4b7b-b51d-aadb2d203ed7") },
                    { new Guid("b8759465-565d-4e4f-9904-48a44fec4bc1"), "Author 1", "Comment 1 about post 1", new DateTime(2022, 11, 16, 14, 34, 57, 522, DateTimeKind.Local).AddTicks(5212), new Guid("18461bb0-d8a4-425a-97d7-cc0338462c1c") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("114e2d7e-c71c-4a2b-bd5e-9807895a3ea5"));

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("8785cc95-5c34-4379-919f-ec6366135de8"));

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("8d0cfc25-936b-4217-b3a2-89063f9b788c"));

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("8de1b74f-a5d4-4859-96da-51f0e5919118"));

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("9791b85b-97a3-43bf-944b-289f4bbd5757"));

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("b8759465-565d-4e4f-9904-48a44fec4bc1"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("052fd34a-1073-4f8c-acbc-0186334ae88c"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("71e333ec-a5b5-42ac-a147-c9b6c5daa4c2"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("18461bb0-d8a4-425a-97d7-cc0338462c1c"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("2ce103ca-87f2-4b7b-b51d-aadb2d203ed7"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("4775adb5-c2dc-479d-ab20-f1cd24e38769"));

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(120)",
                oldMaxLength: 120,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

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
    }
}
