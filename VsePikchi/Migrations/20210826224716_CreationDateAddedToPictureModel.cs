using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VsePikchi.Migrations
{
    public partial class CreationDateAddedToPictureModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Pictures",
                type: "Date",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Pictures");
        }
    }
}
