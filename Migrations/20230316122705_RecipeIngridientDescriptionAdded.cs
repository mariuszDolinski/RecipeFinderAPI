using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeFinderAPI.Migrations
{
    /// <inheritdoc />
    public partial class RecipeIngridientDescriptionAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RecipeIngridients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Ingridients_Name",
                table: "Ingridients",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ingridients_Name",
                table: "Ingridients");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "RecipeIngridients");
        }
    }
}
