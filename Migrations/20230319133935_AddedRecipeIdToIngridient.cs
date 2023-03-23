using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeFinderAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedRecipeIdToIngridient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngridients_Recipes_RecipeId",
                table: "RecipeIngridients");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeId",
                table: "RecipeIngridients",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngridients_Recipes_RecipeId",
                table: "RecipeIngridients",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngridients_Recipes_RecipeId",
                table: "RecipeIngridients");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeId",
                table: "RecipeIngridients",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngridients_Recipes_RecipeId",
                table: "RecipeIngridients",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id");
        }
    }
}
