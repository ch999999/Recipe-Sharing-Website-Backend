﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RecipeSiteBackend.Data;

#nullable disable

namespace RecipeSiteBackend.Migrations
{
    [DbContext(typeof(RecipesDbContext))]
    [Migration("20240126131917_Add-Tags-m2m-with-Recipes")]
    partial class AddTagsm2mwithRecipes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DietRecipe", b =>
                {
                    b.Property<int>("DietsId")
                        .HasColumnType("integer");

                    b.Property<string>("RecipesUUID")
                        .HasColumnType("text");

                    b.HasKey("DietsId", "RecipesUUID");

                    b.HasIndex("RecipesUUID");

                    b.ToTable("DietRecipe");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Diet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Diet_Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Diets");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Ingredient", b =>
                {
                    b.Property<string>("UUID")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("RecipesUUID")
                        .HasColumnType("text");

                    b.HasKey("UUID");

                    b.HasIndex("RecipesUUID");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Permitted_User", b =>
                {
                    b.Property<string>("UserUUID")
                        .HasColumnType("text");

                    b.Property<string>("RecipeUUID")
                        .HasColumnType("text");

                    b.Property<string>("Permission_Level")
                        .HasColumnType("text");

                    b.HasKey("UserUUID", "RecipeUUID");

                    b.HasIndex("RecipeUUID");

                    b.ToTable("Policies");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Rating", b =>
                {
                    b.Property<string>("UserUUID")
                        .HasColumnType("text");

                    b.Property<string>("RecipeUUID")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<decimal>("Score")
                        .HasColumnType("decimal(2,1)");

                    b.HasKey("UserUUID", "RecipeUUID");

                    b.HasIndex("RecipeUUID");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Recipe", b =>
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

            modelBuilder.Entity("RecipeSiteBackend.Models.Recipe_Image", b =>
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

            modelBuilder.Entity("RecipeSiteBackend.Models.Recipe_Video", b =>
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

                    b.ToTable("Recipe_Videos");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Tag_Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tags");
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

            modelBuilder.Entity("RecipeTag", b =>
                {
                    b.Property<string>("RecipesUUID")
                        .HasColumnType("text");

                    b.Property<int>("TagsId")
                        .HasColumnType("integer");

                    b.HasKey("RecipesUUID", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("RecipeTag");
                });

            modelBuilder.Entity("DietRecipe", b =>
                {
                    b.HasOne("RecipeSiteBackend.Models.Diet", null)
                        .WithMany()
                        .HasForeignKey("DietsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecipeSiteBackend.Models.Recipe", null)
                        .WithMany()
                        .HasForeignKey("RecipesUUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Ingredient", b =>
                {
                    b.HasOne("RecipeSiteBackend.Models.Recipe", "Recipes")
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipesUUID");

                    b.Navigation("Recipes");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Permitted_User", b =>
                {
                    b.HasOne("RecipeSiteBackend.Models.Recipe", "Recipe")
                        .WithMany("Policies")
                        .HasForeignKey("RecipeUUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecipeSiteBackend.Models.User", "User")
                        .WithMany("Policies")
                        .HasForeignKey("UserUUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Rating", b =>
                {
                    b.HasOne("RecipeSiteBackend.Models.Recipe", "Recipe")
                        .WithMany("Ratings")
                        .HasForeignKey("RecipeUUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecipeSiteBackend.Models.User", "User")
                        .WithMany("Ratings")
                        .HasForeignKey("UserUUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Recipe", b =>
                {
                    b.HasOne("RecipeSiteBackend.Models.User", "Owner")
                        .WithMany("Recipes")
                        .HasForeignKey("OwnerUUID");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Recipe_Image", b =>
                {
                    b.HasOne("RecipeSiteBackend.Models.Recipe", "Recipe")
                        .WithMany("Images")
                        .HasForeignKey("RecipeUUID");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Recipe_Video", b =>
                {
                    b.HasOne("RecipeSiteBackend.Models.Recipe", "Recipe")
                        .WithMany("Videos")
                        .HasForeignKey("RecipeUUID");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("RecipeTag", b =>
                {
                    b.HasOne("RecipeSiteBackend.Models.Recipe", null)
                        .WithMany()
                        .HasForeignKey("RecipesUUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecipeSiteBackend.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Recipe", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Ingredients");

                    b.Navigation("Policies");

                    b.Navigation("Ratings");

                    b.Navigation("Videos");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.User", b =>
                {
                    b.Navigation("Policies");

                    b.Navigation("Ratings");

                    b.Navigation("Recipes");
                });
#pragma warning restore 612, 618
        }
    }
}
