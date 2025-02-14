﻿// <auto-generated />
using System;
using Logbook.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Logbook.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Logbook.Entities.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Logbook.Entities.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UtcCreatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Logbook.Entities.GroupTeacher", b =>
                {
                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GroupId", "TeacherId");

                    b.HasIndex("TeacherId");

                    b.ToTable("GroupTeachers");
                });

            modelBuilder.Entity("Logbook.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FatherName")
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(60)
                        .IsUnicode(false)
                        .HasColumnType("varchar(60)");

                    b.Property<string>("Role")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateOnly>("UtcBornAt")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("UtcLastSeenAt")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("FirstName");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.HasIndex("Role");

                    b.ToTable("Users");

                    b.HasDiscriminator().HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Logbook.Entities.Student", b =>
                {
                    b.HasBaseType("Logbook.Entities.User");

                    b.Property<int>("Coins")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("Diamonds")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("Logbook.Entities.Teacher", b =>
                {
                    b.HasBaseType("Logbook.Entities.User");

                    b.HasDiscriminator().HasValue("Teacher");
                });

            modelBuilder.Entity("Logbook.Entities.Group", b =>
                {
                    b.HasOne("Logbook.Entities.Department", "Department")
                        .WithMany("Groups")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Logbook.Entities.GroupTeacher", b =>
                {
                    b.HasOne("Logbook.Entities.Group", "Group")
                        .WithMany("GroupTeachers")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Logbook.Entities.Teacher", "Teacher")
                        .WithMany("GroupTeachers")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("Logbook.Entities.Department", b =>
                {
                    b.Navigation("Groups");
                });

            modelBuilder.Entity("Logbook.Entities.Group", b =>
                {
                    b.Navigation("GroupTeachers");
                });

            modelBuilder.Entity("Logbook.Entities.Teacher", b =>
                {
                    b.Navigation("GroupTeachers");
                });
#pragma warning restore 612, 618
        }
    }
}
