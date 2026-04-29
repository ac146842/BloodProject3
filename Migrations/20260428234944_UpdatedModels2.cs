using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodProject3.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedModels2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastDonationDate",
                table: "Donor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastDonationDate",
                table: "Donor",
                type: "datetime2",
                nullable: true);
        }
    }
}
