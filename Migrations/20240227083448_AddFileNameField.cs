using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeSiteBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddFileNameField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Filename",
                table: "Instruction_Images",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Filename",
                table: "Description_Medias",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Filename",
                table: "Instruction_Images");

            migrationBuilder.DropColumn(
                name: "Filename",
                table: "Description_Medias");
        }
    }
}
