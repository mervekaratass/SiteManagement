using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiteManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Apartments_ApartmentNumber_BlockNumber",
                table: "Apartments");

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_Id",
                table: "Apartments",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Apartments_Id",
                table: "Apartments");

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_ApartmentNumber_BlockNumber",
                table: "Apartments",
                columns: new[] { "ApartmentNumber", "BlockNumber" },
                unique: true);
        }
    }
}
