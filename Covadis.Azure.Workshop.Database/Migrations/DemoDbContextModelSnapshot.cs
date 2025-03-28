﻿// <auto-generated />
using Covadis.Azure.Database;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Covadis.Azure.Workshop.Database.Migrations;

[DbContext(typeof(DemoDbContext))]
partial class DemoDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.2")
            .HasAnnotation("Relational:MaxIdentifierLength", 128);

        SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

        modelBuilder.Entity("Covadis.Azure.Workshop.Database.Models.Order", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("ArticleNumber")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar(50)");

                b.Property<bool>("IsPaid")
                    .HasColumnType("bit");

                b.Property<bool>("IsShipped")
                    .HasColumnType("bit");

                b.Property<int>("Quantity")
                    .HasColumnType("int");

                b.Property<decimal>("TotalPrice")
                    .HasColumnType("decimal(18, 2)");

                b.HasKey("Id");

                b.ToTable("Orders");
            });
#pragma warning restore 612, 618
    }
}
