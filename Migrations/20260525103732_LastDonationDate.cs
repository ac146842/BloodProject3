using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodProject3.Migrations
{
    /// <inheritdoc />
    public partial class LastDonationDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DonatedBlood_DonorID",
                table: "DonatedBlood",
                column: "DonorID");

            migrationBuilder.AddForeignKey(
                name: "FK_DonatedBlood_Donor_DonorID",
                table: "DonatedBlood",
                column: "DonorID",
                principalTable: "Donor",
                principalColumn: "DonorID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonatedBlood_Donor_DonorID",
                table: "DonatedBlood");

            migrationBuilder.DropIndex(
                name: "IX_DonatedBlood_DonorID",
                table: "DonatedBlood");
        }
    }
}
