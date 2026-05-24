using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodProject3.Migrations
{
    /// <inheritdoc />
    public partial class DB2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswersText",
                table: "Answers");

            migrationBuilder.AddColumn<bool>(
                name: "AnswersBool",
                table: "Answers",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.DropColumn(
                name: "AnswersBool",
                table: "Answers");

            migrationBuilder.AddColumn<string>(
                name: "AnswersText",
                table: "Answers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
