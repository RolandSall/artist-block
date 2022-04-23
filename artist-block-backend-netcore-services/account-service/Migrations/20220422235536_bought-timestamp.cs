using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace account_service.Migrations
{
    public partial class boughttimestamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "bought_timestamp",
                table: "painting",
                type: "timestamp with time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bought_timestamp",
                table: "painting");
        }
    }
}
