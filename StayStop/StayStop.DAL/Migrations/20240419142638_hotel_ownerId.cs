using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StayStop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class hotel_ownerId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelUser_Hotels_ManagedHotelsHotelId",
                table: "HotelUser");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelUser_Users_ManagersUserId",
                table: "HotelUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelUser",
                table: "HotelUser");

            migrationBuilder.RenameTable(
                name: "HotelUser",
                newName: "HotelManagers");

            migrationBuilder.RenameIndex(
                name: "IX_HotelUser_ManagersUserId",
                table: "HotelManagers",
                newName: "IX_HotelManagers_ManagersUserId");

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Hotels",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelManagers",
                table: "HotelManagers",
                columns: new[] { "ManagedHotelsHotelId", "ManagersUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_OwnerId",
                table: "Hotels",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelManagers_Hotels_ManagedHotelsHotelId",
                table: "HotelManagers",
                column: "ManagedHotelsHotelId",
                principalTable: "Hotels",
                principalColumn: "HotelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelManagers_Users_ManagersUserId",
                table: "HotelManagers",
                column: "ManagersUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Users_OwnerId",
                table: "Hotels",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelManagers_Hotels_ManagedHotelsHotelId",
                table: "HotelManagers");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelManagers_Users_ManagersUserId",
                table: "HotelManagers");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Users_OwnerId",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_OwnerId",
                table: "Hotels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelManagers",
                table: "HotelManagers");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Hotels");

            migrationBuilder.RenameTable(
                name: "HotelManagers",
                newName: "HotelUser");

            migrationBuilder.RenameIndex(
                name: "IX_HotelManagers_ManagersUserId",
                table: "HotelUser",
                newName: "IX_HotelUser_ManagersUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelUser",
                table: "HotelUser",
                columns: new[] { "ManagedHotelsHotelId", "ManagersUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_HotelUser_Hotels_ManagedHotelsHotelId",
                table: "HotelUser",
                column: "ManagedHotelsHotelId",
                principalTable: "Hotels",
                principalColumn: "HotelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelUser_Users_ManagersUserId",
                table: "HotelUser",
                column: "ManagersUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
