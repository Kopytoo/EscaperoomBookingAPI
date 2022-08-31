﻿// <auto-generated />
using System;
using EscaperoomBookingAPI.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EscaperoomBookingAPI.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220831072834_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0-preview.7.22376.2");

            modelBuilder.Entity("EscaperoomBookingAPI.Core.Domain.Entities.Master.BookingDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("NumberOfPeople")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SelectedRoom")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("SummaryReference")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("VisitDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SummaryReference")
                        .IsUnique();

                    b.ToTable("BookingDetails");
                });

            modelBuilder.Entity("EscaperoomBookingAPI.Core.Domain.Entities.Master.CustomerDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("OtherInfo")
                        .HasColumnType("TEXT");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("SummaryReference")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SummaryReference")
                        .IsUnique();

                    b.ToTable("CustomerDetails");
                });

            modelBuilder.Entity("EscaperoomBookingAPI.Core.Domain.Entities.Master.Summary", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("BookingVariant")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Price")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Summary");
                });

            modelBuilder.Entity("EscaperoomBookingAPI.Core.Domain.Entities.Master.BookingDetails", b =>
                {
                    b.HasOne("EscaperoomBookingAPI.Core.Domain.Entities.Master.Summary", "Summary")
                        .WithOne("BookingDetails")
                        .HasForeignKey("EscaperoomBookingAPI.Core.Domain.Entities.Master.BookingDetails", "SummaryReference")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Summary");
                });

            modelBuilder.Entity("EscaperoomBookingAPI.Core.Domain.Entities.Master.CustomerDetails", b =>
                {
                    b.HasOne("EscaperoomBookingAPI.Core.Domain.Entities.Master.Summary", "Summary")
                        .WithOne("CustomerDetails")
                        .HasForeignKey("EscaperoomBookingAPI.Core.Domain.Entities.Master.CustomerDetails", "SummaryReference")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Summary");
                });

            modelBuilder.Entity("EscaperoomBookingAPI.Core.Domain.Entities.Master.Summary", b =>
                {
                    b.Navigation("BookingDetails")
                        .IsRequired();

                    b.Navigation("CustomerDetails")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}