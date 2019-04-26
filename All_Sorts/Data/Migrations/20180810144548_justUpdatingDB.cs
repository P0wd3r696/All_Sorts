using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace All_Sorts.Data.Migrations
{
    public partial class justUpdatingDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserPost",
                table: "Posts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserPost",
                table: "Posts",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
