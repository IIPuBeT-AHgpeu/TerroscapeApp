using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TerroscapeApp.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:game_name", "base,feral_instincts,amorphous_peril,lethal_immortals,putrefied_enmity")
                .Annotation("Npgsql:Enum:killer_win", "murder,time,other")
                .Annotation("Npgsql:Enum:survivor_state", "alive,injured,dead")
                .Annotation("Npgsql:Enum:survivor_win", "escape,police,plan,other");

            migrationBuilder.CreateTable(
                name: "avatar",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    first_skill = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    second_skill = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("survivor_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "killer",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    strength = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("killer_id_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "map",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    locations_num = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("map_id_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "player",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    login = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("player_id_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "survivor",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    player_id = table.Column<int>(type: "integer", nullable: true),
                    avatar_id = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "round",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    map_id = table.Column<int>(type: "integer", nullable: false),
                    killer_id = table.Column<int>(type: "integer", nullable: false),
                    killer_player_id = table.Column<int>(type: "integer", nullable: false),
                    killer_boost_num = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    killer_win = table.Column<bool>(type: "boolean", nullable: false),
                    killer_level = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    first_survivor = table.Column<int>(type: "integer", nullable: false),
                    second_survivor = table.Column<int>(type: "integer", nullable: false),
                    third_survivor = table.Column<int>(type: "integer", nullable: false),
                    survivor_boost_num = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    has_plans = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    got_keys = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    done_radio = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    done_plan = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("round_id_pk", x => x.id);
                    table.ForeignKey(
                        name: "round_first_survivor_fk",
                        column: x => x.first_survivor,
                        principalTable: "survivor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "round_killer_fk",
                        column: x => x.killer_id,
                        principalTable: "killer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "round_killer_player_fk",
                        column: x => x.killer_player_id,
                        principalTable: "player",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "round_map_fk",
                        column: x => x.map_id,
                        principalTable: "map",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "round_second_survivor_fk",
                        column: x => x.second_survivor,
                        principalTable: "survivor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "round_third_survivor_fk",
                        column: x => x.third_survivor,
                        principalTable: "survivor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "map_name_uq",
                table: "map",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "player_login_uq",
                table: "player",
                column: "login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_round_first_survivor",
                table: "round",
                column: "first_survivor");

            migrationBuilder.CreateIndex(
                name: "IX_round_killer_id",
                table: "round",
                column: "killer_id");

            migrationBuilder.CreateIndex(
                name: "IX_round_killer_player_id",
                table: "round",
                column: "killer_player_id");

            migrationBuilder.CreateIndex(
                name: "IX_round_map_id",
                table: "round",
                column: "map_id");

            migrationBuilder.CreateIndex(
                name: "IX_round_second_survivor",
                table: "round",
                column: "second_survivor");

            migrationBuilder.CreateIndex(
                name: "IX_round_third_survivor",
                table: "round",
                column: "third_survivor");

            migrationBuilder.CreateIndex(
                name: "IX_survivor_avatar_id",
                table: "survivor",
                column: "avatar_id");

            migrationBuilder.CreateIndex(
                name: "IX_survivor_player_id",
                table: "survivor",
                column: "player_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "round");

            migrationBuilder.DropTable(
                name: "survivor");

            migrationBuilder.DropTable(
                name: "killer");

            migrationBuilder.DropTable(
                name: "map");

            migrationBuilder.DropTable(
                name: "avatar");

            migrationBuilder.DropTable(
                name: "player");
        }
    }
}
