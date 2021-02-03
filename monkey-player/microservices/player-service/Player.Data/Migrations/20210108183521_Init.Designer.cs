﻿// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Player.Data.Migrations
{
    [DbContext(typeof(MonkeyPlayerDataContext))]
    [Migration("20210108183521_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("MonkeyPlayer.Domain.Song.SongEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DurationInMinutes")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ReleasedAt")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("SongInBytes")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("StyleType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Songs", "player");
                });
#pragma warning restore 612, 618
        }
    }
}
