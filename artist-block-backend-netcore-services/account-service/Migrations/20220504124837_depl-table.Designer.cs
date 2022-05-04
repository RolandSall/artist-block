﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using account_service.Repository;

#nullable disable

namespace account_service.Migrations
{
    [DbContext(typeof(ArtistBlockDbContext))]
    [Migration("20220504124837_depl-table")]
    partial class depltable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("account_service.Models.AuthUser", b =>
                {
                    b.Property<string>("Auth0Id")
                        .HasColumnType("text")
                        .HasColumnName("PK_auth0_id");

                    b.Property<Guid>("RegisteredUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("FK_auth0_registered_user_id");

                    b.HasKey("Auth0Id");

                    b.HasIndex("RegisteredUserId");

                    b.ToTable("auth_user");
                });

            modelBuilder.Entity("account_service.Models.Deployment", b =>
                {
                    b.Property<Guid>("DeploymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("deployment_id");

                    b.Property<int>("count")
                        .HasColumnType("integer")
                        .HasColumnName("deployment_count");

                    b.Property<DateTime>("timestamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deployment_timestamp");

                    b.HasKey("DeploymentId");

                    b.ToTable("deployment");
                });

            modelBuilder.Entity("account_service.Models.GanGeneratedImage", b =>
                {
                    b.Property<Guid>("GanImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("PK_gan_image_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("gan_image_description");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("gan_image_url");

                    b.Property<Guid?>("RegisteredUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("FK_painting_registered_user_id");

                    b.HasKey("GanImageId");

                    b.HasIndex("RegisteredUserId");

                    b.ToTable("gan_image");
                });

            modelBuilder.Entity("account_service.Models.Painter", b =>
                {
                    b.Property<Guid>("PainterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("PK_painter_id");

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("bio");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Location");

                    b.Property<Guid>("RegisteredUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("FK_painter_registered_user_id");

                    b.Property<string>("YearsOfExperience")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("years_of_experience");

                    b.HasKey("PainterId");

                    b.HasIndex("RegisteredUserId")
                        .IsUnique();

                    b.ToTable("painter");
                });

            modelBuilder.Entity("account_service.Models.PainterSpeciality", b =>
                {
                    b.Property<Guid>("PainterSpecialityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("PK_painter_speciality_id");

                    b.Property<Guid>("PainterId")
                        .HasColumnType("uuid")
                        .HasColumnName("FK_painter_id");

                    b.Property<int>("Priority")
                        .HasColumnType("integer")
                        .HasColumnName("priority");

                    b.Property<Guid>("SpecialityId")
                        .HasColumnType("uuid")
                        .HasColumnName("FK_speciality_id");

                    b.HasKey("PainterSpecialityId");

                    b.HasIndex("PainterId");

                    b.HasIndex("SpecialityId");

                    b.ToTable("painter_speciality");
                });

            modelBuilder.Entity("account_service.Models.Painting", b =>
                {
                    b.Property<Guid>("PaintingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("PK_painting_id");

                    b.Property<DateTime?>("BoughtTimeStamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("bought_timestamp");

                    b.Property<DateTime?>("BuyTimeStamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("buy_timestamp");

                    b.Property<string>("PaintedYear")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("painted_year");

                    b.Property<Guid>("PainterId")
                        .HasColumnType("uuid")
                        .HasColumnName("FK_painting_painter_id");

                    b.Property<string>("PaintingDescription")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("painting_description");

                    b.Property<string>("PaintingName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("painting_name");

                    b.Property<int?>("PaintingPrice")
                        .IsRequired()
                        .HasColumnType("integer")
                        .HasColumnName("painting_price");

                    b.Property<string>("PaintingStatus")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.Property<string>("PaintingUrl")
                        .HasColumnType("text")
                        .HasColumnName("painting_url");

                    b.Property<Guid?>("RegisteredUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("FK_painting_registered_user_id");

                    b.HasKey("PaintingId");

                    b.HasIndex("PainterId");

                    b.HasIndex("RegisteredUserId");

                    b.ToTable("painting");
                });

            modelBuilder.Entity("account_service.Models.PaintingReview", b =>
                {
                    b.Property<Guid>("PaintingReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("PK_painting_review_id");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("comment");

                    b.Property<bool>("LikeStatus")
                        .HasColumnType("boolean")
                        .HasColumnName("like_status");

                    b.Property<Guid>("PaintingId")
                        .HasColumnType("uuid")
                        .HasColumnName("FK_painting_review_painting_id");

                    b.Property<Guid?>("RegisteredUserId")
                        .IsRequired()
                        .HasColumnType("uuid")
                        .HasColumnName("FK_painting_review_registered_user_id");

                    b.Property<DateTime?>("Timestamp")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp");

                    b.HasKey("PaintingReviewId");

                    b.HasIndex("PaintingId");

                    b.HasIndex("RegisteredUserId");

                    b.ToTable("PaintingReview");
                });

            modelBuilder.Entity("account_service.Models.RegisteredUser", b =>
                {
                    b.Property<Guid>("RegisteredUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("PK_registered_user_id");

                    b.Property<DateTime?>("BirthDate")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("birth_date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("Image")
                        .HasColumnType("text")
                        .HasColumnName("image_url");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nationality");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("RegisteredUserId");

                    b.ToTable("client");
                });

            modelBuilder.Entity("account_service.Models.Speciality", b =>
                {
                    b.Property<Guid>("SpecialityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("PK_speciality_id");

                    b.Property<string>("SpecialityType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("speciality_type");

                    b.HasKey("SpecialityId");

                    b.ToTable("speciality");
                });

            modelBuilder.Entity("account_service.Models.AuthUser", b =>
                {
                    b.HasOne("account_service.Models.RegisteredUser", "RegisteredUser")
                        .WithMany()
                        .HasForeignKey("RegisteredUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RegisteredUser");
                });

            modelBuilder.Entity("account_service.Models.GanGeneratedImage", b =>
                {
                    b.HasOne("account_service.Models.RegisteredUser", null)
                        .WithMany("ClaimedGanImages")
                        .HasForeignKey("RegisteredUserId");
                });

            modelBuilder.Entity("account_service.Models.Painter", b =>
                {
                    b.HasOne("account_service.Models.RegisteredUser", "RegisteredUser")
                        .WithOne("Painter")
                        .HasForeignKey("account_service.Models.Painter", "RegisteredUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RegisteredUser");
                });

            modelBuilder.Entity("account_service.Models.PainterSpeciality", b =>
                {
                    b.HasOne("account_service.Models.Painter", null)
                        .WithMany("PainterSpecialities")
                        .HasForeignKey("PainterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("account_service.Models.Speciality", "Speciality")
                        .WithMany("PainterSpecialities")
                        .HasForeignKey("SpecialityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Speciality");
                });

            modelBuilder.Entity("account_service.Models.Painting", b =>
                {
                    b.HasOne("account_service.Models.Painter", "Painter")
                        .WithMany("Paintings")
                        .HasForeignKey("PainterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("account_service.Models.RegisteredUser", null)
                        .WithMany("PaintingsBought")
                        .HasForeignKey("RegisteredUserId");

                    b.Navigation("Painter");
                });

            modelBuilder.Entity("account_service.Models.PaintingReview", b =>
                {
                    b.HasOne("account_service.Models.Painting", "Painting")
                        .WithMany("Reviews")
                        .HasForeignKey("PaintingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("account_service.Models.RegisteredUser", "RegisteredUser")
                        .WithMany("Reviews")
                        .HasForeignKey("RegisteredUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Painting");

                    b.Navigation("RegisteredUser");
                });

            modelBuilder.Entity("account_service.Models.Painter", b =>
                {
                    b.Navigation("PainterSpecialities");

                    b.Navigation("Paintings");
                });

            modelBuilder.Entity("account_service.Models.Painting", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("account_service.Models.RegisteredUser", b =>
                {
                    b.Navigation("ClaimedGanImages");

                    b.Navigation("Painter")
                        .IsRequired();

                    b.Navigation("PaintingsBought");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("account_service.Models.Speciality", b =>
                {
                    b.Navigation("PainterSpecialities");
                });
#pragma warning restore 612, 618
        }
    }
}
