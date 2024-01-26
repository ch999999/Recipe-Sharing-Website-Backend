using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeSiteBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddRecipesUsersPolicies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DietsRecipes");

            migrationBuilder.CreateTable(
                name: "DietRecipe",
                columns: table => new
                {
                    DietsId = table.Column<int>(type: "integer", nullable: false),
                    RecipesUUID = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietRecipe", x => new { x.DietsId, x.RecipesUUID });
                    table.ForeignKey(
                        name: "FK_DietRecipe_Diets_DietsId",
                        column: x => x.DietsId,
                        principalTable: "Diets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DietRecipe_Recipes_RecipesUUID",
                        column: x => x.RecipesUUID,
                        principalTable: "Recipes",
                        principalColumn: "UUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    UUID = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    RecipesUUID = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.UUID);
                    table.ForeignKey(
                        name: "FK_Ingredients_Recipes_RecipesUUID",
                        column: x => x.RecipesUUID,
                        principalTable: "Recipes",
                        principalColumn: "UUID");
                });

            migrationBuilder.CreateTable(
                name: "Policies",
                columns: table => new
                {
                    UserUUID = table.Column<string>(type: "text", nullable: false),
                    RecipeUUID = table.Column<string>(type: "text", nullable: false),
                    Permission_Level = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policies", x => new { x.UserUUID, x.RecipeUUID });
                    table.ForeignKey(
                        name: "FK_Policies_Recipes_RecipeUUID",
                        column: x => x.RecipeUUID,
                        principalTable: "Recipes",
                        principalColumn: "UUID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Policies_Users_UserUUID",
                        column: x => x.UserUUID,
                        principalTable: "Users",
                        principalColumn: "UUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recipe_Videos",
                columns: table => new
                {
                    UUID = table.Column<string>(type: "text", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: true),
                    RecipeUUID = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe_Videos", x => x.UUID);
                    table.ForeignKey(
                        name: "FK_Recipe_Videos_Recipes_RecipeUUID",
                        column: x => x.RecipeUUID,
                        principalTable: "Recipes",
                        principalColumn: "UUID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DietRecipe_RecipesUUID",
                table: "DietRecipe",
                column: "RecipesUUID");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RecipesUUID",
                table: "Ingredients",
                column: "RecipesUUID");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_RecipeUUID",
                table: "Policies",
                column: "RecipeUUID");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_Videos_RecipeUUID",
                table: "Recipe_Videos",
                column: "RecipeUUID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DietRecipe");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Policies");

            migrationBuilder.DropTable(
                name: "Recipe_Videos");

            migrationBuilder.CreateTable(
                name: "DietsRecipes",
                columns: table => new
                {
                    DietsId = table.Column<int>(type: "integer", nullable: false),
                    RecipesUUID = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietsRecipes", x => new { x.DietsId, x.RecipesUUID });
                    table.ForeignKey(
                        name: "FK_DietsRecipes_Diets_DietsId",
                        column: x => x.DietsId,
                        principalTable: "Diets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DietsRecipes_Recipes_RecipesUUID",
                        column: x => x.RecipesUUID,
                        principalTable: "Recipes",
                        principalColumn: "UUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DietsRecipes_RecipesUUID",
                table: "DietsRecipes",
                column: "RecipesUUID");
        }
    }
}
