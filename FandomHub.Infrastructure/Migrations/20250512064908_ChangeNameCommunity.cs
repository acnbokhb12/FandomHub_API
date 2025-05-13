using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FandomHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNameCommunity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Community",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Community",
                newName: "Title");
        }
    }
}
