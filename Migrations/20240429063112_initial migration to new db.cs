using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeSiteBackend.Migrations
{
    /// <inheritdoc />
    public partial class initialmigrationtonewdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserRefreshTokens",
                columns: table => new
                {
                    UUID = table.Column<Guid>(type: "uuid", nullable: false),
                    UserUUID = table.Column<Guid>(type: "uuid", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: false),
                    ValidUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRefreshTokens", x => x.UUID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UUID = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Firstname = table.Column<string>(type: "text", nullable: false),
                    Lastname = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UUID);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    UUID = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Prep_Time_Mins = table.Column<int>(type: "integer", nullable: false),
                    Cook_Time_Mins = table.Column<int>(type: "integer", nullable: false),
                    Servings = table.Column<int>(type: "integer", nullable: false),
                    OwnerUUID = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsViewableByPublic = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.UUID);
                    table.ForeignKey(
                        name: "FK_Recipes_Users_OwnerUUID",
                        column: x => x.OwnerUUID,
                        principalTable: "Users",
                        principalColumn: "UUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Description_Medias",
                columns: table => new
                {
                    UUID = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Filetype = table.Column<string>(type: "text", nullable: true),
                    RecipeUUID = table.Column<Guid>(type: "uuid", nullable: false),
                    Filename = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Description_Medias", x => x.UUID);
                    table.ForeignKey(
                        name: "FK_Description_Medias_Recipes_RecipeUUID",
                        column: x => x.RecipeUUID,
                        principalTable: "Recipes",
                        principalColumn: "UUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    UUID = table.Column<Guid>(type: "uuid", nullable: false),
                    Ingredient_Number = table.Column<int>(type: "integer", nullable: false),
                    RecipeUUID = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.UUID);
                    table.ForeignKey(
                        name: "FK_Ingredients_Recipes_RecipeUUID",
                        column: x => x.RecipeUUID,
                        principalTable: "Recipes",
                        principalColumn: "UUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instructions",
                columns: table => new
                {
                    UUID = table.Column<Guid>(type: "uuid", nullable: false),
                    Sequence_Number = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    RecipeUUID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructions", x => x.UUID);
                    table.ForeignKey(
                        name: "FK_Instructions_Recipes_RecipeUUID",
                        column: x => x.RecipeUUID,
                        principalTable: "Recipes",
                        principalColumn: "UUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    UUID = table.Column<Guid>(type: "uuid", nullable: false),
                    Note_Number = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    RecipeUUID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.UUID);
                    table.ForeignKey(
                        name: "FK_Notes_Recipes_RecipeUUID",
                        column: x => x.RecipeUUID,
                        principalTable: "Recipes",
                        principalColumn: "UUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Policies",
                columns: table => new
                {
                    UUID = table.Column<Guid>(type: "uuid", nullable: false),
                    Permission_Level = table.Column<string>(type: "text", nullable: true),
                    UserUUID = table.Column<Guid>(type: "uuid", nullable: false),
                    RecipeUUID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policies", x => x.UUID);
                    table.ForeignKey(
                        name: "FK_Policies_Recipes_RecipeUUID",
                        column: x => x.RecipeUUID,
                        principalTable: "Recipes",
                        principalColumn: "UUID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Policies_Users_UserUUID",
                        column: x => x.UserUUID,
                        principalTable: "Users",
                        principalColumn: "UUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instruction_Images",
                columns: table => new
                {
                    UUID = table.Column<Guid>(type: "uuid", nullable: false),
                    Image_Number = table.Column<int>(type: "integer", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: true),
                    Filename = table.Column<string>(type: "text", nullable: true),
                    InstructionUUID = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instruction_Images", x => x.UUID);
                    table.ForeignKey(
                        name: "FK_Instruction_Images_Instructions_InstructionUUID",
                        column: x => x.InstructionUUID,
                        principalTable: "Instructions",
                        principalColumn: "UUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Description_Medias_RecipeUUID",
                table: "Description_Medias",
                column: "RecipeUUID");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RecipeUUID_Ingredient_Number",
                table: "Ingredients",
                columns: new[] { "RecipeUUID", "Ingredient_Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instruction_Images_InstructionUUID_Image_Number",
                table: "Instruction_Images",
                columns: new[] { "InstructionUUID", "Image_Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_RecipeUUID_Sequence_Number",
                table: "Instructions",
                columns: new[] { "RecipeUUID", "Sequence_Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_RecipeUUID_Note_Number",
                table: "Notes",
                columns: new[] { "RecipeUUID", "Note_Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Policies_RecipeUUID_UserUUID",
                table: "Policies",
                columns: new[] { "RecipeUUID", "UserUUID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Policies_UserUUID",
                table: "Policies",
                column: "UserUUID");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_OwnerUUID",
                table: "Recipes",
                column: "OwnerUUID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Description_Medias");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Instruction_Images");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Policies");

            migrationBuilder.DropTable(
                name: "UserRefreshTokens");

            migrationBuilder.DropTable(
                name: "Instructions");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
