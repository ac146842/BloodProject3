using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodProject3.Migrations
{
    /// <inheritdoc />
    public partial class donorid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Answers_DonorID",
                table: "Answers",
                column: "DonorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Donor_DonorID",
                table: "Answers",
                column: "DonorID",
                principalTable: "Donor",
                principalColumn: "DonorID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Donor_DonorID",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_DonorID",
                table: "Answers");
        }
    }
}
