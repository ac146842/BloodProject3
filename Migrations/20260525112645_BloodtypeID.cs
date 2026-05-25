using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodProject3.Migrations
{
    /// <inheritdoc />
    public partial class BloodtypeID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Inventory_BloodTypeID",
                table: "Inventory",
                column: "BloodTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_BloodType_BloodTypeID",
                table: "Inventory",
                column: "BloodTypeID",
                principalTable: "BloodType",
                principalColumn: "BloodTypeID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_BloodType_BloodTypeID",
                table: "Inventory");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_BloodTypeID",
                table: "Inventory");
        }
    }
}
