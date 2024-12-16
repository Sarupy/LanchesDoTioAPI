using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanchesDoTioAPI.Migrations
{
    /// <inheritdoc />
    public partial class removeStaticPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPrice",
                table: "Meal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CurrentPrice",
                table: "Meal",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
