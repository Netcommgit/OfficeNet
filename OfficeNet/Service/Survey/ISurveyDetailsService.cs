using OfficeNet.Domain.Contracts;
using OfficeNet.Domain.Entities;
using OfficeNet.Migrations;

namespace OfficeNet.Service.Survey
{
    public interface ISurveyDetailsService
    {
        Task<SurveyDetails> SaveSurveyDetailsAsync(SurveyDetails surveyDetails, List<string> userList,SurveyQuestion surveyQuestion, List<string> questionType);
        Task<SurveyAuthenticateUser> CreateSurveyAuthenticateUserAsync(SurveyAuthenticateUser surveyAuthenticateUser);
        Task<List<SurveyList>> GetSurveyListAsync();
        Task<SurveyDetailWithUserDto> GetSurveyDetailById(int surveyId);
        Task<List<SurveyQuestionDto>> GetQuestionById(int surveyId);
        //Task<List<SurveyOption>> GetSurveyOptionList(int quesitonId);
        Task<SurveyUpdateDto> UpdateSurveyDetailsAsync(SurveyUpdateDto surveyUpdateDto);
        Task<SurveyList> DeactivateSurvey(SurveyList surveyUpdateDto);
        Task<int> DeleteQuestion(int quesitonId);
    }
}
