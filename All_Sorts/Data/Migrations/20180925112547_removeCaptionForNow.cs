using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace All_Sorts.Data.Migrations
{
    public partial class removeCaptionForNow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserCaption",
                table: "Posts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserCaption",
                table: "Posts",
                nullable: true);
        }
    }
}
