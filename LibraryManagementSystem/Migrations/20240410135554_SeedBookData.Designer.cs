﻿// <auto-generated />
using LibraryManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LibraryManagementSystem.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240410135554_SeedBookData")]
    partial class SeedBookData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LibraryManagementSystem.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Book");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "It is a action book",
                            ISBN = "N1256FB",
                            Title = "Action Book"
                        },
                        new
                        {
                            Id = 2,
                            Description = "It is a action book",
                            ISBN = "N1256FB",
                            Title = "Action Book"
                        },
                        new
                        {
                            Id = 3,
                            Description = "It is a action book",
                            ISBN = "N1256FB",
                            Title = "Action Book"
                        },
                        new
                        {
                            Id = 4,
                            Description = "It is a action book",
                            ISBN = "N1256FB",
                            Title = "Action Book"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
