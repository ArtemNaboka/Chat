using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FreelanceHunter.Data.Migrations
{
    public partial class SupaMigr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Completed",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRelevant",
                table: "Adverts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completed",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "IsRelevant",
                table: "Adverts");
        }
    }
}
