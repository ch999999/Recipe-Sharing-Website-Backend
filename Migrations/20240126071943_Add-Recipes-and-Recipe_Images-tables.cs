using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeSiteBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddRecipesandRecipe_Imagestables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    UUID = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Difficulty = table.Column<string>(type: "text", nullable: true),
                    Prep_Time_Mins = table.Column<int>(type: "integer", nullable: false),
                    Cook_Time_Mins = table.Column<int>(type: "integer", nullable: false),
                    Servings = table.Column<int>(type: "integer", nullable: false),
                    Cuisine = table.Column<string>(type: "text", nullable: true),
                    Meal_Type = table.Column<string>(type: "text", nullable: true),
                    Source = table.Column<string>(type: "text", nullable: true),
                    Rating = table.Column<decimal>(type: "numeric(2,1)", nullable: false),
                    OwnerUUID = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastMofifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsViewableByPublic = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.UUID);
                    table.ForeignKey(
                        name: "FK_Recipes_Users_OwnerUUID",
                        column: x => x.OwnerUUID,
                        principalTable: "Users",
                        principalColumn: "UUID");
                });

            migrationBuilder.CreateTable(
                name: "Recipe_Images",
                columns: table => new
                {
                    UUID = table.Column<string>(type: "text", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: true),
                    RecipeUUID = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe_Images", x => x.UUID);
                    table.ForeignKey(
                        name: "FK_Recipe_Images_Recipes_RecipeUUID",
                        column: x => x.RecipeUUID,
                        principalTable: "Recipes",
                        principalColumn: "UUID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_Images_RecipeUUID",
                table: "Recipe_Images",
                column: "RecipeUUID");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_OwnerUUID",
                table: "Recipes",
                column: "OwnerUUID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recipe_Images");

            migrationBuilder.DropTable(
                name: "Recipes");
        }
    }
}
