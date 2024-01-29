using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeSiteBackend.Migrations
{
    /// <inheritdoc />
    public partial class Addcompositeuniquekeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Recipe_Videos_RecipeUUID",
                table: "Recipe_Videos");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_Images_RecipeUUID",
                table: "Recipe_Images");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_RecipeUUID",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Policies_RecipeUUID",
                table: "Policies");

            migrationBuilder.DropIndex(
                name: "IX_Notes_RecipeUUID",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Instructions_RecipeUUID",
                table: "Instructions");

            migrationBuilder.DropIndex(
                name: "IX_Instruction_Videos_InstructionUUID",
                table: "Instruction_Videos");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_RecipeUUID",
                table: "Ingredients");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_Videos_RecipeUUID_Video_Number",
                table: "Recipe_Videos",
                columns: new[] { "RecipeUUID", "Video_Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_Images_RecipeUUID_Image_Number",
                table: "Recipe_Images",
                columns: new[] { "RecipeUUID", "Image_Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_RecipeUUID_UserUUID",
                table: "Ratings",
                columns: new[] { "RecipeUUID", "UserUUID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Policies_RecipeUUID_UserUUID",
                table: "Policies",
                columns: new[] { "RecipeUUID", "UserUUID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_RecipeUUID_Note_Number",
                table: "Notes",
                columns: new[] { "RecipeUUID", "Note_Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_RecipeUUID_Sequence_Number",
                table: "Instructions",
                columns: new[] { "RecipeUUID", "Sequence_Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instruction_Videos_InstructionUUID_Video_Number",
                table: "Instruction_Videos",
                columns: new[] { "InstructionUUID", "Video_Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RecipeUUID_Ingredient_Number",
                table: "Ingredients",
                columns: new[] { "RecipeUUID", "Ingredient_Number" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Recipe_Videos_RecipeUUID_Video_Number",
                table: "Recipe_Videos");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_Images_RecipeUUID_Image_Number",
                table: "Recipe_Images");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_RecipeUUID_UserUUID",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Policies_RecipeUUID_UserUUID",
                table: "Policies");

            migrationBuilder.DropIndex(
                name: "IX_Notes_RecipeUUID_Note_Number",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Instructions_RecipeUUID_Sequence_Number",
                table: "Instructions");

            migrationBuilder.DropIndex(
                name: "IX_Instruction_Videos_InstructionUUID_Video_Number",
                table: "Instruction_Videos");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_RecipeUUID_Ingredient_Number",
                table: "Ingredients");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_Videos_RecipeUUID",
                table: "Recipe_Videos",
                column: "RecipeUUID");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_Images_RecipeUUID",
                table: "Recipe_Images",
                column: "RecipeUUID");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_RecipeUUID",
                table: "Ratings",
                column: "RecipeUUID");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_RecipeUUID",
                table: "Policies",
                column: "RecipeUUID");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_RecipeUUID",
                table: "Notes",
                column: "RecipeUUID");

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_RecipeUUID",
                table: "Instructions",
                column: "RecipeUUID");

            migrationBuilder.CreateIndex(
                name: "IX_Instruction_Videos_InstructionUUID",
                table: "Instruction_Videos",
                column: "InstructionUUID");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RecipeUUID",
                table: "Ingredients",
                column: "RecipeUUID");
        }
    }
}
