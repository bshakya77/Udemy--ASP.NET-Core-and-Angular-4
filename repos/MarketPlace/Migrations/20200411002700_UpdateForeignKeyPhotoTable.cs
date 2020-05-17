using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketPlace.Migrations
{
    public partial class UpdateForeignKeyPhotoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_VehicleModels_VehicleModelId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_VehicleModelId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "VehicleModelId",
                table: "Photos");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_VehicleId",
                table: "Photos",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_VehicleModels_VehicleId",
                table: "Photos",
                column: "VehicleId",
                principalTable: "VehicleModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_VehicleModels_VehicleId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_VehicleId",
                table: "Photos");

            migrationBuilder.AddColumn<int>(
                name: "VehicleModelId",
                table: "Photos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_VehicleModelId",
                table: "Photos",
                column: "VehicleModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_VehicleModels_VehicleModelId",
                table: "Photos",
                column: "VehicleModelId",
                principalTable: "VehicleModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
