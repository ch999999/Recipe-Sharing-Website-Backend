using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeSiteBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddComposite_KeyforInstructions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instruction_Images_Instructions_InstructionUUID",
                table: "Instruction_Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Instruction_Videos_Instructions_InstructionUUID",
                table: "Instruction_Videos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instructions",
                table: "Instructions");

            migrationBuilder.DropIndex(
                name: "IX_Instruction_Videos_InstructionUUID",
                table: "Instruction_Videos");

            migrationBuilder.DropIndex(
                name: "IX_Instruction_Images_InstructionUUID",
                table: "Instruction_Images");

            migrationBuilder.AddColumn<int>(
                name: "InstructionSequence_Number",
                table: "Instruction_Videos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InstructionSequence_Number",
                table: "Instruction_Images",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instructions",
                table: "Instructions",
                columns: new[] { "UUID", "Sequence_Number" });

            migrationBuilder.CreateIndex(
                name: "IX_Instruction_Videos_InstructionUUID_InstructionSequence_Numb~",
                table: "Instruction_Videos",
                columns: new[] { "InstructionUUID", "InstructionSequence_Number" });

            migrationBuilder.CreateIndex(
                name: "IX_Instruction_Images_InstructionUUID_InstructionSequence_Numb~",
                table: "Instruction_Images",
                columns: new[] { "InstructionUUID", "InstructionSequence_Number" });

            migrationBuilder.AddForeignKey(
                name: "FK_Instruction_Images_Instructions_InstructionUUID_Instruction~",
                table: "Instruction_Images",
                columns: new[] { "InstructionUUID", "InstructionSequence_Number" },
                principalTable: "Instructions",
                principalColumns: new[] { "UUID", "Sequence_Number" });

            migrationBuilder.AddForeignKey(
                name: "FK_Instruction_Videos_Instructions_InstructionUUID_Instruction~",
                table: "Instruction_Videos",
                columns: new[] { "InstructionUUID", "InstructionSequence_Number" },
                principalTable: "Instructions",
                principalColumns: new[] { "UUID", "Sequence_Number" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instruction_Images_Instructions_InstructionUUID_Instruction~",
                table: "Instruction_Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Instruction_Videos_Instructions_InstructionUUID_Instruction~",
                table: "Instruction_Videos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instructions",
                table: "Instructions");

            migrationBuilder.DropIndex(
                name: "IX_Instruction_Videos_InstructionUUID_InstructionSequence_Numb~",
                table: "Instruction_Videos");

            migrationBuilder.DropIndex(
                name: "IX_Instruction_Images_InstructionUUID_InstructionSequence_Numb~",
                table: "Instruction_Images");

            migrationBuilder.DropColumn(
                name: "InstructionSequence_Number",
                table: "Instruction_Videos");

            migrationBuilder.DropColumn(
                name: "InstructionSequence_Number",
                table: "Instruction_Images");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instructions",
                table: "Instructions",
                column: "UUID");

            migrationBuilder.CreateIndex(
                name: "IX_Instruction_Videos_InstructionUUID",
                table: "Instruction_Videos",
                column: "InstructionUUID");

            migrationBuilder.CreateIndex(
                name: "IX_Instruction_Images_InstructionUUID",
                table: "Instruction_Images",
                column: "InstructionUUID");

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
    }
}
