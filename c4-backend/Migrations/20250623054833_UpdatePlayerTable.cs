using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConnectFour.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePlayerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Players_CurrentTurn",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Players_PlayerOne",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Players_PlayerTwo",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Players_Winner",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "PlayerOne",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "PlayerTwo",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "Winner",
                table: "Games",
                newName: "WinnerId");

            migrationBuilder.RenameColumn(
                name: "PlayerTwo",
                table: "Games",
                newName: "PlayerTwoId");

            migrationBuilder.RenameColumn(
                name: "PlayerOne",
                table: "Games",
                newName: "PlayerOneId");

            migrationBuilder.RenameColumn(
                name: "CurrentTurn",
                table: "Games",
                newName: "CurrentTurnId");

            migrationBuilder.RenameIndex(
                name: "IX_Games_Winner",
                table: "Games",
                newName: "IX_Games_WinnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Games_PlayerTwo",
                table: "Games",
                newName: "IX_Games_PlayerTwoId");

            migrationBuilder.RenameIndex(
                name: "IX_Games_PlayerOne",
                table: "Games",
                newName: "IX_Games_PlayerOneId");

            migrationBuilder.RenameIndex(
                name: "IX_Games_CurrentTurn",
                table: "Games",
                newName: "IX_Games_CurrentTurnId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Players_CurrentTurnId",
                table: "Games",
                column: "CurrentTurnId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Players_PlayerOneId",
                table: "Games",
                column: "PlayerOneId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Players_PlayerTwoId",
                table: "Games",
                column: "PlayerTwoId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Players_WinnerId",
                table: "Games",
                column: "WinnerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Players_CurrentTurnId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Players_PlayerOneId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Players_PlayerTwoId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Players_WinnerId",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "WinnerId",
                table: "Games",
                newName: "Winner");

            migrationBuilder.RenameColumn(
                name: "PlayerTwoId",
                table: "Games",
                newName: "PlayerTwo");

            migrationBuilder.RenameColumn(
                name: "PlayerOneId",
                table: "Games",
                newName: "PlayerOne");

            migrationBuilder.RenameColumn(
                name: "CurrentTurnId",
                table: "Games",
                newName: "CurrentTurn");

            migrationBuilder.RenameIndex(
                name: "IX_Games_WinnerId",
                table: "Games",
                newName: "IX_Games_Winner");

            migrationBuilder.RenameIndex(
                name: "IX_Games_PlayerTwoId",
                table: "Games",
                newName: "IX_Games_PlayerTwo");

            migrationBuilder.RenameIndex(
                name: "IX_Games_PlayerOneId",
                table: "Games",
                newName: "IX_Games_PlayerOne");

            migrationBuilder.RenameIndex(
                name: "IX_Games_CurrentTurnId",
                table: "Games",
                newName: "IX_Games_CurrentTurn");

            migrationBuilder.AddColumn<Guid>(
                name: "PlayerOne",
                table: "Rooms",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PlayerTwo",
                table: "Rooms",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Players_CurrentTurn",
                table: "Games",
                column: "CurrentTurn",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Players_PlayerOne",
                table: "Games",
                column: "PlayerOne",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Players_PlayerTwo",
                table: "Games",
                column: "PlayerTwo",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Players_Winner",
                table: "Games",
                column: "Winner",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
