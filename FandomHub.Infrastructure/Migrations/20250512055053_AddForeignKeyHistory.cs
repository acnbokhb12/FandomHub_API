using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FandomHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "EditHistory",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeleteBy",
                table: "EditHistory",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "EditHistory",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EditHistory_CreatedBy",
                table: "EditHistory",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_EditHistory_DeleteBy",
                table: "EditHistory",
                column: "DeleteBy");

            migrationBuilder.CreateIndex(
                name: "IX_EditHistory_UpdatedBy",
                table: "EditHistory",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_EditHistory_AspNetUsers_CreatedBy",
                table: "EditHistory",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EditHistory_AspNetUsers_DeleteBy",
                table: "EditHistory",
                column: "DeleteBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EditHistory_AspNetUsers_UpdatedBy",
                table: "EditHistory",
                column: "UpdatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EditHistory_AspNetUsers_CreatedBy",
                table: "EditHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_EditHistory_AspNetUsers_DeleteBy",
                table: "EditHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_EditHistory_AspNetUsers_UpdatedBy",
                table: "EditHistory");

            migrationBuilder.DropIndex(
                name: "IX_EditHistory_CreatedBy",
                table: "EditHistory");

            migrationBuilder.DropIndex(
                name: "IX_EditHistory_DeleteBy",
                table: "EditHistory");

            migrationBuilder.DropIndex(
                name: "IX_EditHistory_UpdatedBy",
                table: "EditHistory");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "EditHistory",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeleteBy",
                table: "EditHistory",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "EditHistory",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
