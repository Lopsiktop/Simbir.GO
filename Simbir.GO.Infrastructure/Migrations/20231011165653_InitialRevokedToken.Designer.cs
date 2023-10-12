﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Simbir.GO.Infrastructure.Persistence;

#nullable disable

namespace Simbir.GO.Infrastructure.Migrations
{
    [DbContext(typeof(SimbirDbContext))]
    [Migration("20231011165653_InitialRevokedToken")]
    partial class InitialRevokedToken
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Simbir.GO.Domain.AccountEntity.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Balance")
                        .HasColumnType("double precision");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Simbir.GO.Domain.AccountEntity.RevokedToken", b =>
                {
                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasIndex("Token")
                        .IsUnique();

                    b.ToTable("RevokedTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
