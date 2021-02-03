﻿// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Player.Data.Migrations
{
    [DbContext(typeof(MonkeyPlayerDataContext))]
    partial class MonkeyPlayerDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ReleasedAt")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("SongInBytes")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("StyleType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Songs", "player");
                });

            modelBuilder.Entity("MonkeyPlayer.Domain.User.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Status");

                    b.ToTable("Users", "access");
                });

            modelBuilder.Entity("MonkeyPlayer.Domain.UserStatus.UserStatusReferenceEntity", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("UserStatusReferences", "access");

                    b.HasData(
                        new
                        {
                            Id = 0
                        },
                        new
                        {
                            Id = 1
                        },
                        new
                        {
                            Id = 2
                        });
                });

            modelBuilder.Entity("MonkeyPlayer.Domain.User.UserEntity", b =>
                {
                    b.HasOne("MonkeyPlayer.Domain.UserStatus.UserStatusReferenceEntity", "UserStatusReference")
                        .WithMany("Users")
                        .HasForeignKey("Status")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("UserStatusReference");
                });

            modelBuilder.Entity("MonkeyPlayer.Domain.UserStatus.UserStatusReferenceEntity", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
