﻿// <auto-generated />
using CF_Sample_Api.AppContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CF_Sample_Api.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CF_Sample_Api.Models.AuthorModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("LastName")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Author", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Email = "aijaz.ali@hotmail.com",
                            FirstName = "Aijaz",
                            LastName = "Ali"
                        });
                });

            modelBuilder.Entity("CF_Sample_Api.Models.BookModel", b =>
                {
                    b.Property<long>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("BookId"));

                    b.Property<long?>("AuthorId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("BookPrice")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("BookTitle")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Isbn")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("BookId");

                    b.HasIndex("AuthorId");

                    b.ToTable("Book", (string)null);

                    b.HasData(
                        new
                        {
                            BookId = 1L,
                            AuthorId = 3L,
                            BookPrice = 100.0m,
                            BookTitle = "Sample Book 1"
                        },
                        new
                        {
                            BookId = 2L,
                            AuthorId = 3L,
                            BookPrice = 500.41m,
                            BookTitle = "Sample Book 2"
                        });
                });

            modelBuilder.Entity("CF_Sample_Api.Models.BookModel", b =>
                {
                    b.HasOne("CF_Sample_Api.Models.AuthorModel", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("CF_Sample_Api.Models.AuthorModel", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
