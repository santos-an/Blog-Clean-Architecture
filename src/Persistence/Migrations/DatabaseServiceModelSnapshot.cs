﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence.Database;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(BlogContext))]
    partial class DatabaseServiceModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-preview.5.22302.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Entities.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("Comments");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e6bb680f-92da-4f3e-8da2-0559c945c055"),
                            Author = "Author 1",
                            Content = "Comment 1 about post 1",
                            CreationDate = new DateTime(2022, 11, 17, 18, 51, 58, 243, DateTimeKind.Local).AddTicks(1500),
                            PostId = new Guid("7af6a5ee-bdf3-4f16-b250-ad1831301485")
                        },
                        new
                        {
                            Id = new Guid("86a34dee-9e13-41b9-9ca7-bead904867e4"),
                            Author = "Author 2",
                            Content = "Comment 2 about post 1",
                            CreationDate = new DateTime(2022, 11, 17, 18, 51, 58, 243, DateTimeKind.Local).AddTicks(1762),
                            PostId = new Guid("7af6a5ee-bdf3-4f16-b250-ad1831301485")
                        },
                        new
                        {
                            Id = new Guid("4a226aa0-7aa6-4a53-aca9-887d676f7e77"),
                            Author = "Author 3",
                            Content = "Comment 1 about post 2",
                            CreationDate = new DateTime(2022, 11, 17, 18, 51, 58, 243, DateTimeKind.Local).AddTicks(1779),
                            PostId = new Guid("1cb8444d-cde0-4d43-8d7f-b444a7602261")
                        },
                        new
                        {
                            Id = new Guid("fc31aa2e-482a-4bc3-9b69-4aacedff8488"),
                            Author = "Author 4",
                            Content = "Comment 2 about post 2",
                            CreationDate = new DateTime(2022, 11, 17, 18, 51, 58, 243, DateTimeKind.Local).AddTicks(1788),
                            PostId = new Guid("1cb8444d-cde0-4d43-8d7f-b444a7602261")
                        },
                        new
                        {
                            Id = new Guid("d786da69-d957-4307-bb52-f7c7eb31cdbc"),
                            Author = "Author 5",
                            Content = "Comment 1 about post 3",
                            CreationDate = new DateTime(2022, 11, 17, 18, 51, 58, 243, DateTimeKind.Local).AddTicks(1791),
                            PostId = new Guid("947a1661-beb6-4bbb-9089-741aa764dd8f")
                        },
                        new
                        {
                            Id = new Guid("8fca6783-bc1a-4d9f-9fc1-c59088ce5a6b"),
                            Author = "Author 6",
                            Content = "Comment 2 about post 3",
                            CreationDate = new DateTime(2022, 11, 17, 18, 51, 58, 243, DateTimeKind.Local).AddTicks(1795),
                            PostId = new Guid("947a1661-beb6-4bbb-9089-741aa764dd8f")
                        });
                });

            modelBuilder.Entity("Domain.Entities.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1200)
                        .HasColumnType("nvarchar(1200)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7af6a5ee-bdf3-4f16-b250-ad1831301485"),
                            Content = "Content 1",
                            CreationDate = new DateTime(2022, 11, 17, 18, 51, 58, 240, DateTimeKind.Local).AddTicks(1580),
                            Title = "Post 1"
                        },
                        new
                        {
                            Id = new Guid("1cb8444d-cde0-4d43-8d7f-b444a7602261"),
                            Content = "Content 2",
                            CreationDate = new DateTime(2022, 11, 17, 18, 51, 58, 242, DateTimeKind.Local).AddTicks(9617),
                            Title = "Post 2"
                        },
                        new
                        {
                            Id = new Guid("947a1661-beb6-4bbb-9089-741aa764dd8f"),
                            Content = "Content 3",
                            CreationDate = new DateTime(2022, 11, 17, 18, 51, 58, 242, DateTimeKind.Local).AddTicks(9636),
                            Title = "Post 3"
                        },
                        new
                        {
                            Id = new Guid("c89b3e40-e61d-4945-88fb-aaeefcbcb20f"),
                            Content = "Content 4",
                            CreationDate = new DateTime(2022, 11, 17, 18, 51, 58, 242, DateTimeKind.Local).AddTicks(9639),
                            Title = "Post 4"
                        },
                        new
                        {
                            Id = new Guid("7ec5638e-7684-4164-998a-4c9d0e6a1759"),
                            Content = "Content 5",
                            CreationDate = new DateTime(2022, 11, 17, 18, 51, 58, 242, DateTimeKind.Local).AddTicks(9641),
                            Title = "Post 5"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Comment", b =>
                {
                    b.HasOne("Domain.Entities.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Domain.Entities.Post", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
