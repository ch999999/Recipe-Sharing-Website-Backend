using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeSiteBackend.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceCompositeKeyswithNormal_PrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instruction_Images_Instructions_InstructionRecipeUUID_Instr~",
                table: "Instruction_Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Instruction_Videos_Instructions_InstructionRecipeUUID_Instr~",
                table: "Instruction_Videos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipe_Videos",
                table: "Recipe_Videos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipe_Images",
                table: "Recipe_Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Policies",
                table: "Policies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notes",
                table: "Notes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instructions",
                table: "Instructions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instruction_Videos",
                table: "Instruction_Videos");

            migrationBuilder.DropIndex(
                name: "IX_Instruction_Videos_InstructionRecipeUUID_InstructionSequenc~",
                table: "Instruction_Videos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instruction_Images",
                table: "Instruction_Images");

            migrationBuilder.DropIndex(
                name: "IX_Instruction_Images_InstructionRecipeUUID_InstructionSequenc~",
                table: "Instruction_Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "InstructionRecipeUUID",
                table: "Instruction_Videos");

            migrationBuilder.DropColumn(
                name: "InstructionSequence_Number",
                table: "Instruction_Videos");

            migrationBuilder.DropColumn(
                name: "InstructionRecipeUUID",
                table: "Instruction_Images");

            migrationBuilder.DropColumn(
                name: "InstructionSequence_Number",
                table: "Instruction_Images");

            migrationBuilder.AddColumn<Guid>(
                name: "UUID",
                table: "Recipe_Videos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UUID",
                table: "Recipe_Images",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UUID",
                table: "Ratings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UUID",
                table: "Policies",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UUID",
                table: "Notes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UUID",
                table: "Instructions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UUID",
                table: "Instruction_Videos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UUID",
                table: "Instruction_Images",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UUID",
                table: "Ingredients",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipe_Videos",
                table: "Recipe_Videos",
                column: "UUID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipe_Images",
                table: "Recipe_Images",
                column: "UUID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings",
                column: "UUID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Policies",
                table: "Policies",
                column: "UUID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notes",
                table: "Notes",
                column: "UUID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instructions",
                table: "Instructions",
                column: "UUID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instruction_Videos",
                table: "Instruction_Videos",
                column: "UUID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instruction_Images",
                table: "Instruction_Images",
                column: "UUID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients",
                column: "UUID");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_Videos_RecipeUUID",
                table: "Recipe_Videos",
                column: "RecipeUUID");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_Images_RecipeUUID",
                table: "Recipe_Images",
                column: "RecipeUUID");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserUUID",
                table: "Ratings",
                column: "UserUUID");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_UserUUID",
                table: "Policies",
                column: "UserUUID");

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
                name: "IX_Instruction_Images_InstructionUUID",
                table: "Instruction_Images",
                column: "InstructionUUID");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RecipeUUID",
                table: "Ingredients",
                column: "RecipeUUID");

            migrationBuilder.AddForeignKey(
                name: "FK_Instruction_Images_Instructions_InstructionUUID",
                table: "Instruction_Images",
                column: "InstructionUUID",
                principalTable: "Instructions",
                principalColumn: "UUID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Instruction_Videos_Instructions_InstructionUUID",
                table: "Instruction_Videos",
                column: "InstructionUUID",
                principalTable: "Instructions",
                principalColumn: "UUID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instruction_Images_Instructions_InstructionUUID",
                table: "Instruction_Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Instruction_Videos_Instructions_InstructionUUID",
                table: "Instruction_Videos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipe_Videos",
                table: "Recipe_Videos");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_Videos_RecipeUUID",
                table: "Recipe_Videos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipe_Images",
                table: "Recipe_Images");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_Images_RecipeUUID",
                table: "Recipe_Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_UserUUID",
                table: "Ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Policies",
                table: "Policies");

            migrationBuilder.DropIndex(
                name: "IX_Policies_UserUUID",
                table: "Policies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notes",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_RecipeUUID",
                table: "Notes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instructions",
                table: "Instructions");

            migrationBuilder.DropIndex(
                name: "IX_Instructions_RecipeUUID",
                table: "Instructions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instruction_Videos",
                table: "Instruction_Videos");

            migrationBuilder.DropIndex(
                name: "IX_Instruction_Videos_InstructionUUID",
                table: "Instruction_Videos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instruction_Images",
                table: "Instruction_Images");

            migrationBuilder.DropIndex(
                name: "IX_Instruction_Images_InstructionUUID",
                table: "Instruction_Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_RecipeUUID",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "UUID",
                table: "Recipe_Videos");

            migrationBuilder.DropColumn(
                name: "UUID",
                table: "Recipe_Images");

            migrationBuilder.DropColumn(
                name: "UUID",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "UUID",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "UUID",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "UUID",
                table: "Instructions");

            migrationBuilder.DropColumn(
                name: "UUID",
                table: "Instruction_Videos");

            migrationBuilder.DropColumn(
                name: "UUID",
                table: "Instruction_Images");

            migrationBuilder.DropColumn(
                name: "UUID",
                table: "Ingredients");

            migrationBuilder.AddColumn<Guid>(
                name: "InstructionRecipeUUID",
                table: "Instruction_Videos",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InstructionSequence_Number",
                table: "Instruction_Videos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InstructionRecipeUUID",
                table: "Instruction_Images",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InstructionSequence_Number",
                table: "Instruction_Images",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipe_Videos",
                table: "Recipe_Videos",
                columns: new[] { "RecipeUUID", "Video_Number" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipe_Images",
                table: "Recipe_Images",
                columns: new[] { "RecipeUUID", "Image_Number" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings",
                columns: new[] { "UserUUID", "RecipeUUID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Policies",
                table: "Policies",
                columns: new[] { "UserUUID", "RecipeUUID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notes",
                table: "Notes",
                columns: new[] { "RecipeUUID", "Note_Number" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instructions",
                table: "Instructions",
                columns: new[] { "RecipeUUID", "Sequence_Number" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instruction_Videos",
                table: "Instruction_Videos",
                columns: new[] { "InstructionUUID", "Video_Number" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instruction_Images",
                table: "Instruction_Images",
                columns: new[] { "InstructionUUID", "Image_Number" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients",
                columns: new[] { "RecipeUUID", "Ingredient_Number" });

            migrationBuilder.CreateIndex(
                name: "IX_Instruction_Videos_InstructionRecipeUUID_InstructionSequenc~",
                table: "Instruction_Videos",
                columns: new[] { "InstructionRecipeUUID", "InstructionSequence_Number" });

            migrationBuilder.CreateIndex(
                name: "IX_Instruction_Images_InstructionRecipeUUID_InstructionSequenc~",
                table: "Instruction_Images",
                columns: new[] { "InstructionRecipeUUID", "InstructionSequence_Number" });

            migrationBuilder.AddForeignKey(
                name: "FK_Instruction_Images_Instructions_InstructionRecipeUUID_Instr~",
                table: "Instruction_Images",
                columns: new[] { "InstructionRecipeUUID", "InstructionSequence_Number" },
                principalTable: "Instructions",
                principalColumns: new[] { "RecipeUUID", "Sequence_Number" });

            migrationBuilder.AddForeignKey(
                name: "FK_Instruction_Videos_Instructions_InstructionRecipeUUID_Instr~",
                table: "Instruction_Videos",
                columns: new[] { "InstructionRecipeUUID", "InstructionSequence_Number" },
                principalTable: "Instructions",
                principalColumns: new[] { "RecipeUUID", "Sequence_Number" });
        }
    }
}
