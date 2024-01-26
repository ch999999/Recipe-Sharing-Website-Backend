﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RecipeSiteBackend.Data;

#nullable disable

namespace RecipeSiteBackend.Migrations
{
    [DbContext(typeof(RecipesDbContext))]
    partial class RecipesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RecipeSiteBackend.Models.Recipe_Images", b =>
                {
                    b.Property<string>("UUID")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("RecipeUUID")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("UUID");

                    b.HasIndex("RecipeUUID");

                    b.ToTable("Recipe_Images");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Recipes", b =>
                {
                    b.Property<string>("UUID")
                        .HasColumnType("text");

                    b.Property<int>("Cook_Time_Mins")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Cuisine")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Difficulty")
                        .HasColumnType("text");

                    b.Property<bool>("IsViewableByPublic")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Meal_Type")
                        .HasColumnType("text");

                    b.Property<string>("OwnerUUID")
                        .HasColumnType("text");

                    b.Property<int>("Prep_Time_Mins")
                        .HasColumnType("integer");

                    b.Property<int>("Servings")
                        .HasColumnType("integer");

                    b.Property<string>("Source")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("UUID");

                    b.HasIndex("OwnerUUID");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.User", b =>
                {
                    b.Property<string>("UUID")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Lastname")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.HasKey("UUID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Recipe_Images", b =>
                {
                    b.HasOne("RecipeSiteBackend.Models.Recipes", "Recipe")
                        .WithMany("Images")
                        .HasForeignKey("RecipeUUID");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Recipes", b =>
                {
                    b.HasOne("RecipeSiteBackend.Models.User", "Owner")
                        .WithMany("Recipes")
                        .HasForeignKey("OwnerUUID");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Recipes", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.User", b =>
                {
                    b.Navigation("Recipes");
                });
#pragma warning restore 612, 618
        }
    }
}
