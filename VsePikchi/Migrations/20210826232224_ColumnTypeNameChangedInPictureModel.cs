using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VsePikchi.Migrations
{
    public partial class ColumnTypeNameChangedInPictureModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Pictures",
                type: "Datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Pictures",
                type: "Date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "Datetime",
                oldNullable: true);
        }
    }
}
