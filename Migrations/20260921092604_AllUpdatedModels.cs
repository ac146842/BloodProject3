using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodProject3.Migrations
{
    /// <inheritdoc />
    public partial class AllUpdatedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DonatedBloodDonationID",
                table: "Inventory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_DonatedBloodDonationID",
                table: "Inventory",
                column: "DonatedBloodDonationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_DonatedBlood_DonatedBloodDonationID",
                table: "Inventory",
                column: "DonatedBloodDonationID",
                principalTable: "DonatedBlood",
                principalColumn: "DonationID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_DonatedBlood_DonatedBloodDonationID",
                table: "Inventory");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_DonatedBloodDonationID",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "DonatedBloodDonationID",
                table: "Inventory");
        }
    }
}
