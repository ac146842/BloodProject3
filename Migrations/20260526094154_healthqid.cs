using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodProject3.Migrations
{
    /// <inheritdoc />
    public partial class healthqid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Answers_HealthQID",
                table: "Answers",
                column: "HealthQID");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_HealthQID",
                table: "Answers",
                column: "HealthQID",
                principalTable: "Questions",
                principalColumn: "HealthQID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_HealthQID",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_HealthQID",
                table: "Answers");
        }
    }
}
