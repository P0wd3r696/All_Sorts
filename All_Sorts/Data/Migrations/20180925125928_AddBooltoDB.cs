using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace All_Sorts.Data.Migrations
{
    public partial class AddBooltoDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "toFacebook",
                table: "Posts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "toLinkedin",
                table: "Posts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "toTwitter",
                table: "Posts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "toFacebook",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "toLinkedin",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "toTwitter",
                table: "Posts");
        }
    }
}
