using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConnectFour.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGameModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BoardState",
                table: "Games",
                newName: "GameBoard");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GameBoard",
                table: "Games",
                newName: "BoardState");
        }
    }
}
