using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeSiteBackend.Migrations
{
    /// <inheritdoc />
    public partial class correctuniquecompositekeyfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Instruction_Images_InstructionUUID",
                table: "Instruction_Images");

            migrationBuilder.DropIndex(
                name: "IX_Instruction_Images_UUID_Image_Number",
                table: "Instruction_Images");

            migrationBuilder.CreateIndex(
                name: "IX_Instruction_Images_InstructionUUID_Image_Number",
                table: "Instruction_Images",
                columns: new[] { "InstructionUUID", "Image_Number" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Instruction_Images_InstructionUUID_Image_Number",
                table: "Instruction_Images");

            migrationBuilder.CreateIndex(
                name: "IX_Instruction_Images_InstructionUUID",
                table: "Instruction_Images",
                column: "InstructionUUID");

            migrationBuilder.CreateIndex(
                name: "IX_Instruction_Images_UUID_Image_Number",
                table: "Instruction_Images",
                columns: new[] { "UUID", "Image_Number" },
                unique: true);
        }
    }
}
