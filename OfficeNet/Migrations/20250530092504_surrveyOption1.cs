using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeNet.Migrations
{
    /// <inheritdoc />
    public partial class surrveyOption1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SurveyOptions",
                columns: table => new
                {
                    OptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    OptionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionOrder = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    Archive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyOptions", x => x.OptionId);
                    table.ForeignKey(
                        name: "FK_SurveyOptions_SurveyQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "SurveyQuestions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SurveyOptions_QuestionId",
                table: "SurveyOptions",
                column: "QuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SurveyOptions");
        }
    }
}
