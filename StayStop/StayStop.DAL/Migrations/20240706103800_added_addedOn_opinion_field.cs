using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StayStop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class added_addedOn_opinion_field : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
         
            migrationBuilder.AddColumn<DateTime>(
                name: "AddedOn",
                table: "Opinions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedOn",
                table: "Opinions");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfAdults",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfChildren",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
