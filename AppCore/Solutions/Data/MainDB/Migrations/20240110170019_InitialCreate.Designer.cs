﻿// <auto-generated />
using System;
using Common.Solutions.Data.MainDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Common.Plugins.DataAccess.Migrations
{
    [DbContext(typeof(MainDB))]
    [Migration("20240110170019_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Common.Sockets.DataModel.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PostId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PostId");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            PostId = 1,
                            Content = "Content1",
                            CreatedAt = new DateTime(2023, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Title1"
                        },
                        new
                        {
                            PostId = 2,
                            Content = "Content2",
                            CreatedAt = new DateTime(2023, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Title2"
                        },
                        new
                        {
                            PostId = 3,
                            Content = "Content3",
                            CreatedAt = new DateTime(2023, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Title3"
                        });
                });

            modelBuilder.Entity("Common.Sockets.DataModel.PostTag", b =>
                {
                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("PostId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("PostTags");

                    b.HasData(
                        new
                        {
                            PostId = 1,
                            TagId = 1
                        },
                        new
                        {
                            PostId = 1,
                            TagId = 2
                        },
                        new
                        {
                            PostId = 2,
                            TagId = 1
                        });
                });

            modelBuilder.Entity("Common.Sockets.DataModel.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TagId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TagId");

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            TagId = 1,
                            Name = "Tag1"
                        },
                        new
                        {
                            TagId = 2,
                            Name = "Tag2"
                        });
                });

            modelBuilder.Entity("Common.Sockets.DataModel.PostTag", b =>
                {
                    b.HasOne("Common.Sockets.DataModel.Post", "Post")
                        .WithMany("PostTags")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Common.Sockets.DataModel.Tag", "Tag")
                        .WithMany("PostTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("Common.Sockets.DataModel.Post", b =>
                {
                    b.Navigation("PostTags");
                });

            modelBuilder.Entity("Common.Sockets.DataModel.Tag", b =>
                {
                    b.Navigation("PostTags");
                });
#pragma warning restore 612, 618
        }
    }
}