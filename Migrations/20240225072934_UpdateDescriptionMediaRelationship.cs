using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeSiteBackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDescriptionMediaRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Description_Medias_RecipeUUID",
                table: "Description_Medias");

            migrationBuilder.AddColumn<Guid>(
                name: "DescriptionMediaUUID",
                table: "Recipes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Description_Medias_RecipeUUID",
                table: "Description_Medias",
                column: "RecipeUUID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Description_Medias_RecipeUUID",
                table: "Description_Medias");

            migrationBuilder.DropColumn(
                name: "DescriptionMediaUUID",
                table: "Recipes");

            migrationBuilder.CreateIndex(
                name: "IX_Description_Medias_RecipeUUID",
                table: "Description_Medias",
                column: "RecipeUUID");
        }
    }
}
