using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class ApprovelStatusColumnAddition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCustomsApproved",
                table: "TruckGoods");

            migrationBuilder.AddColumn<int>(
                name: "ApprovalStatus",
                table: "TruckGoods",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalStatus",
                table: "TruckGoods");

            migrationBuilder.AddColumn<bool>(
                name: "IsCustomsApproved",
                table: "TruckGoods",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
