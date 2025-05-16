using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FandomHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAtribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Page",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubTitle",
                table: "Page",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Community",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubName",
                table: "Community",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Page");

            migrationBuilder.DropColumn(
                name: "SubTitle",
                table: "Page");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Community");

            migrationBuilder.DropColumn(
                name: "SubName",
                table: "Community");
        }
    }
}
