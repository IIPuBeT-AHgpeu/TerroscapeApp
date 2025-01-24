using Microsoft.EntityFrameworkCore.Migrations;
using TerroscapeApp.Models;

#nullable disable

namespace TerroscapeApp.Migrations
{
    /// <inheritdoc />
    public partial class MergeWinEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "how_killer_win",
                table: "round");

            migrationBuilder.DropColumn(
                name: "how_survivors_win",
                table: "round");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:game_name_enum", "base,feral_instincts,amorphous_peril,lethal_immortals,putrefied_enmity")
                .Annotation("Npgsql:Enum:survivor_state_enum", "alive,injured,dead")
                .Annotation("Npgsql:Enum:win_enum", "murder,time,escape,police,plan,other")
                .OldAnnotation("Npgsql:Enum:game_name_enum", "base,feral_instincts,amorphous_peril,lethal_immortals,putrefied_enmity")
                .OldAnnotation("Npgsql:Enum:killer_win_enum", "murder,time,other")
                .OldAnnotation("Npgsql:Enum:survivor_state_enum", "alive,injured,dead")
                .OldAnnotation("Npgsql:Enum:survivor_win_enum", "escape,police,plan,other");

            migrationBuilder.AddColumn<DBEnums.WinEnum>(
                name: "win_way",
                table: "round",
                type: "win_enum",
                nullable: false,
                defaultValue: DBEnums.WinEnum.Murder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "win_way",
                table: "round");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:game_name_enum", "base,feral_instincts,amorphous_peril,lethal_immortals,putrefied_enmity")
                .Annotation("Npgsql:Enum:killer_win_enum", "murder,time,other")
                .Annotation("Npgsql:Enum:survivor_state_enum", "alive,injured,dead")
                .Annotation("Npgsql:Enum:survivor_win_enum", "escape,police,plan,other")
                .OldAnnotation("Npgsql:Enum:game_name_enum", "base,feral_instincts,amorphous_peril,lethal_immortals,putrefied_enmity")
                .OldAnnotation("Npgsql:Enum:survivor_state_enum", "alive,injured,dead")
                .OldAnnotation("Npgsql:Enum:win_enum", "murder,time,escape,police,plan,other");

            migrationBuilder.AddColumn<int>(
                name: "how_killer_win",
                table: "round",
                type: "killer_win_enum",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "how_survivors_win",
                table: "round",
                type: "survivor_win_enum",
                nullable: true);
        }
    }
}
