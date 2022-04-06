using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace account_service.Migrations
{
    public partial class ganimageentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "gan_image",
                columns: table => new
                {
                    PK_gan_image_id = table.Column<Guid>(type: "uuid", nullable: false),
                    gan_image_description = table.Column<string>(type: "text", nullable: false),
                    FK_painting_registered_user_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gan_image", x => x.PK_gan_image_id);
                    table.ForeignKey(
                        name: "FK_gan_image_client_FK_painting_registered_user_id",
                        column: x => x.FK_painting_registered_user_id,
                        principalTable: "client",
                        principalColumn: "PK_registered_user_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_gan_image_FK_painting_registered_user_id",
                table: "gan_image",
                column: "FK_painting_registered_user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "gan_image");
        }
    }
}
