using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodProject3.Migrations
{
    /// <inheritdoc />
    public partial class AppointmentID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DonatedBlood_AppointmentID",
                table: "DonatedBlood",
                column: "AppointmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_DonatedBlood_Appointment_AppointmentID",
                table: "DonatedBlood",
                column: "AppointmentID",
                principalTable: "Appointment",
                principalColumn: "AppointmentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonatedBlood_Appointment_AppointmentID",
                table: "DonatedBlood");

            migrationBuilder.DropIndex(
                name: "IX_DonatedBlood_AppointmentID",
                table: "DonatedBlood");
        }
    }
}
