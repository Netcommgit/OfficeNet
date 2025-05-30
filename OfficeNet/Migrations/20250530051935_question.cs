using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeNet.Migrations
{
    /// <inheritdoc />
    public partial class question : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SurveyQuestions",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyId = table.Column<int>(type: "int", nullable: false),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionRequierd = table.Column<bool>(type: "bit", nullable: false),
                    QuestionErrorMsg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionViewReport = table.Column<int>(type: "int", nullable: true),
                    QuestionType = table.Column<int>(type: "int", nullable: true),
                    QuestionOrder = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    Archieve = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyQuestions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_SurveyQuestions_SurveyDetail_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "SurveyDetail",
                        principalColumn: "SurveyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestions_SurveyId",
                table: "SurveyQuestions",
                column: "SurveyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SurveyQuestions");
        }
    }
}
