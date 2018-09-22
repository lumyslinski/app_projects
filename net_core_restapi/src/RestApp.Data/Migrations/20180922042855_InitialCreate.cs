using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RestApp.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Episode",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacterFriend",
                columns: table => new
                {
                    CharacterId = table.Column<int>(nullable: false),
                    FriendId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterFriend", x => new { x.CharacterId, x.FriendId });
                    table.ForeignKey(
                        name: "FK_CharacterFriend_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CharacterFriend_Character_FriendId",
                        column: x => x.FriendId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterEpisode",
                columns: table => new
                {
                    CharacterId = table.Column<int>(nullable: false),
                    EpisodeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterEpisode", x => new { x.CharacterId, x.EpisodeId });
                    table.ForeignKey(
                        name: "FK_CharacterEpisode_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CharacterEpisode_Episode_EpisodeId",
                        column: x => x.EpisodeId,
                        principalTable: "Episode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Character",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Luke Skywalker" },
                    { 2, "Darth Vader" },
                    { 3, "Han Solo" },
                    { 4, "Leia Organa" },
                    { 5, "Wilhuff Tarkin" },
                    { 6, "C-3PO" },
                    { 7, "R2-D2" }
                });

            migrationBuilder.InsertData(
                table: "Episode",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "NEWHOPE" },
                    { 2, "EMPIRE" },
                    { 3, "JEDI" }
                });

            migrationBuilder.InsertData(
                table: "CharacterEpisode",
                columns: new[] { "CharacterId", "EpisodeId" },
                values: new object[,]
                {
                    { 7, 3 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 1 },
                    { 6, 1 },
                    { 7, 1 },
                    { 1, 2 },
                    { 1, 1 },
                    { 2, 2 },
                    { 4, 2 },
                    { 6, 2 },
                    { 7, 2 },
                    { 1, 3 },
                    { 2, 3 },
                    { 3, 3 },
                    { 4, 3 },
                    { 3, 2 },
                    { 6, 3 }
                });

            migrationBuilder.InsertData(
                table: "CharacterFriend",
                columns: new[] { "CharacterId", "FriendId" },
                values: new object[,]
                {
                    { 7, 4 },
                    { 7, 1 },
                    { 3, 1 },
                    { 1, 4 },
                    { 3, 4 },
                    { 4, 1 },
                    { 4, 3 },
                    { 2, 5 },
                    { 5, 2 },
                    { 7, 3 },
                    { 1, 6 },
                    { 6, 1 },
                    { 6, 3 },
                    { 6, 4 },
                    { 1, 7 },
                    { 3, 7 },
                    { 4, 7 },
                    { 6, 7 },
                    { 4, 6 },
                    { 1, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterEpisode_EpisodeId",
                table: "CharacterEpisode",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterFriend_FriendId",
                table: "CharacterFriend",
                column: "FriendId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterEpisode");

            migrationBuilder.DropTable(
                name: "CharacterFriend");

            migrationBuilder.DropTable(
                name: "Episode");

            migrationBuilder.DropTable(
                name: "Character");
        }
    }
}
