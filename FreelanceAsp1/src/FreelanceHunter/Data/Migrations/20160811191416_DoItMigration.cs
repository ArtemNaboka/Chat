using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FreelanceHunter.Data.Migrations
{
    public partial class DoItMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dislikers",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "Likers",
                table: "Adverts");

            migrationBuilder.AddColumn<string>(
                name: "Dislikers",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Likers",
                table: "Profiles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dislikers",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "Likers",
                table: "Profiles");

            migrationBuilder.AddColumn<string>(
                name: "Dislikers",
                table: "Adverts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Likers",
                table: "Adverts",
                nullable: true);
        }
    }
}
