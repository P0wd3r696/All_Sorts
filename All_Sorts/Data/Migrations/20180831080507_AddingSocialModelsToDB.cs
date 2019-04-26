using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace All_Sorts.Data.Migrations
{
    public partial class AddingSocialModelsToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FacebookTokens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FacebookAccessToken = table.Column<string>(nullable: true),
                    FacebookEmail = table.Column<string>(nullable: true),
                    FacebookPageId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacebookTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacebookTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LinkedinTokens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LinkedinAccessToken = table.Column<string>(nullable: true),
                    LinkedinEmail = table.Column<string>(nullable: true),
                    LinkedinPageId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkedinTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkedinTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacebookTokens_UserId",
                table: "FacebookTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkedinTokens_UserId",
                table: "LinkedinTokens",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacebookTokens");

            migrationBuilder.DropTable(
                name: "LinkedinTokens");
        }
    }
}
