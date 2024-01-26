using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeSiteBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddRatingstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    UserUUID = table.Column<string>(type: "text", nullable: false),
                    RecipeUUID = table.Column<string>(type: "text", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_RecipeUUID",
                table: "Ratings",
                column: "RecipeUUID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ratings");
        }
    }
}
