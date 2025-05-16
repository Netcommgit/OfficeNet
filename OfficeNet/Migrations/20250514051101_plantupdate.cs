using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeNet.Migrations
{
    /// <inheritdoc />
    public partial class plantupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlantDescription",
                table: "Plants",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlantDescription",
                table: "Plants");
        }
    }
}
