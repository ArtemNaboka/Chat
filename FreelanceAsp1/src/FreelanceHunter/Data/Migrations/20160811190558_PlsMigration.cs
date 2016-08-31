using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FreelanceHunter.Data.Migrations
{
    public partial class PlsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfCreate",
                table: "Adverts");

            migrationBuilder.AddColumn<string>(
                name: "Dislikers",
                table: "Adverts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Likers",
                table: "Adverts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dislikers",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "Likers",
                table: "Adverts");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfCreate",
                table: "Adverts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
