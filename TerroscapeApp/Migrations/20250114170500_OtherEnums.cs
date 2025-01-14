using Microsoft.EntityFrameworkCore.Migrations;
using TerroscapeApp.Models;

#nullable disable

namespace TerroscapeApp.Migrations
{
    /// <inheritdoc />
    public partial class OtherEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DBEnums.SurvivorStateEnum>(
                name: "state",
                table: "survivor",
                type: "survivor_state_enum",
                nullable: false,
                defaultValue: DBEnums.SurvivorStateEnum.Alive);

            migrationBuilder.AddColumn<DBEnums.KillerWinEnum>(
                name: "how_killer_win",
                table: "round",
                type: "killer_win_enum",
                nullable: true);

            migrationBuilder.AddColumn<DBEnums.SurvivorWinEnum>(
                name: "how_survivors_win",
                table: "round",
                type: "survivor_win_enum",
                nullable: true);

            migrationBuilder.AddColumn<DBEnums.GameNameEnum>(
                name: "game_name",
                table: "avatar",
                type: "game_name_enum",
                nullable: false,
                defaultValue: DBEnums.GameNameEnum.Base);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "state",
                table: "survivor");

            migrationBuilder.DropColumn(
                name: "how_killer_win",
                table: "round");

            migrationBuilder.DropColumn(
                name: "how_survivors_win",
                table: "round");

            migrationBuilder.DropColumn(
                name: "game_name",
                table: "avatar");
        }
    }
}
