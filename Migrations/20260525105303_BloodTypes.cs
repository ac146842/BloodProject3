using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodProject3.Migrations
{
    /// <inheritdoc />
    public partial class BloodTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DonatedBlood_BloodTypeID",
                table: "DonatedBlood",
                column: "BloodTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_DonatedBlood_BloodType_BloodTypeID",
                table: "DonatedBlood",
                column: "BloodTypeID",
                principalTable: "BloodType",
                principalColumn: "BloodTypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonatedBlood_BloodType_BloodTypeID",
                table: "DonatedBlood");

            migrationBuilder.DropIndex(
                name: "IX_DonatedBlood_BloodTypeID",
                table: "DonatedBlood");
        }
    }
}
