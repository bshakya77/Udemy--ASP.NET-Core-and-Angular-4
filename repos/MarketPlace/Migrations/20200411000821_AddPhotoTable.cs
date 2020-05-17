using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketPlace.Migrations
{
    public partial class AddPhotoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Features",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Name = table.Column<string>(maxLength: 255, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Features", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Manufacturers",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Name = table.Column<string>(maxLength: 255, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Manufacturers", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(maxLength: 255, nullable: false),
                    VehicleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                });

            //migrationBuilder.CreateTable(
            //    name: "Vehicles",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Name = table.Column<string>(maxLength: 255, nullable: false),
            //        ManufacturerId = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Vehicles", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Vehicles_Manufacturers_ManufacturerId",
            //            column: x => x.ManufacturerId,
            //            principalTable: "Manufacturers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "VehicleModels",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        VehicleId = table.Column<int>(nullable: false),
            //        IsRegistered = table.Column<bool>(nullable: false),
            //        ContactName = table.Column<string>(maxLength: 255, nullable: false),
            //        ContactEmail = table.Column<string>(maxLength: 255, nullable: true),
            //        ContactPhone = table.Column<string>(maxLength: 255, nullable: false),
            //        LastUpdate = table.Column<DateTime>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_VehicleModels", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_VehicleModels_Vehicles_VehicleId",
            //            column: x => x.VehicleId,
            //            principalTable: "Vehicles",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "VehicleFeatures",
            //    columns: table => new
            //    {
            //        VehicleModelId = table.Column<int>(nullable: false),
            //        FeatureId = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_VehicleFeatures", x => new { x.VehicleModelId, x.FeatureId });
            //        table.ForeignKey(
            //            name: "FK_VehicleFeatures_Features_FeatureId",
            //            column: x => x.FeatureId,
            //            principalTable: "Features",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_VehicleFeatures_VehicleModels_VehicleModelId",
            //            column: x => x.VehicleModelId,
            //            principalTable: "VehicleModels",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_VehicleFeatures_FeatureId",
            //    table: "VehicleFeatures",
            //    column: "FeatureId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_VehicleModels_VehicleId",
            //    table: "VehicleModels",
            //    column: "VehicleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Vehicles_ManufacturerId",
            //    table: "Vehicles",
            //    column: "ManufacturerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos");

            //migrationBuilder.DropTable(
            //    name: "VehicleFeatures");

            //migrationBuilder.DropTable(
            //    name: "Features");

            //migrationBuilder.DropTable(
            //    name: "VehicleModels");

            //migrationBuilder.DropTable(
            //    name: "Vehicles");

            //migrationBuilder.DropTable(
            //    name: "Manufacturers");
        }
    }
}
