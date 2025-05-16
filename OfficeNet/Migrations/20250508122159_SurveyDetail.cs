using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeNet.Migrations
{
    /// <inheritdoc />
    public partial class SurveyDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SurveyDetail",
                columns: table => new
                {
                    SurveyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SurveyStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SurveyEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SurveyInstruction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SurveyConfirmation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SurveyView = table.Column<int>(type: "int", nullable: false),
                    AuthView = table.Column<int>(type: "int", nullable: false),
                    PlantId = table.Column<int>(type: "int", nullable: false),
                    IsExcel = table.Column<bool>(type: "bit", nullable: false),
                    SurveyStatus = table.Column<bool>(type: "bit", nullable: false),
                    Archieve = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyDetail", x => x.SurveyId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SurveyDetail");
        }
    }
}
