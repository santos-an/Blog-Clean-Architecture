using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "Title",
                table: "Posts",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Posts",
                type: "nvarchar(1200)",
                maxLength: 1200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1200)",
                oldMaxLength: 1200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Comments",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(120)",
                oldMaxLength: 120,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Comments",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "CreationDate", "Title" },
                values: new object[,]
                {
                    { new Guid("1cb8444d-cde0-4d43-8d7f-b444a7602261"), "Content 2", new DateTime(2022, 11, 17, 18, 51, 58, 242, DateTimeKind.Local).AddTicks(9617), "Post 2" },
                    { new Guid("7af6a5ee-bdf3-4f16-b250-ad1831301485"), "Content 1", new DateTime(2022, 11, 17, 18, 51, 58, 240, DateTimeKind.Local).AddTicks(1580), "Post 1" },
                    { new Guid("7ec5638e-7684-4164-998a-4c9d0e6a1759"), "Content 5", new DateTime(2022, 11, 17, 18, 51, 58, 242, DateTimeKind.Local).AddTicks(9641), "Post 5" },
                    { new Guid("947a1661-beb6-4bbb-9089-741aa764dd8f"), "Content 3", new DateTime(2022, 11, 17, 18, 51, 58, 242, DateTimeKind.Local).AddTicks(9636), "Post 3" },
                    { new Guid("c89b3e40-e61d-4945-88fb-aaeefcbcb20f"), "Content 4", new DateTime(2022, 11, 17, 18, 51, 58, 242, DateTimeKind.Local).AddTicks(9639), "Post 4" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Author", "Content", "CreationDate", "PostId" },
                values: new object[,]
                {
                    { new Guid("4a226aa0-7aa6-4a53-aca9-887d676f7e77"), "Author 3", "Comment 1 about post 2", new DateTime(2022, 11, 17, 18, 51, 58, 243, DateTimeKind.Local).AddTicks(1779), new Guid("1cb8444d-cde0-4d43-8d7f-b444a7602261") },
                    { new Guid("86a34dee-9e13-41b9-9ca7-bead904867e4"), "Author 2", "Comment 2 about post 1", new DateTime(2022, 11, 17, 18, 51, 58, 243, DateTimeKind.Local).AddTicks(1762), new Guid("7af6a5ee-bdf3-4f16-b250-ad1831301485") },
                    { new Guid("8fca6783-bc1a-4d9f-9fc1-c59088ce5a6b"), "Author 6", "Comment 2 about post 3", new DateTime(2022, 11, 17, 18, 51, 58, 243, DateTimeKind.Local).AddTicks(1795), new Guid("947a1661-beb6-4bbb-9089-741aa764dd8f") },
                    { new Guid("d786da69-d957-4307-bb52-f7c7eb31cdbc"), "Author 5", "Comment 1 about post 3", new DateTime(2022, 11, 17, 18, 51, 58, 243, DateTimeKind.Local).AddTicks(1791), new Guid("947a1661-beb6-4bbb-9089-741aa764dd8f") },
                    { new Guid("e6bb680f-92da-4f3e-8da2-0559c945c055"), "Author 1", "Comment 1 about post 1", new DateTime(2022, 11, 17, 18, 51, 58, 243, DateTimeKind.Local).AddTicks(1500), new Guid("7af6a5ee-bdf3-4f16-b250-ad1831301485") },
                    { new Guid("fc31aa2e-482a-4bc3-9b69-4aacedff8488"), "Author 4", "Comment 2 about post 2", new DateTime(2022, 11, 17, 18, 51, 58, 243, DateTimeKind.Local).AddTicks(1788), new Guid("1cb8444d-cde0-4d43-8d7f-b444a7602261") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("4a226aa0-7aa6-4a53-aca9-887d676f7e77"));

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("86a34dee-9e13-41b9-9ca7-bead904867e4"));

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("8fca6783-bc1a-4d9f-9fc1-c59088ce5a6b"));

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("d786da69-d957-4307-bb52-f7c7eb31cdbc"));

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("e6bb680f-92da-4f3e-8da2-0559c945c055"));

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("fc31aa2e-482a-4bc3-9b69-4aacedff8488"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("7ec5638e-7684-4164-998a-4c9d0e6a1759"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("c89b3e40-e61d-4945-88fb-aaeefcbcb20f"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("1cb8444d-cde0-4d43-8d7f-b444a7602261"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("7af6a5ee-bdf3-4f16-b250-ad1831301485"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("947a1661-beb6-4bbb-9089-741aa764dd8f"));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Posts",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Posts",
                type: "nvarchar(1200)",
                maxLength: 1200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1200)",
                oldMaxLength: 1200);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Comments",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(120)",
                oldMaxLength: 120);

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Comments",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

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
    }
}
