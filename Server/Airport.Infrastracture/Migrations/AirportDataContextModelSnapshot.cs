﻿// <auto-generated />
using System;
using Airport.Infrastracture;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Airport.Infrastracture.Migrations
{
    [DbContext(typeof(AirportDataContext))]
    partial class AirportDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Core.Entities.ForFlight.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("Core.Entities.ForFlight.Pilot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rank")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Pilots");
                });

            modelBuilder.Entity("Core.Entities.ForFlight.Plane", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PassangerCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Plane");
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

                    b.Property<string>("LegNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FlightId");

                    b.ToTable("ProcessLogger");
                });

            modelBuilder.Entity("Core.Entities.Terminal.Flight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid?>("Code")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeparture")
                        .HasColumnType("bit");

                    b.Property<int?>("PilotId")
                        .HasColumnType("int");

                    b.Property<int?>("PlaneId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PilotId");

                    b.HasIndex("PlaneId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("Core.Entities.Terminal.Leg", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CurrentLeg")
                        .HasColumnType("int");

                    b.Property<int?>("FlightId")
                        .HasColumnType("int");

                    b.Property<bool>("IsOccupied")
                        .HasColumnType("bit");

                    b.Property<int>("LegType")
                        .HasColumnType("int");

                    b.Property<int>("NextPosibbleLegs")
                        .HasColumnType("int");

                    b.Property<int>("PauseTime")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FlightId");

                    b.ToTable("Legs");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CurrentLeg = 1,
                            IsOccupied = false,
                            LegType = 2,
                            NextPosibbleLegs = 2,
                            PauseTime = 3
                        },
                        new
                        {
                            Id = 2,
                            CurrentLeg = 2,
                            IsOccupied = false,
                            LegType = 4,
                            NextPosibbleLegs = 4,
                            PauseTime = 4
                        },
                        new
                        {
                            Id = 3,
                            CurrentLeg = 4,
                            IsOccupied = false,
                            LegType = 4,
                            NextPosibbleLegs = 8,
                            PauseTime = 5
                        },
                        new
                        {
                            Id = 4,
                            CurrentLeg = 8,
                            IsOccupied = false,
                            LegType = 12,
                            NextPosibbleLegs = 272,
                            PauseTime = 2
                        },
                        new
                        {
                            Id = 5,
                            CurrentLeg = 16,
                            IsOccupied = false,
                            LegType = 4,
                            NextPosibbleLegs = 96,
                            PauseTime = 3
                        },
                        new
                        {
                            Id = 6,
                            CurrentLeg = 32,
                            IsOccupied = false,
                            LegType = 1,
                            NextPosibbleLegs = 128,
                            PauseTime = 4
                        },
                        new
                        {
                            Id = 7,
                            CurrentLeg = 64,
                            IsOccupied = false,
                            LegType = 1,
                            NextPosibbleLegs = 128,
                            PauseTime = 4
                        },
                        new
                        {
                            Id = 8,
                            CurrentLeg = 128,
                            IsOccupied = false,
                            LegType = 4,
                            NextPosibbleLegs = 8,
                            PauseTime = 5
                        });
                });

            modelBuilder.Entity("Core.Entities.ForFlight.Plane", b =>
                {
                    b.HasOne("Core.Entities.ForFlight.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Core.Entities.ProcessLog", b =>
                {
                    b.HasOne("Core.Entities.Terminal.Flight", "Flight")
                        .WithMany("ProcessLog")
                        .HasForeignKey("FlightId");

                    b.Navigation("Flight");
                });

            modelBuilder.Entity("Core.Entities.Terminal.Flight", b =>
                {
                    b.HasOne("Core.Entities.ForFlight.Pilot", "Pilot")
                        .WithMany()
                        .HasForeignKey("PilotId");

                    b.HasOne("Core.Entities.ForFlight.Plane", "Plane")
                        .WithMany()
                        .HasForeignKey("PlaneId");

                    b.Navigation("Pilot");

                    b.Navigation("Plane");
                });

            modelBuilder.Entity("Core.Entities.Terminal.Leg", b =>
                {
                    b.HasOne("Core.Entities.Terminal.Flight", "Flight")
                        .WithMany()
                        .HasForeignKey("FlightId");

                    b.Navigation("Flight");
                });

            modelBuilder.Entity("Core.Entities.Terminal.Flight", b =>
                {
                    b.Navigation("ProcessLog");
                });
#pragma warning restore 612, 618
        }
    }
}
