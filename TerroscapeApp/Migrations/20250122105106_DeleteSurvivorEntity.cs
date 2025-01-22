using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TerroscapeApp.Models;

#nullable disable

namespace TerroscapeApp.Migrations
{
    /// <inheritdoc />
    public partial class DeleteSurvivorEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "round_first_survivor_fk",
                table: "round");

            migrationBuilder.DropForeignKey(
                name: "round_second_survivor_fk",
                table: "round");

            migrationBuilder.DropForeignKey(
                name: "round_third_survivor_fk",
                table: "round");

            migrationBuilder.DropTable(
                name: "survivor");

            migrationBuilder.RenameColumn(
                name: "third_survivor",
                table: "round",
                newName: "third_survivor_avatar");

            migrationBuilder.RenameColumn(
                name: "second_survivor",
                table: "round",
                newName: "second_survivor_avatar");

            migrationBuilder.RenameColumn(
                name: "first_survivor",
                table: "round",
                newName: "first_survivor_player");

            migrationBuilder.RenameIndex(
                name: "IX_round_third_survivor",
                table: "round",
                newName: "IX_round_third_survivor_avatar");

            migrationBuilder.RenameIndex(
                name: "IX_round_second_survivor",
                table: "round",
                newName: "IX_round_second_survivor_avatar");

            migrationBuilder.RenameIndex(
                name: "IX_round_first_survivor",
                table: "round",
                newName: "IX_round_first_survivor_player");

            migrationBuilder.AddColumn<DateTime>(
                name: "date",
                table: "round",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "first_survivor_avatar",
                table: "round",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DBEnums.SurvivorStateEnum>(
                name: "first_survivor_state",
                table: "round",
                type: "survivor_state_enum",
                nullable: false,
                defaultValue: DBEnums.SurvivorStateEnum.Alive);

            migrationBuilder.AddColumn<int>(
                name: "second_survivor_player",
                table: "round",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DBEnums.SurvivorStateEnum>(
                name: "second_survivor_state",
                table: "round",
                type: "survivor_state_enum",
                nullable: false,
                defaultValue: DBEnums.SurvivorStateEnum.Alive);

            migrationBuilder.AddColumn<int>(
                name: "third_survivor_player",
                table: "round",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DBEnums.SurvivorStateEnum>(
                name: "third_survivor_state",
                table: "round",
                type: "survivor_state_enum",
                nullable: false,
                defaultValue: DBEnums.SurvivorStateEnum.Alive);

            migrationBuilder.CreateIndex(
                name: "IX_round_first_survivor_avatar",
                table: "round",
                column: "first_survivor_avatar");

            migrationBuilder.CreateIndex(
                name: "IX_round_second_survivor_player",
                table: "round",
                column: "second_survivor_player");

            migrationBuilder.CreateIndex(
                name: "IX_round_third_survivor_player",
                table: "round",
                column: "third_survivor_player");

            migrationBuilder.AddForeignKey(
                name: "round_first_avatar_fk",
                table: "round",
                column: "first_survivor_avatar",
                principalTable: "avatar",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade,
                onUpdate: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "round_first_player_fk",
                table: "round",
                column: "first_survivor_player",
                principalTable: "player",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade,
                onUpdate: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "round_second_avatar_fk",
                table: "round",
                column: "second_survivor_avatar",
                principalTable: "avatar",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade,
                onUpdate: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "round_second_player_fk",
                table: "round",
                column: "second_survivor_player",
                principalTable: "player",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade, 
                onUpdate: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "round_third_avatar_fk",
                table: "round",
                column: "third_survivor_avatar",
                principalTable: "avatar",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade, 
                onUpdate: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "round_third_player_fk",
                table: "round",
                column: "third_survivor_player",
                principalTable: "player",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade, 
                onUpdate: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "round_first_avatar_fk",
                table: "round");

            migrationBuilder.DropForeignKey(
                name: "round_first_player_fk",
                table: "round");

            migrationBuilder.DropForeignKey(
                name: "round_second_avatar_fk",
                table: "round");

            migrationBuilder.DropForeignKey(
                name: "round_second_player_fk",
                table: "round");

            migrationBuilder.DropForeignKey(
                name: "round_third_avatar_fk",
                table: "round");

            migrationBuilder.DropForeignKey(
                name: "round_third_player_fk",
                table: "round");

            migrationBuilder.DropIndex(
                name: "IX_round_first_survivor_avatar",
                table: "round");

            migrationBuilder.DropIndex(
                name: "IX_round_second_survivor_player",
                table: "round");

            migrationBuilder.DropIndex(
                name: "IX_round_third_survivor_player",
                table: "round");

            migrationBuilder.DropColumn(
                name: "date",
                table: "round");

            migrationBuilder.DropColumn(
                name: "first_survivor_avatar",
                table: "round");

            migrationBuilder.DropColumn(
                name: "first_survivor_state",
                table: "round");

            migrationBuilder.DropColumn(
                name: "second_survivor_player",
                table: "round");

            migrationBuilder.DropColumn(
                name: "second_survivor_state",
                table: "round");

            migrationBuilder.DropColumn(
                name: "third_survivor_player",
                table: "round");

            migrationBuilder.DropColumn(
                name: "third_survivor_state",
                table: "round");

            migrationBuilder.RenameColumn(
                name: "third_survivor_avatar",
                table: "round",
                newName: "third_survivor");

            migrationBuilder.RenameColumn(
                name: "second_survivor_avatar",
                table: "round",
                newName: "second_survivor");

            migrationBuilder.RenameColumn(
                name: "first_survivor_player",
                table: "round",
                newName: "first_survivor");

            migrationBuilder.RenameIndex(
                name: "IX_round_third_survivor_avatar",
                table: "round",
                newName: "IX_round_third_survivor");

            migrationBuilder.RenameIndex(
                name: "IX_round_second_survivor_avatar",
                table: "round",
                newName: "IX_round_second_survivor");

            migrationBuilder.RenameIndex(
                name: "IX_round_first_survivor_player",
                table: "round",
                newName: "IX_round_first_survivor");

            migrationBuilder.CreateTable(
                name: "survivor",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    avatar_id = table.Column<int>(type: "integer", nullable: false),
                    player_id = table.Column<int>(type: "integer", nullable: true),
                    state = table.Column<DBEnums.SurvivorStateEnum>(type: "survivor_state_enum", nullable: false, defaultValue: DBEnums.SurvivorStateEnum.Alive)
                },
                constraints: table =>
                {
                    table.PrimaryKey("survivor_id_pk", x => x.id);
                    table.ForeignKey(
                        name: "survivor_avatar_fk",
                        column: x => x.avatar_id,
                        principalTable: "avatar",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "survivor_player_fk",
                        column: x => x.player_id,
                        principalTable: "player",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_survivor_avatar_id",
                table: "survivor",
                column: "avatar_id");

            migrationBuilder.CreateIndex(
                name: "IX_survivor_player_id",
                table: "survivor",
                column: "player_id");

            migrationBuilder.AddForeignKey(
                name: "round_first_survivor_fk",
                table: "round",
                column: "first_survivor",
                principalTable: "survivor",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "round_second_survivor_fk",
                table: "round",
                column: "second_survivor",
                principalTable: "survivor",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "round_third_survivor_fk",
                table: "round",
                column: "third_survivor",
                principalTable: "survivor",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
