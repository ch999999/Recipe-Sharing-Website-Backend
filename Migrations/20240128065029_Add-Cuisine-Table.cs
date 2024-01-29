using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RecipeSiteBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddCuisineTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cuisine",
                table: "Recipes");

            migrationBuilder.AddColumn<int>(
                name: "CuisineId",
                table: "Recipes",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cuisine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cuisine_Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuisine", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Tag_Name",
                table: "Tags",
                column: "Tag_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_CuisineId",
                table: "Recipes",
                column: "CuisineId");

            migrationBuilder.CreateIndex(
                name: "IX_Diets_Diet_Name",
                table: "Diets",
                column: "Diet_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cuisine_Cuisine_Name",
                table: "Cuisine",
                column: "Cuisine_Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Cuisine_CuisineId",
                table: "Recipes",
                column: "CuisineId",
                principalTable: "Cuisine",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Cuisine_CuisineId",
                table: "Recipes");

            migrationBuilder.DropTable(
                name: "Cuisine");

            migrationBuilder.DropIndex(
                name: "IX_Tags_Tag_Name",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_CuisineId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Diets_Diet_Name",
                table: "Diets");

            migrationBuilder.DropColumn(
                name: "CuisineId",
                table: "Recipes");

            migrationBuilder.AddColumn<string>(
                name: "Cuisine",
                table: "Recipes",
                type: "text",
                nullable: true);
        }
    }
}
