using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanchesDoTioAPI.Migrations
{
    /// <inheritdoc />
    public partial class entitiesImprovement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PaymentAmount",
                table: "Order",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Order",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentAmount",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Order");
        }
    }
}
