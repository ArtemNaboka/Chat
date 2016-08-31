using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FreelanceHunter.Data.Migrations
{
    public partial class InformationMigr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Informtation",
                table: "Profiles");

            migrationBuilder.AddColumn<string>(
                name: "Information",
                table: "Profiles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Information",
                table: "Profiles");

            migrationBuilder.AddColumn<string>(
                name: "Informtation",
                table: "Profiles",
                nullable: true);
        }
    }
}
