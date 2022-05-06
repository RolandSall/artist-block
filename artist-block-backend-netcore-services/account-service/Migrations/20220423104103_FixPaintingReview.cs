using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace account_service.Migrations
{
    public partial class FixPaintingReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FK_registered_user_id",
                table: "PaintingReview",
                newName: "FK_painting_review_registered_user_id");

            migrationBuilder.RenameColumn(
                name: "FK_painting_id",
                table: "PaintingReview",
                newName: "FK_painting_review_painting_id");

            migrationBuilder.CreateIndex(
                name: "IX_PaintingReview_FK_painting_review_painting_id",
                table: "PaintingReview",
                column: "FK_painting_review_painting_id");

            migrationBuilder.CreateIndex(
                name: "IX_PaintingReview_FK_painting_review_registered_user_id",
                table: "PaintingReview",
                column: "FK_painting_review_registered_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaintingReview_client_FK_painting_review_registered_user_id",
                table: "PaintingReview",
                column: "FK_painting_review_registered_user_id",
                principalTable: "client",
                principalColumn: "PK_registered_user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaintingReview_painting_FK_painting_review_painting_id",
                table: "PaintingReview",
                column: "FK_painting_review_painting_id",
                principalTable: "painting",
                principalColumn: "PK_painting_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaintingReview_client_FK_painting_review_registered_user_id",
                table: "PaintingReview");

            migrationBuilder.DropForeignKey(
                name: "FK_PaintingReview_painting_FK_painting_review_painting_id",
                table: "PaintingReview");

            migrationBuilder.DropIndex(
                name: "IX_PaintingReview_FK_painting_review_painting_id",
                table: "PaintingReview");

            migrationBuilder.DropIndex(
                name: "IX_PaintingReview_FK_painting_review_registered_user_id",
                table: "PaintingReview");

            migrationBuilder.RenameColumn(
                name: "FK_painting_review_registered_user_id",
                table: "PaintingReview",
                newName: "FK_registered_user_id");

            migrationBuilder.RenameColumn(
                name: "FK_painting_review_painting_id",
                table: "PaintingReview",
                newName: "FK_painting_id");
        }
    }
}
