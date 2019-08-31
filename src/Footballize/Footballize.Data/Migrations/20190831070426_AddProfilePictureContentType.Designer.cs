﻿// <auto-generated />
using System;
using Footballize.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Footballize.Data.Migrations
{
    [DbContext(typeof(FootballizeDbContext))]
    [Migration("20190831070426_AddProfilePictureContentType")]
    partial class AddProfilePictureContentType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Footballize.Models.Address", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LocationId");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<int>("Number")
                        .HasMaxLength(3);

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<string>("TownId");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("LocationId");

                    b.HasIndex("TownId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Footballize.Models.ChatMessage", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("GatherId");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Sender")
                        .IsRequired()
                        .IsUnicode(true);

                    b.Property<string>("Text")
                        .IsRequired()
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("GatherId");

                    b.ToTable("ChatMessages");
                });

            modelBuilder.Entity("Footballize.Models.Country", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("IsoCode")
                        .IsRequired()
                        .HasColumnType("VARCHAR(5)")
                        .HasMaxLength(5);

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(30)")
                        .HasMaxLength(30)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Footballize.Models.Gather", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("CreatorId");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Description")
                        .HasColumnType("NVARCHAR(300)")
                        .HasMaxLength(300)
                        .IsUnicode(true);

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("MaximumPlayers");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("PitchId");

                    b.Property<DateTime>("StartingAt");

                    b.Property<int>("Status");

                    b.Property<int>("TeamFormat");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(30)")
                        .HasMaxLength(30)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("PitchId");

                    b.ToTable("Gathers");
                });

            modelBuilder.Entity("Footballize.Models.GatherUser", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("GatherId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.HasKey("UserId", "GatherId");

                    b.HasIndex("GatherId");

                    b.HasIndex("IsDeleted");

                    b.ToTable("GatherUsers");
                });

            modelBuilder.Entity("Footballize.Models.Location", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<DateTime?>("ModifiedOn");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Footballize.Models.Pitch", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(50)")
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("IsDeleted");

                    b.ToTable("Pitches");
                });

            modelBuilder.Entity("Footballize.Models.Playpal", b =>
                {
                    b.Property<string>("FromUserId");

                    b.Property<string>("ToUserId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.HasKey("FromUserId", "ToUserId");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("ToUserId");

                    b.ToTable("Playpals");
                });

            modelBuilder.Entity("Footballize.Models.ProfilePicture", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContentType");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<string>("PathToFile");

                    b.Property<int>("Size");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("ProfilePicture");
                });

            modelBuilder.Entity("Footballize.Models.Province", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CountryId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(75)")
                        .HasMaxLength(75)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("IsDeleted");

                    b.ToTable("Provinces");
                });

            modelBuilder.Entity("Footballize.Models.Recruitment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("CreatorId");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("MaximumPlayers");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("PitchId");

                    b.Property<DateTime>("StartingAt");

                    b.Property<int>("Status");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("PitchId");

                    b.ToTable("Recruitments");
                });

            modelBuilder.Entity("Footballize.Models.RecruitmentUser", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RecruitmentId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.HasKey("UserId", "RecruitmentId");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("RecruitmentId");

                    b.ToTable("RecruitmentUsers");
                });

            modelBuilder.Entity("Footballize.Models.Role", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Footballize.Models.Town", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(30)")
                        .HasMaxLength(30)
                        .IsUnicode(true);

                    b.Property<string>("ProvinceId");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("ProvinceId");

                    b.ToTable("Towns");
                });

            modelBuilder.Entity("Footballize.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<DateTime?>("BanUntil");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(30)")
                        .HasMaxLength(30)
                        .IsUnicode(true);

                    b.Property<bool>("IsBanned");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(30)")
                        .HasMaxLength(30)
                        .IsUnicode(true);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("UserName")
                        .IsUnique()
                        .HasFilter("[UserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Footballize.Models.UserReport", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("ReportedUserId")
                        .IsRequired();

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(350)")
                        .HasMaxLength(350)
                        .IsUnicode(true);

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("ReportedUserId");

                    b.ToTable("UserReports");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Footballize.Models.Address", b =>
                {
                    b.HasOne("Footballize.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.HasOne("Footballize.Models.Town", "Town")
                        .WithMany("Addresses")
                        .HasForeignKey("TownId");
                });

            modelBuilder.Entity("Footballize.Models.ChatMessage", b =>
                {
                    b.HasOne("Footballize.Models.Gather", "Gather")
                        .WithMany("ChatMessages")
                        .HasForeignKey("GatherId");
                });

            modelBuilder.Entity("Footballize.Models.Gather", b =>
                {
                    b.HasOne("Footballize.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.HasOne("Footballize.Models.Pitch", "Pitch")
                        .WithMany()
                        .HasForeignKey("PitchId");
                });

            modelBuilder.Entity("Footballize.Models.GatherUser", b =>
                {
                    b.HasOne("Footballize.Models.Gather", "Gather")
                        .WithMany("Players")
                        .HasForeignKey("GatherId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Footballize.Models.User", "User")
                        .WithMany("GathersPlayed")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Footballize.Models.Pitch", b =>
                {
                    b.HasOne("Footballize.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");
                });

            modelBuilder.Entity("Footballize.Models.Playpal", b =>
                {
                    b.HasOne("Footballize.Models.User", "FromUser")
                        .WithMany("PlaypalsAdded")
                        .HasForeignKey("FromUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Footballize.Models.User", "ToUser")
                        .WithMany("PlaypalsAddedMe")
                        .HasForeignKey("ToUserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Footballize.Models.ProfilePicture", b =>
                {
                    b.HasOne("Footballize.Models.User", "User")
                        .WithOne("ProfilePicture")
                        .HasForeignKey("Footballize.Models.ProfilePicture", "UserId");
                });

            modelBuilder.Entity("Footballize.Models.Province", b =>
                {
                    b.HasOne("Footballize.Models.Country", "Country")
                        .WithMany("Provinces")
                        .HasForeignKey("CountryId");
                });

            modelBuilder.Entity("Footballize.Models.Recruitment", b =>
                {
                    b.HasOne("Footballize.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.HasOne("Footballize.Models.Pitch", "Pitch")
                        .WithMany()
                        .HasForeignKey("PitchId");
                });

            modelBuilder.Entity("Footballize.Models.RecruitmentUser", b =>
                {
                    b.HasOne("Footballize.Models.Recruitment", "Recruitment")
                        .WithMany("Players")
                        .HasForeignKey("RecruitmentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Footballize.Models.User", "User")
                        .WithMany("GamesRecruited")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Footballize.Models.Town", b =>
                {
                    b.HasOne("Footballize.Models.Province", "Province")
                        .WithMany("Towns")
                        .HasForeignKey("ProvinceId");
                });

            modelBuilder.Entity("Footballize.Models.UserReport", b =>
                {
                    b.HasOne("Footballize.Models.User", "ReportedUser")
                        .WithMany("Reports")
                        .HasForeignKey("ReportedUserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Footballize.Models.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Footballize.Models.User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Footballize.Models.User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Footballize.Models.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Footballize.Models.User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Footballize.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
