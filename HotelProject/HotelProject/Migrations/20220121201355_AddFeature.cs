using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelProject.Migrations
{
    public partial class AddFeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_hotels_HotelId",
                table: "Room");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomFeatures_Room_RoomId",
                table: "RoomFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomPhoto_Room_RoomId",
                table: "RoomPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomPhoto",
                table: "RoomPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Room",
                table: "Room");

            migrationBuilder.RenameTable(
                name: "RoomPhoto",
                newName: "RoomPhotos");

            migrationBuilder.RenameTable(
                name: "Room",
                newName: "Rooms");

            migrationBuilder.RenameIndex(
                name: "IX_RoomPhoto_RoomId",
                table: "RoomPhotos",
                newName: "IX_RoomPhotos_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Room_HotelId",
                table: "Rooms",
                newName: "IX_Rooms_HotelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomPhotos",
                table: "RoomPhotos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomFeatures_Rooms_RoomId",
                table: "RoomFeatures",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomPhotos_Rooms_RoomId",
                table: "RoomPhotos",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_hotels_HotelId",
                table: "Rooms",
                column: "HotelId",
                principalTable: "hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomFeatures_Rooms_RoomId",
                table: "RoomFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomPhotos_Rooms_RoomId",
                table: "RoomPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_hotels_HotelId",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomPhotos",
                table: "RoomPhotos");

            migrationBuilder.RenameTable(
                name: "Rooms",
                newName: "Room");

            migrationBuilder.RenameTable(
                name: "RoomPhotos",
                newName: "RoomPhoto");

            migrationBuilder.RenameIndex(
                name: "IX_Rooms_HotelId",
                table: "Room",
                newName: "IX_Room_HotelId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomPhotos_RoomId",
                table: "RoomPhoto",
                newName: "IX_RoomPhoto_RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Room",
                table: "Room",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomPhoto",
                table: "RoomPhoto",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_hotels_HotelId",
                table: "Room",
                column: "HotelId",
                principalTable: "hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomFeatures_Room_RoomId",
                table: "RoomFeatures",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomPhoto_Room_RoomId",
                table: "RoomPhoto",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
