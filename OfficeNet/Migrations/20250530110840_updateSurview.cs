using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeNet.Migrations
{
    /// <inheritdoc />
    public partial class updateSurview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SurveyView",
                table: "SurveyDetail",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "SurveyView",
                table: "SurveyDetail",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
