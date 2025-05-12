using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FandomHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePageCommunity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Page",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeleteBy",
                table: "Page",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Page",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Community",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeleteBy",
                table: "Community",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Community",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Page_CreatedBy",
                table: "Page",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Page_DeleteBy",
                table: "Page",
                column: "DeleteBy");

            migrationBuilder.CreateIndex(
                name: "IX_Page_UpdatedBy",
                table: "Page",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Community_CreatedBy",
                table: "Community",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Community_DeleteBy",
                table: "Community",
                column: "DeleteBy");

            migrationBuilder.CreateIndex(
                name: "IX_Community_UpdatedBy",
                table: "Community",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Community_AspNetUsers_CreatedBy",
                table: "Community",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Community_AspNetUsers_DeleteBy",
                table: "Community",
                column: "DeleteBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Community_AspNetUsers_UpdatedBy",
                table: "Community",
                column: "UpdatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Page_AspNetUsers_CreatedBy",
                table: "Page",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Page_AspNetUsers_DeleteBy",
                table: "Page",
                column: "DeleteBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Page_AspNetUsers_UpdatedBy",
                table: "Page",
                column: "UpdatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Community_AspNetUsers_CreatedBy",
                table: "Community");

            migrationBuilder.DropForeignKey(
                name: "FK_Community_AspNetUsers_DeleteBy",
                table: "Community");

            migrationBuilder.DropForeignKey(
                name: "FK_Community_AspNetUsers_UpdatedBy",
                table: "Community");

            migrationBuilder.DropForeignKey(
                name: "FK_Page_AspNetUsers_CreatedBy",
                table: "Page");

            migrationBuilder.DropForeignKey(
                name: "FK_Page_AspNetUsers_DeleteBy",
                table: "Page");

            migrationBuilder.DropForeignKey(
                name: "FK_Page_AspNetUsers_UpdatedBy",
                table: "Page");

            migrationBuilder.DropIndex(
                name: "IX_Page_CreatedBy",
                table: "Page");

            migrationBuilder.DropIndex(
                name: "IX_Page_DeleteBy",
                table: "Page");

            migrationBuilder.DropIndex(
                name: "IX_Page_UpdatedBy",
                table: "Page");

            migrationBuilder.DropIndex(
                name: "IX_Community_CreatedBy",
                table: "Community");

            migrationBuilder.DropIndex(
                name: "IX_Community_DeleteBy",
                table: "Community");

            migrationBuilder.DropIndex(
                name: "IX_Community_UpdatedBy",
                table: "Community");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Page",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeleteBy",
                table: "Page",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Page",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Community",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeleteBy",
                table: "Community",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Community",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
