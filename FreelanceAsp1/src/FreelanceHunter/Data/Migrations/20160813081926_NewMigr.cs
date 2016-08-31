using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FreelanceHunter.Data.Migrations
{
    public partial class NewMigr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DateOfCreate",
                table: "Offers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateOfCreate",
                table: "Adverts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfCreate",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "DateOfCreate",
                table: "Adverts");
        }
    }
}
