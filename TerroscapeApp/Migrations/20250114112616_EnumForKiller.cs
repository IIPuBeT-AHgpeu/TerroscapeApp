using Microsoft.EntityFrameworkCore.Migrations;
using TerroscapeApp.Models;

#nullable disable

namespace TerroscapeApp.Migrations
{
    /// <inheritdoc />
    public partial class EnumForKiller : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:game_name_enum", "base,feral_instincts,amorphous_peril,lethal_immortals,putrefied_enmity")
                .Annotation("Npgsql:Enum:killer_win_enum", "murder,time,other")
                .Annotation("Npgsql:Enum:survivor_state_enum", "alive,injured,dead")
                .Annotation("Npgsql:Enum:survivor_win_enum", "escape,police,plan,other")
                .OldAnnotation("Npgsql:Enum:game_name", "base,feral_instincts,amorphous_peril,lethal_immortals,putrefied_enmity")
                .OldAnnotation("Npgsql:Enum:killer_win", "murder,time,other")
                .OldAnnotation("Npgsql:Enum:survivor_state", "alive,injured,dead")
                .OldAnnotation("Npgsql:Enum:survivor_win", "escape,police,plan,other");

            migrationBuilder.AddColumn<DBEnums.GameNameEnum>(
                name: "game_name",
                table: "killer",
                type: "game_name_enum",
                nullable: false,
                defaultValue: DBEnums.GameNameEnum.Base);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "game_name",
                table: "killer");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:game_name", "base,feral_instincts,amorphous_peril,lethal_immortals,putrefied_enmity")
                .Annotation("Npgsql:Enum:killer_win", "murder,time,other")
                .Annotation("Npgsql:Enum:survivor_state", "alive,injured,dead")
                .Annotation("Npgsql:Enum:survivor_win", "escape,police,plan,other")
                .OldAnnotation("Npgsql:Enum:game_name_enum", "base,feral_instincts,amorphous_peril,lethal_immortals,putrefied_enmity")
                .OldAnnotation("Npgsql:Enum:killer_win_enum", "murder,time,other")
                .OldAnnotation("Npgsql:Enum:survivor_state_enum", "alive,injured,dead")
                .OldAnnotation("Npgsql:Enum:survivor_win_enum", "escape,police,plan,other");
        }
    }
}
