using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UrlShortener.WebApi.Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "core");

            migrationBuilder.CreateSequence(
                name: "UrlDetailsSeq",
                schema: "core",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "UrlRequestSeq",
                schema: "core",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "UrlSeq",
                schema: "core",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "UrlDetails",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    UrlId = table.Column<int>(nullable: false),
                    RequestCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Url",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    LongUrl = table.Column<string>(maxLength: 2083, nullable: false),
                    ShortUrl = table.Column<string>(maxLength: 8, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Url", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Url_UrlDetails_Id",
                        column: x => x.Id,
                        principalSchema: "core",
                        principalTable: "UrlDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UrlRequest",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    UrlId = table.Column<int>(nullable: false),
                    AccessedOn = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UrlRequest_Url_UrlId",
                        column: x => x.UrlId,
                        principalSchema: "core",
                        principalTable: "Url",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Url_ShortUrl",
                schema: "core",
                table: "Url",
                column: "ShortUrl",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UrlRequest_UrlId",
                schema: "core",
                table: "UrlRequest",
                column: "UrlId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrlRequest",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Url",
                schema: "core");

            migrationBuilder.DropTable(
                name: "UrlDetails",
                schema: "core");

            migrationBuilder.DropSequence(
                name: "UrlDetailsSeq",
                schema: "core");

            migrationBuilder.DropSequence(
                name: "UrlRequestSeq",
                schema: "core");

            migrationBuilder.DropSequence(
                name: "UrlSeq",
                schema: "core");
        }
    }
}
