using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RecipeSiteBackend.Migrations
{
    /// <inheritdoc />
    public partial class ChangedUUIDsFromString_typetouuid_type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Diet_Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    UUID = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Difficulty = table.Column<string>(type: "text", nullable: true),
                    Prep_Time_Mins = table.Column<int>(type: "integer", nullable: false),
                    Cook_Time_Mins = table.Column<int>(type: "integer", nullable: false),
                    Servings = table.Column<int>(type: "integer", nullable: false),
                    Cuisine = table.Column<string>(type: "text", nullable: true),
                    Meal_Type = table.Column<string>(type: "text", nullable: true),
                    Source = table.Column<string>(type: "text", nullable: true),
                    OwnerUUID = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tag_Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DietRecipe",
                columns: table => new
                {
                    DietsId = table.Column<int>(type: "integer", nullable: false),
                    RecipesUUID = table.Column<Guid>(type: "uuid", nullable: false)
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
                    Ingredient_Number = table.Column<int>(type: "integer", nullable: false),
                    RecipeUUID = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => new { x.RecipeUUID, x.Ingredient_Number });
                    table.ForeignKey(
                        name: "FK_Ingredients_Recipes_RecipeUUID",
                        column: x => x.RecipeUUID,
                        principalTable: "Recipes",
                        principalColumn: "UUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instructions",
                columns: table => new
                {
                    Sequence_Number = table.Column<int>(type: "integer", nullable: false),
                    RecipeUUID = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructions", x => new { x.RecipeUUID, x.Sequence_Number });
                    table.ForeignKey(
                        name: "FK_Instructions_Recipes_RecipeUUID",
                        column: x => x.RecipeUUID,
                        principalTable: "Recipes",
                        principalColumn: "UUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Note_Number = table.Column<int>(type: "integer", nullable: false),
                    RecipeUUID = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => new { x.RecipeUUID, x.Note_Number });
                    table.ForeignKey(
                        name: "FK_Notes_Recipes_RecipeUUID",
                        column: x => x.RecipeUUID,
                        principalTable: "Recipes",
                        principalColumn: "UUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Policies",
                columns: table => new
                {
                    UserUUID = table.Column<Guid>(type: "uuid", nullable: false),
                    RecipeUUID = table.Column<Guid>(type: "uuid", nullable: false),
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
                name: "Ratings",
                columns: table => new
                {
                    UserUUID = table.Column<Guid>(type: "uuid", nullable: false),
                    RecipeUUID = table.Column<Guid>(type: "uuid", nullable: false),
                    Score = table.Column<decimal>(type: "numeric(2,1)", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => new { x.UserUUID, x.RecipeUUID });
                    table.ForeignKey(
                        name: "FK_Ratings_Recipes_RecipeUUID",
                        column: x => x.RecipeUUID,
                        principalTable: "Recipes",
                        principalColumn: "UUID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Users_UserUUID",
                        column: x => x.UserUUID,
                        principalTable: "Users",
                        principalColumn: "UUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recipe_Images",
                columns: table => new
                {
                    Image_Number = table.Column<int>(type: "integer", nullable: false),
                    RecipeUUID = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe_Images", x => new { x.RecipeUUID, x.Image_Number });
                    table.ForeignKey(
                        name: "FK_Recipe_Images_Recipes_RecipeUUID",
                        column: x => x.RecipeUUID,
                        principalTable: "Recipes",
                        principalColumn: "UUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recipe_Videos",
                columns: table => new
                {
                    Video_Number = table.Column<int>(type: "integer", nullable: false),
                    RecipeUUID = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe_Videos", x => new { x.RecipeUUID, x.Video_Number });
                    table.ForeignKey(
                        name: "FK_Recipe_Videos_Recipes_RecipeUUID",
                        column: x => x.RecipeUUID,
                        principalTable: "Recipes",
                        principalColumn: "UUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeTag",
                columns: table => new
                {
                    RecipesUUID = table.Column<Guid>(type: "uuid", nullable: false),
                    TagsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeTag", x => new { x.RecipesUUID, x.TagsId });
                    table.ForeignKey(
                        name: "FK_RecipeTag_Recipes_RecipesUUID",
                        column: x => x.RecipesUUID,
                        principalTable: "Recipes",
                        principalColumn: "UUID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instruction_Images",
                columns: table => new
                {
                    Image_Number = table.Column<int>(type: "integer", nullable: false),
                    InstructionUUID = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: true),
                    InstructionRecipeUUID = table.Column<Guid>(type: "uuid", nullable: true),
                    InstructionSequence_Number = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instruction_Images", x => new { x.InstructionUUID, x.Image_Number });
                    table.ForeignKey(
                        name: "FK_Instruction_Images_Instructions_InstructionRecipeUUID_Instr~",
                        columns: x => new { x.InstructionRecipeUUID, x.InstructionSequence_Number },
                        principalTable: "Instructions",
                        principalColumns: new[] { "RecipeUUID", "Sequence_Number" });
                });

            migrationBuilder.CreateTable(
                name: "Instruction_Videos",
                columns: table => new
                {
                    Video_Number = table.Column<int>(type: "integer", nullable: false),
                    InstructionUUID = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: true),
                    InstructionRecipeUUID = table.Column<Guid>(type: "uuid", nullable: true),
                    InstructionSequence_Number = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instruction_Videos", x => new { x.InstructionUUID, x.Video_Number });
                    table.ForeignKey(
                        name: "FK_Instruction_Videos_Instructions_InstructionRecipeUUID_Instr~",
                        columns: x => new { x.InstructionRecipeUUID, x.InstructionSequence_Number },
                        principalTable: "Instructions",
                        principalColumns: new[] { "RecipeUUID", "Sequence_Number" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_DietRecipe_RecipesUUID",
                table: "DietRecipe",
                column: "RecipesUUID");

            migrationBuilder.CreateIndex(
                name: "IX_Instruction_Images_InstructionRecipeUUID_InstructionSequenc~",
                table: "Instruction_Images",
                columns: new[] { "InstructionRecipeUUID", "InstructionSequence_Number" });

            migrationBuilder.CreateIndex(
                name: "IX_Instruction_Videos_InstructionRecipeUUID_InstructionSequenc~",
                table: "Instruction_Videos",
                columns: new[] { "InstructionRecipeUUID", "InstructionSequence_Number" });

            migrationBuilder.CreateIndex(
                name: "IX_Policies_RecipeUUID",
                table: "Policies",
                column: "RecipeUUID");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_RecipeUUID",
                table: "Ratings",
                column: "RecipeUUID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeTag_TagsId",
                table: "RecipeTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_OwnerUUID",
                table: "Recipes",
                column: "OwnerUUID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DietRecipe");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Instruction_Images");

            migrationBuilder.DropTable(
                name: "Instruction_Videos");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Policies");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "RecipeTag");

            migrationBuilder.DropTable(
                name: "Recipe_Images");

            migrationBuilder.DropTable(
                name: "Recipe_Videos");

            migrationBuilder.DropTable(
                name: "Diets");

            migrationBuilder.DropTable(
                name: "Instructions");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Recipes");
        }
    }
}
