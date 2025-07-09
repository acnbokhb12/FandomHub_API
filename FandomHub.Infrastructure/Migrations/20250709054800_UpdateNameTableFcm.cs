using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FandomHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNameTableFcm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FcmTokens",
                table: "FcmTokens");

            migrationBuilder.RenameTable(
                name: "FcmTokens",
                newName: "FcmToken");

            migrationBuilder.RenameIndex(
                name: "IX_FcmTokens_UserId",
                table: "FcmToken",
                newName: "IX_FcmToken_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FcmTokens_Token",
                table: "FcmToken",
                newName: "IX_FcmToken_Token");

            migrationBuilder.RenameIndex(
                name: "IX_FcmTokens_DeviceId",
                table: "FcmToken",
                newName: "IX_FcmToken_DeviceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FcmToken",
                table: "FcmToken",
                column: "FcmTokenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FcmToken",
                table: "FcmToken");

            migrationBuilder.RenameTable(
                name: "FcmToken",
                newName: "FcmTokens");

            migrationBuilder.RenameIndex(
                name: "IX_FcmToken_UserId",
                table: "FcmTokens",
                newName: "IX_FcmTokens_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FcmToken_Token",
                table: "FcmTokens",
                newName: "IX_FcmTokens_Token");

            migrationBuilder.RenameIndex(
                name: "IX_FcmToken_DeviceId",
                table: "FcmTokens",
                newName: "IX_FcmTokens_DeviceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FcmTokens",
                table: "FcmTokens",
                column: "FcmTokenId");
        }
    }
}
