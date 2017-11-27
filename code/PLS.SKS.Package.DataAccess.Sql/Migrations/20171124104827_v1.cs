using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PLS.SKS.Package.DataAccess.Sql.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.AlterColumn<int>(
				name: "Id",
				table: "Warehouses",
				nullable: false,
				oldClrType: typeof(int))
			.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

			migrationBuilder.AlterColumn<int>(
				name: "Id",
				table: "Trucks",
				nullable: false,
				oldClrType: typeof(int))
			.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

			migrationBuilder.AlterColumn<int>(
				name: "Id",
				table: "TrackingInformations",
				nullable: false,
				oldClrType: typeof(int))
			.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

			migrationBuilder.AlterColumn<int>(
				name: "Id",
				table: "Recipients",
				nullable: false,
				oldClrType: typeof(int))
			.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

			migrationBuilder.AlterColumn<int>(
				name: "Id",
				table: "Parcels",
				nullable: false,
				oldClrType: typeof(int))
			.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

			migrationBuilder.AlterColumn<int>(
				name: "Id",
				table: "HopArrivals",
				nullable: false,
				oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
