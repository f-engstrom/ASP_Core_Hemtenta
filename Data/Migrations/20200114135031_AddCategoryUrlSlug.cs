using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP_Core_Hemtenta.Data.Migrations
{
    public partial class AddCategoryUrlSlug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlSlug",
                table: "Categories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlSlug",
                table: "Categories");
        }
    }
}
