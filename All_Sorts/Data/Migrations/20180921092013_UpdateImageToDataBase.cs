using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace All_Sorts.Data.Migrations
{
    public partial class UpdateImageToDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserPost",
                table: "Posts",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "UserCaption",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserImage",
                table: "Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserCaption",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "UserImage",
                table: "Posts");

            migrationBuilder.AlterColumn<string>(
                name: "UserPost",
                table: "Posts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
