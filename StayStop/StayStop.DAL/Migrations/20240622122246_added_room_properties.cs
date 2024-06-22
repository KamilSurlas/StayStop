using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StayStop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class added_room_properties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfAdults",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfChildren",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfAdults",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "NumberOfChildren",
                table: "Rooms");
        }
    }
}
