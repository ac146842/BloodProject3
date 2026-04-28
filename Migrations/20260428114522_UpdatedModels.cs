using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodProject3.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BloodTypeName",
                table: "BloodType");

            migrationBuilder.DropColumn(
                name: "AppointmentTimes",
                table: "Appointment");

            migrationBuilder.AlterColumn<string>(
                name: "FormQuestions",
                table: "Questions",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentVolumeML",
                table: "Inventory",
                type: "decimal(3,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "VolumeML",
                table: "DonatedBlood",
                type: "decimal(6,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "SelectedBloodType",
                table: "BloodType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FormID",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedBloodType",
                table: "BloodType");

            migrationBuilder.DropColumn(
                name: "FormID",
                table: "Answers");

            migrationBuilder.AlterColumn<string>(
                name: "FormQuestions",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentVolumeML",
                table: "Inventory",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "VolumeML",
                table: "DonatedBlood",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)");

            migrationBuilder.AddColumn<string>(
                name: "BloodTypeName",
                table: "BloodType",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentTimes",
                table: "Appointment",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
