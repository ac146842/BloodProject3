using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodProject3.Migrations
{
    /// <inheritdoc />
    public partial class FinalDB10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MedicalForm_AppointmentID",
                table: "MedicalForm",
                column: "AppointmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalForm_Appointment_AppointmentID",
                table: "MedicalForm",
                column: "AppointmentID",
                principalTable: "Appointment",
                principalColumn: "AppointmentID",
                onDelete: ReferentialAction.NoAction); // Changed to NoAction to prevent circular path
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalForm_Appointment_AppointmentID",
                table: "MedicalForm");

            migrationBuilder.DropIndex(
                name: "IX_MedicalForm_AppointmentID",
                table: "MedicalForm");
        }
    }
}