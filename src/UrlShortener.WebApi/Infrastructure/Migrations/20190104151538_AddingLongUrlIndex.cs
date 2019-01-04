using Microsoft.EntityFrameworkCore.Migrations;

namespace UrlShortener.WebApi.Infrastructure.Migrations
{
    public partial class AddingLongUrlIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Url_LongUrl",
                schema: "core",
                table: "Url",
                column: "LongUrl",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Url_LongUrl",
                schema: "core",
                table: "Url");
        }
    }
}
