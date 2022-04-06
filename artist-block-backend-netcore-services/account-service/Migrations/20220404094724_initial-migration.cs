using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace account_service.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "client",
                columns: table => new
                {
                    PK_registered_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    nationality = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    image_url = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    birth_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client", x => x.PK_registered_user_id);
                });

            migrationBuilder.CreateTable(
                name: "speciality",
                columns: table => new
                {
                    PK_speciality_id = table.Column<Guid>(type: "uuid", nullable: false),
                    speciality_type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_speciality", x => x.PK_speciality_id);
                });

            migrationBuilder.CreateTable(
                name: "auth_user",
                columns: table => new
                {
                    PK_auth0_id = table.Column<string>(type: "text", nullable: false),
                    FK_auth0_registered_user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auth_user", x => x.PK_auth0_id);
                    table.ForeignKey(
                        name: "FK_auth_user_client_FK_auth0_registered_user_id",
                        column: x => x.FK_auth0_registered_user_id,
                        principalTable: "client",
                        principalColumn: "PK_registered_user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "painter",
                columns: table => new
                {
                    PK_painter_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    years_of_experience = table.Column<string>(type: "text", nullable: false),
                    bio = table.Column<string>(type: "text", nullable: false),
                    FK_painter_registered_user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_painter", x => x.PK_painter_id);
                    table.ForeignKey(
                        name: "FK_painter_client_FK_painter_registered_user_id",
                        column: x => x.FK_painter_registered_user_id,
                        principalTable: "client",
                        principalColumn: "PK_registered_user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "painter_speciality",
                columns: table => new
                {
                    PK_painter_speciality_id = table.Column<Guid>(type: "uuid", nullable: false),
                    FK_speciality_id = table.Column<Guid>(type: "uuid", nullable: false),
                    FK_painter_id = table.Column<Guid>(type: "uuid", nullable: false),
                    priority = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_painter_speciality", x => x.PK_painter_speciality_id);
                    table.ForeignKey(
                        name: "FK_painter_speciality_painter_FK_painter_id",
                        column: x => x.FK_painter_id,
                        principalTable: "painter",
                        principalColumn: "PK_painter_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_painter_speciality_speciality_FK_speciality_id",
                        column: x => x.FK_speciality_id,
                        principalTable: "speciality",
                        principalColumn: "PK_speciality_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "painting",
                columns: table => new
                {
                    PK_painting_id = table.Column<Guid>(type: "uuid", nullable: false),
                    painting_name = table.Column<string>(type: "text", nullable: false),
                    painting_description = table.Column<string>(type: "text", nullable: false),
                    painted_year = table.Column<string>(type: "text", nullable: false),
                    painting_price = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    painting_url = table.Column<string>(type: "text", nullable: true),
                    FK_painting_registered_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    FK_painting_painter_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_painting", x => x.PK_painting_id);
                    table.ForeignKey(
                        name: "FK_painting_client_FK_painting_registered_user_id",
                        column: x => x.FK_painting_registered_user_id,
                        principalTable: "client",
                        principalColumn: "PK_registered_user_id");
                    table.ForeignKey(
                        name: "FK_painting_painter_FK_painting_painter_id",
                        column: x => x.FK_painting_painter_id,
                        principalTable: "painter",
                        principalColumn: "PK_painter_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_auth_user_FK_auth0_registered_user_id",
                table: "auth_user",
                column: "FK_auth0_registered_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_painter_FK_painter_registered_user_id",
                table: "painter",
                column: "FK_painter_registered_user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_painter_speciality_FK_painter_id",
                table: "painter_speciality",
                column: "FK_painter_id");

            migrationBuilder.CreateIndex(
                name: "IX_painter_speciality_FK_speciality_id",
                table: "painter_speciality",
                column: "FK_speciality_id");

            migrationBuilder.CreateIndex(
                name: "IX_painting_FK_painting_painter_id",
                table: "painting",
                column: "FK_painting_painter_id");

            migrationBuilder.CreateIndex(
                name: "IX_painting_FK_painting_registered_user_id",
                table: "painting",
                column: "FK_painting_registered_user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "auth_user");

            migrationBuilder.DropTable(
                name: "painter_speciality");

            migrationBuilder.DropTable(
                name: "painting");

            migrationBuilder.DropTable(
                name: "speciality");

            migrationBuilder.DropTable(
                name: "painter");

            migrationBuilder.DropTable(
                name: "client");
        }
    }
}
