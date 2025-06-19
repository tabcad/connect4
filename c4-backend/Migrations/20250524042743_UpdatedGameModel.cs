using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConnectFour.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedGameModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameBoard",
                table: "Games");

            migrationBuilder.AddColumn<string>(
                name: "Board",
                table: "Games",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Board",
                table: "Games");

            migrationBuilder.AddColumn<string>(
                name: "GameBoard",
                table: "Games",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
