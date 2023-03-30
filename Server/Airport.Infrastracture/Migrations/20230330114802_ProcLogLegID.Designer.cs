﻿// <auto-generated />
using System;
using Airport.Infrastracture;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Airport.Infrastracture.Migrations
{
    [DbContext(typeof(AirportDataContext))]
    [Migration("20230330114802_ProcLogLegID")]
    partial class ProcLogLegID
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Core.Entities.Flight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeparture")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PilotId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PilotId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("Core.Entities.Leg", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CurrentLeg")
                        .HasColumnType("int");

                    b.Property<int>("LegType")
                        .HasColumnType("int");

                    b.Property<int>("NextPosibbleLegs")
                        .HasColumnType("int");

                    b.Property<int>("PauseTime")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Legs");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CurrentLeg = 1,
                            LegType = 2,
                            NextPosibbleLegs = 2,
                            PauseTime = 3
                        },
                        new
                        {
                            Id = 2,
                            CurrentLeg = 2,
                            LegType = 4,
                            NextPosibbleLegs = 4,
                            PauseTime = 4
                        },
                        new
                        {
                            Id = 3,
                            CurrentLeg = 4,
                            LegType = 4,
                            NextPosibbleLegs = 8,
                            PauseTime = 5
                        },
                        new
                        {
                            Id = 4,
                            CurrentLeg = 8,
                            LegType = 12,
                            NextPosibbleLegs = 272,
                            PauseTime = 2
                        },
                        new
                        {
                            Id = 5,
                            CurrentLeg = 16,
                            LegType = 4,
                            NextPosibbleLegs = 96,
                            PauseTime = 3
                        },
                        new
                        {
                            Id = 6,
                            CurrentLeg = 32,
                            LegType = 1,
                            NextPosibbleLegs = 128,
                            PauseTime = 4
                        },
                        new
                        {
                            Id = 7,
                            CurrentLeg = 64,
                            LegType = 1,
                            NextPosibbleLegs = 128,
                            PauseTime = 4
                        },
                        new
                        {
                            Id = 8,
                            CurrentLeg = 128,
                            LegType = 4,
                            NextPosibbleLegs = 8,
                            PauseTime = 5
                        });
                });

            modelBuilder.Entity("Core.Entities.Pilot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pilots");
                });

            modelBuilder.Entity("Core.Entities.ProcessLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("EnterTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ExitTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("FlightId")
                        .HasColumnType("int");

                    b.Property<int>("LegId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FlightId");

                    b.ToTable("ProcessLogger");
                });

            modelBuilder.Entity("Core.Entities.Flight", b =>
                {
                    b.HasOne("Core.Entities.Pilot", "Pilot")
                        .WithMany()
                        .HasForeignKey("PilotId");

                    b.Navigation("Pilot");
                });

            modelBuilder.Entity("Core.Entities.ProcessLog", b =>
                {
                    b.HasOne("Core.Entities.Flight", "Flight")
                        .WithMany("ProcessLog")
                        .HasForeignKey("FlightId");

                    b.Navigation("Flight");
                });

            modelBuilder.Entity("Core.Entities.Flight", b =>
                {
                    b.Navigation("ProcessLog");
                });
#pragma warning restore 612, 618
        }
    }
}
