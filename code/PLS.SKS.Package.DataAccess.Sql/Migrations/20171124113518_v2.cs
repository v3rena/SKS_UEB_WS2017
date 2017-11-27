using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PLS.SKS.Package.DataAccess.Sql.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HopArrivals_TrackingInformations_TrackingInformationId",
                table: "HopArrivals");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcels_Recipients_RecipientId",
                table: "Parcels");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcels_TrackingInformations_TrackingInformationId",
                table: "Parcels");

            migrationBuilder.DropForeignKey(
                name: "FK_Trucks_Warehouses_WarehouseId",
                table: "Trucks");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Warehouses_WarehouseId",
                table: "Warehouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Warehouses",
                table: "Warehouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trucks",
                table: "Trucks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrackingInformations",
                table: "TrackingInformations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipients",
                table: "Recipients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parcels",
                table: "Parcels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HopArrivals",
                table: "HopArrivals");

            migrationBuilder.RenameTable(
                name: "Warehouses",
                newName: "Warehouse");

            migrationBuilder.RenameTable(
                name: "Trucks",
                newName: "Truck");

            migrationBuilder.RenameTable(
                name: "TrackingInformations",
                newName: "TrackingInformation");

            migrationBuilder.RenameTable(
                name: "Recipients",
                newName: "Recipient");

            migrationBuilder.RenameTable(
                name: "Parcels",
                newName: "Parcel");

            migrationBuilder.RenameTable(
                name: "HopArrivals",
                newName: "HopArrival");

            migrationBuilder.RenameIndex(
                name: "IX_Warehouses_WarehouseId",
                table: "Warehouse",
                newName: "IX_Warehouse_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Trucks_WarehouseId",
                table: "Truck",
                newName: "IX_Truck_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Parcels_TrackingInformationId",
                table: "Parcel",
                newName: "IX_Parcel_TrackingInformationId");

            migrationBuilder.RenameIndex(
                name: "IX_Parcels_RecipientId",
                table: "Parcel",
                newName: "IX_Parcel_RecipientId");

            migrationBuilder.RenameIndex(
                name: "IX_HopArrivals_TrackingInformationId",
                table: "HopArrival",
                newName: "IX_HopArrival_TrackingInformationId");

			migrationBuilder.AlterColumn<int>(
				name: "Id",
				table: "Warehouse",
				nullable: false,
				oldClrType: typeof(int))
			.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

			migrationBuilder.AlterColumn<int>(
				name: "Id",
				table: "Truck",
				nullable: false,
				oldClrType: typeof(int))
			.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

			migrationBuilder.AlterColumn<int>(
				name: "Id",
				table: "TrackingInformation",
				nullable: false,
				oldClrType: typeof(int))
			.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

			migrationBuilder.AlterColumn<int>(
				name: "Id",
				table: "Recipient",
				nullable: false,
				oldClrType: typeof(int))
			.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

			migrationBuilder.AlterColumn<int>(
				name: "Id",
				table: "Parcel",
				nullable: false,
				oldClrType: typeof(int))
			.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

			migrationBuilder.AlterColumn<int>(
				name: "Id",
				table: "HopArrival",
				nullable: false,
				oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Warehouse",
                table: "Warehouse",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Truck",
                table: "Truck",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrackingInformation",
                table: "TrackingInformation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipient",
                table: "Recipient",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parcel",
                table: "Parcel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HopArrival",
                table: "HopArrival",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HopArrival_TrackingInformation_TrackingInformationId",
                table: "HopArrival",
                column: "TrackingInformationId",
                principalTable: "TrackingInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parcel_Recipient_RecipientId",
                table: "Parcel",
                column: "RecipientId",
                principalTable: "Recipient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parcel_TrackingInformation_TrackingInformationId",
                table: "Parcel",
                column: "TrackingInformationId",
                principalTable: "TrackingInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Truck_Warehouse_WarehouseId",
                table: "Truck",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouse_Warehouse_WarehouseId",
                table: "Warehouse",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HopArrival_TrackingInformation_TrackingInformationId",
                table: "HopArrival");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcel_Recipient_RecipientId",
                table: "Parcel");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcel_TrackingInformation_TrackingInformationId",
                table: "Parcel");

            migrationBuilder.DropForeignKey(
                name: "FK_Truck_Warehouse_WarehouseId",
                table: "Truck");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouse_Warehouse_WarehouseId",
                table: "Warehouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Warehouse",
                table: "Warehouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Truck",
                table: "Truck");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrackingInformation",
                table: "TrackingInformation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipient",
                table: "Recipient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parcel",
                table: "Parcel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HopArrival",
                table: "HopArrival");

            migrationBuilder.RenameTable(
                name: "Warehouse",
                newName: "Warehouses");

            migrationBuilder.RenameTable(
                name: "Truck",
                newName: "Trucks");

            migrationBuilder.RenameTable(
                name: "TrackingInformation",
                newName: "TrackingInformations");

            migrationBuilder.RenameTable(
                name: "Recipient",
                newName: "Recipients");

            migrationBuilder.RenameTable(
                name: "Parcel",
                newName: "Parcels");

            migrationBuilder.RenameTable(
                name: "HopArrival",
                newName: "HopArrivals");

            migrationBuilder.RenameIndex(
                name: "IX_Warehouse_WarehouseId",
                table: "Warehouses",
                newName: "IX_Warehouses_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Truck_WarehouseId",
                table: "Trucks",
                newName: "IX_Trucks_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Parcel_TrackingInformationId",
                table: "Parcels",
                newName: "IX_Parcels_TrackingInformationId");

            migrationBuilder.RenameIndex(
                name: "IX_Parcel_RecipientId",
                table: "Parcels",
                newName: "IX_Parcels_RecipientId");

            migrationBuilder.RenameIndex(
                name: "IX_HopArrival_TrackingInformationId",
                table: "HopArrivals",
                newName: "IX_HopArrivals_TrackingInformationId");

			migrationBuilder.AlterColumn<int>(
				name: "Id",
				table: "Warehouses",
				nullable: false,
				oldClrType: typeof(int))
			.OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

			migrationBuilder.AlterColumn<int>(
				name: "Id",
				table: "Trucks",
				nullable: false,
				oldClrType: typeof(int))
			.OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

			migrationBuilder.AlterColumn<int>(
				name: "Id",
				table: "TrackingInformations",
				nullable: false,
				oldClrType: typeof(int))
			.OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

			migrationBuilder.AlterColumn<int>(
				name: "Id",
				table: "Recipients",
				nullable: false,
				oldClrType: typeof(int))
			.OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

			migrationBuilder.AlterColumn<int>(
				name: "Id",
				table: "Parcels",
				nullable: false,
				oldClrType: typeof(int))
			.OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

			migrationBuilder.AlterColumn<int>(
				name: "Id",
				table: "HopArrivals",
				nullable: false,
				oldClrType: typeof(int))
			.OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Warehouses",
                table: "Warehouses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trucks",
                table: "Trucks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrackingInformations",
                table: "TrackingInformations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipients",
                table: "Recipients",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parcels",
                table: "Parcels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HopArrivals",
                table: "HopArrivals",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HopArrivals_TrackingInformations_TrackingInformationId",
                table: "HopArrivals",
                column: "TrackingInformationId",
                principalTable: "TrackingInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parcels_Recipients_RecipientId",
                table: "Parcels",
                column: "RecipientId",
                principalTable: "Recipients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parcels_TrackingInformations_TrackingInformationId",
                table: "Parcels",
                column: "TrackingInformationId",
                principalTable: "TrackingInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trucks_Warehouses_WarehouseId",
                table: "Trucks",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_Warehouses_WarehouseId",
                table: "Warehouses",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
