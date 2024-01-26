using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeSiteBackend.Migrations
{
    /// <inheritdoc />
    public partial class RemovedRatingsfieldfromrecipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "LastMofifiedDate",
                table: "Recipes",
                newName: "LastModifiedDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastModifiedDate",
                table: "Recipes",
                newName: "LastMofifiedDate");

            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                table: "Recipes",
                type: "numeric(2,1)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
