using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace All_Sorts.Data.Migrations
{
    public partial class updateApplicationUserData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPageIds");

            migrationBuilder.AddColumn<string>(
                name: "FacebookPageId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedinPageId",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacebookPageId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LinkedinPageId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "UserPageIds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FacebookPageId = table.Column<string>(nullable: false),
                    LinkedinPageId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPageIds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPageIds_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPageIds_UserId",
                table: "UserPageIds",
                column: "UserId");
        }
    }
}
