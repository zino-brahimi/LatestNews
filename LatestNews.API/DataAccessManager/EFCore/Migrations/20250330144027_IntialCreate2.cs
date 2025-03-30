using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LatestNews.API.DataAccessManager.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class IntialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlToImage",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlToImage",
                table: "Articles");
        }
    }
}
