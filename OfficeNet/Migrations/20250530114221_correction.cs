using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeNet.Migrations
{
    /// <inheritdoc />
    public partial class correction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyOptions_SurveyQuestions_QuestionId",
                table: "SurveyOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestions_SurveyDetail_SurveyId",
                table: "SurveyQuestions");

            migrationBuilder.AlterColumn<int>(
                name: "SurveyId",
                table: "SurveyQuestions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "SurveyOptions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyOptions_SurveyQuestions_QuestionId",
                table: "SurveyOptions",
                column: "QuestionId",
                principalTable: "SurveyQuestions",
                principalColumn: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestions_SurveyDetail_SurveyId",
                table: "SurveyQuestions",
                column: "SurveyId",
                principalTable: "SurveyDetail",
                principalColumn: "SurveyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyOptions_SurveyQuestions_QuestionId",
                table: "SurveyOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestions_SurveyDetail_SurveyId",
                table: "SurveyQuestions");

            migrationBuilder.AlterColumn<int>(
                name: "SurveyId",
                table: "SurveyQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "SurveyOptions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyOptions_SurveyQuestions_QuestionId",
                table: "SurveyOptions",
                column: "QuestionId",
                principalTable: "SurveyQuestions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestions_SurveyDetail_SurveyId",
                table: "SurveyQuestions",
                column: "SurveyId",
                principalTable: "SurveyDetail",
                principalColumn: "SurveyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
