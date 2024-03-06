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
    [Migration("20240225081828_undo")]
    partial class undo
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

                    b.Property<Guid>("RecipesUUID")
                        .HasColumnType("uuid");

                    b.HasKey("DietsId", "RecipesUUID");

                    b.HasIndex("RecipesUUID");

                    b.ToTable("DietRecipe");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Cuisine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Cuisine_Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Cuisine_Name")
                        .IsUnique();

                    b.ToTable("Cuisines");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Description_Media", b =>
                {
                    b.Property<Guid>("UUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Filetype")
                        .HasColumnType("text");

                    b.Property<Guid>("RecipeUUID")
                        .HasColumnType("uuid");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("UUID");

                    b.HasIndex("RecipeUUID");

                    b.ToTable("Description_Medias");
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

                    b.HasIndex("Diet_Name")
                        .IsUnique();

                    b.ToTable("Diets");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Difficulty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Difficulty_Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Difficulty_Name")
                        .IsUnique();

                    b.ToTable("Difficulties");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Ingredient", b =>
                {
                    b.Property<Guid>("UUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("Ingredient_Number")
                        .HasColumnType("integer");

                    b.Property<Guid>("RecipeUUID")
                        .HasColumnType("uuid");

                    b.HasKey("UUID");

                    b.HasIndex("RecipeUUID", "Ingredient_Number")
                        .IsUnique();

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Instruction", b =>
                {
                    b.Property<Guid>("UUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid>("RecipeUUID")
                        .HasColumnType("uuid");

                    b.Property<int>("Sequence_Number")
                        .HasColumnType("integer");

                    b.HasKey("UUID");

                    b.HasIndex("RecipeUUID", "Sequence_Number")
                        .IsUnique();

                    b.ToTable("Instructions");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Instruction_Image", b =>
                {
                    b.Property<Guid>("UUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("Image_Number")
                        .HasColumnType("integer");

                    b.Property<Guid>("InstructionUUID")
                        .HasColumnType("uuid");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("UUID");

                    b.HasIndex("InstructionUUID", "Image_Number")
                        .IsUnique();

                    b.ToTable("Instruction_Images");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Instruction_Video", b =>
                {
                    b.Property<Guid>("UUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid>("InstructionUUID")
                        .HasColumnType("uuid");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.Property<int>("Video_Number")
                        .HasColumnType("integer");

                    b.HasKey("UUID");

                    b.HasIndex("InstructionUUID", "Video_Number")
                        .IsUnique();

                    b.ToTable("Instruction_Videos");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Note", b =>
                {
                    b.Property<Guid>("UUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("Note_Number")
                        .HasColumnType("integer");

                    b.Property<Guid>("RecipeUUID")
                        .HasColumnType("uuid");

                    b.HasKey("UUID");

                    b.HasIndex("RecipeUUID", "Note_Number")
                        .IsUnique();

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Permitted_User", b =>
                {
                    b.Property<Guid>("UUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Permission_Level")
                        .HasColumnType("text");

                    b.Property<Guid>("RecipeUUID")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserUUID")
                        .HasColumnType("uuid");

                    b.HasKey("UUID");

                    b.HasIndex("UserUUID");

                    b.HasIndex("RecipeUUID", "UserUUID")
                        .IsUnique();

                    b.ToTable("Policies");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Rating", b =>
                {
                    b.Property<Guid>("UUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid>("RecipeUUID")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Score")
                        .HasColumnType("decimal(2,1)");

                    b.Property<Guid>("UserUUID")
                        .HasColumnType("uuid");

                    b.HasKey("UUID");

                    b.HasIndex("UserUUID");

                    b.HasIndex("RecipeUUID", "UserUUID")
                        .IsUnique();

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Recipe", b =>
                {
                    b.Property<Guid>("UUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Cook_Time_Mins")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CuisineId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("DifficultyId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsViewableByPublic")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Meal_Type")
                        .HasColumnType("text");

                    b.Property<Guid>("OwnerUUID")
                        .HasColumnType("uuid");

                    b.Property<int>("Prep_Time_Mins")
                        .HasColumnType("integer");

                    b.Property<int>("Servings")
                        .HasColumnType("integer");

                    b.Property<string>("Source")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("UUID");

                    b.HasIndex("CuisineId");

                    b.HasIndex("DifficultyId");

                    b.HasIndex("OwnerUUID");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Recipe_Image", b =>
                {
                    b.Property<Guid>("UUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("Image_Number")
                        .HasColumnType("integer");

                    b.Property<Guid>("RecipeUUID")
                        .HasColumnType("uuid");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("UUID");

                    b.HasIndex("RecipeUUID", "Image_Number")
                        .IsUnique();

                    b.ToTable("Recipe_Images");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Recipe_Video", b =>
                {
                    b.Property<Guid>("UUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid>("RecipeUUID")
                        .HasColumnType("uuid");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.Property<int>("Video_Number")
                        .HasColumnType("integer");

                    b.HasKey("UUID");

                    b.HasIndex("RecipeUUID", "Video_Number")
                        .IsUnique();

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

                    b.HasIndex("Tag_Name")
                        .IsUnique();

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.User", b =>
                {
                    b.Property<Guid>("UUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

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
                    b.Property<Guid>("RecipesUUID")
                        .HasColumnType("uuid");

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

            modelBuilder.Entity("RecipeSiteBackend.Models.Description_Media", b =>
                {
                    b.HasOne("RecipeSiteBackend.Models.Recipe", "Recipe")
                        .WithMany()
                        .HasForeignKey("RecipeUUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Ingredient", b =>
                {
                    b.HasOne("RecipeSiteBackend.Models.Recipe", "Recipes")
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeUUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipes");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Instruction", b =>
                {
                    b.HasOne("RecipeSiteBackend.Models.Recipe", "Recipe")
                        .WithMany("Instructions")
                        .HasForeignKey("RecipeUUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Instruction_Image", b =>
                {
                    b.HasOne("RecipeSiteBackend.Models.Instruction", "Instruction")
                        .WithMany("Images")
                        .HasForeignKey("InstructionUUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instruction");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Instruction_Video", b =>
                {
                    b.HasOne("RecipeSiteBackend.Models.Instruction", "Instruction")
                        .WithMany("Videos")
                        .HasForeignKey("InstructionUUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instruction");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Note", b =>
                {
                    b.HasOne("RecipeSiteBackend.Models.Recipe", "Recipe")
                        .WithMany("Notes")
                        .HasForeignKey("RecipeUUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
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
                    b.HasOne("RecipeSiteBackend.Models.Cuisine", "Cuisine")
                        .WithMany()
                        .HasForeignKey("CuisineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecipeSiteBackend.Models.Difficulty", "Difficulty")
                        .WithMany()
                        .HasForeignKey("DifficultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecipeSiteBackend.Models.User", "Owner")
                        .WithMany("Recipes")
                        .HasForeignKey("OwnerUUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cuisine");

                    b.Navigation("Difficulty");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Recipe_Image", b =>
                {
                    b.HasOne("RecipeSiteBackend.Models.Recipe", "Recipe")
                        .WithMany("Images")
                        .HasForeignKey("RecipeUUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Recipe_Video", b =>
                {
                    b.HasOne("RecipeSiteBackend.Models.Recipe", "Recipe")
                        .WithMany("Videos")
                        .HasForeignKey("RecipeUUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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

            modelBuilder.Entity("RecipeSiteBackend.Models.Instruction", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Videos");
                });

            modelBuilder.Entity("RecipeSiteBackend.Models.Recipe", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Ingredients");

                    b.Navigation("Instructions");

                    b.Navigation("Notes");

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