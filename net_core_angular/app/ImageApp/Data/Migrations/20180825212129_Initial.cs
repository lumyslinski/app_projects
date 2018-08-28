using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageApp.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImageModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Length = table.Column<long>(nullable: false),
                    Width = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    ContentImageUrl = table.Column<string>(nullable: false),
                    ContentImageUrlThumb = table.Column<string>(nullable: false),
                    ContentType = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImageModelDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImageId = table.Column<int>(nullable: false),
                    Mid = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Score = table.Column<double>(nullable: false),
                    Topicality = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageModelDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageModelDetails_ImageModel_ImageId",
                        column: x => x.ImageId,
                        principalTable: "ImageModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageModelDetailsWeb",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImageId = table.Column<int>(nullable: false),
                    EntityId = table.Column<string>(nullable: true),
                    Score = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageModelDetailsWeb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageModelDetailsWeb_ImageModel_ImageId",
                        column: x => x.ImageId,
                        principalTable: "ImageModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageModelWebMatches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImageId = table.Column<int>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    UrlImage = table.Column<string>(nullable: true),
                    PageTitle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageModelWebMatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageModelWebMatches_ImageModel_ImageId",
                        column: x => x.ImageId,
                        principalTable: "ImageModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageModelDetails_ImageId",
                table: "ImageModelDetails",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageModelDetailsWeb_ImageId",
                table: "ImageModelDetailsWeb",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageModelWebMatches_ImageId",
                table: "ImageModelWebMatches",
                column: "ImageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageModelDetails");

            migrationBuilder.DropTable(
                name: "ImageModelDetailsWeb");

            migrationBuilder.DropTable(
                name: "ImageModelWebMatches");

            migrationBuilder.DropTable(
                name: "ImageModel");
        }
    }
}
