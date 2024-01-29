using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeSiteBackend.Migrations
{
    /// <inheritdoc />
    public partial class CompositeKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipe_Videos",
                table: "Recipe_Videos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipe_Images",
                table: "Recipe_Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instruction_Videos",
                table: "Instruction_Videos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instruction_Images",
                table: "Instruction_Images");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipe_Videos",
                table: "Recipe_Videos",
                columns: new[] { "UUID", "Video_Number" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipe_Images",
                table: "Recipe_Images",
                columns: new[] { "UUID", "Image_Number" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instruction_Videos",
                table: "Instruction_Videos",
                columns: new[] { "UUID", "Video_Number" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instruction_Images",
                table: "Instruction_Images",
                columns: new[] { "UUID", "Image_Number" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipe_Videos",
                table: "Recipe_Videos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipe_Images",
                table: "Recipe_Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instruction_Videos",
                table: "Instruction_Videos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instruction_Images",
                table: "Instruction_Images");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipe_Videos",
                table: "Recipe_Videos",
                column: "UUID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipe_Images",
                table: "Recipe_Images",
                column: "UUID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instruction_Videos",
                table: "Instruction_Videos",
                column: "UUID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instruction_Images",
                table: "Instruction_Images",
                column: "UUID");
        }
    }
}
