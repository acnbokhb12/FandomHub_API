using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FandomHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ViewCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Page",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Community",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Page");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Community");
        }
    }
}
