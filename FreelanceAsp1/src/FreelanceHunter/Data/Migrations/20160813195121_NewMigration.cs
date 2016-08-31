using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FreelanceHunter.Data.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileComment_Profiles_ProfileId",
                table: "ProfileComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfileComment",
                table: "ProfileComment");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfileComments",
                table: "ProfileComment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileComments_Profiles_ProfileId",
                table: "ProfileComment",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameIndex(
                name: "IX_ProfileComment_ProfileId",
                table: "ProfileComment",
                newName: "IX_ProfileComments_ProfileId");

            migrationBuilder.RenameTable(
                name: "ProfileComment",
                newName: "ProfileComments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileComments_Profiles_ProfileId",
                table: "ProfileComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfileComments",
                table: "ProfileComments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfileComment",
                table: "ProfileComments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileComment_Profiles_ProfileId",
                table: "ProfileComments",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameIndex(
                name: "IX_ProfileComments_ProfileId",
                table: "ProfileComments",
                newName: "IX_ProfileComment_ProfileId");

            migrationBuilder.RenameTable(
                name: "ProfileComments",
                newName: "ProfileComment");
        }
    }
}
