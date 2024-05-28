using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StayStop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UserRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "AddedById",
                table: "Opinions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Opinions_AddedById",
                table: "Opinions",
                column: "AddedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Opinions_Users_AddedById",
                table: "Opinions",
                column: "AddedById",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Opinions_Users_AddedById",
                table: "Opinions");

            migrationBuilder.DropIndex(
                name: "IX_Opinions_AddedById",
                table: "Opinions");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AddedById",
                table: "Opinions");
        }
    }
}
