﻿// <auto-generated />
using System;
using Cards.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cards.Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240216144856_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Cards.Infrastructure.Entities.Card", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(25)
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Colour")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("colour");

                    b.Property<Guid>("CreatedBy")
                        .HasMaxLength(25)
                        .HasColumnType("uuid")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<Guid>("DeletedBy")
                        .HasMaxLength(25)
                        .HasColumnType("uuid")
                        .HasColumnName("deleted_by");

                    b.Property<DateTime>("DeletedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_on");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("description");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("name");

                    b.Property<int>("Status")
                        .HasMaxLength(25)
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.ToTable("cards", (string)null);
                });

            modelBuilder.Entity("Cards.Infrastructure.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(25)
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("CreatedBy")
                        .HasMaxLength(25)
                        .HasColumnType("uuid")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<Guid>("DeletedBy")
                        .HasMaxLength(25)
                        .HasColumnType("uuid")
                        .HasColumnName("deleted_by");

                    b.Property<DateTime>("DeletedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_on");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("firstname");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("lastname");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("password");

                    b.Property<int>("Role")
                        .HasMaxLength(20)
                        .HasColumnType("integer")
                        .HasColumnName("role");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("salt");

                    b.HasKey("Id");

                    b.ToTable("users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("26bb5a0f-2911-4c04-b26b-4063cb12ca6e"),
                            CreatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeletedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            DeletedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "john.doe@gmail.com",
                            FirstName = "John",
                            IsActive = true,
                            IsDeleted = false,
                            LastName = "Doe",
                            Password = "MEkfFLOCi1N9sdqFbfM7eDTy7/Q=",
                            Role = 1,
                            Salt = new byte[] { 118, 23, 24, 152, 48, 146, 54, 49, 227, 195, 93, 148, 78, 34, 125, 44 }
                        },
                        new
                        {
                            Id = new Guid("6b88854d-8c61-44b7-9d1c-b27b79dd407a"),
                            CreatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeletedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            DeletedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "jane.doe@gmail.com",
                            FirstName = "Jane",
                            IsActive = true,
                            IsDeleted = false,
                            LastName = "Doe",
                            Password = "jAgJpPqr/vtHe6ENS2LkbiXkc6E=",
                            Role = 1,
                            Salt = new byte[] { 179, 237, 78, 152, 150, 51, 86, 99, 210, 12, 120, 200, 149, 131, 113, 255 }
                        },
                        new
                        {
                            Id = new Guid("59b00b0e-30a4-4daa-b485-0d845495b96f"),
                            CreatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeletedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            DeletedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "jane.doe@gmail.com",
                            FirstName = "Michael",
                            IsActive = true,
                            IsDeleted = false,
                            LastName = "Brown",
                            Password = "qzcHMYCDG8uNDLtLSh3H5PapXvc=",
                            Role = 0,
                            Salt = new byte[] { 34, 107, 43, 40, 2, 198, 171, 112, 200, 169, 66, 128, 211, 174, 29, 7 }
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
