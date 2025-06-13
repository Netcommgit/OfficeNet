using OfficeNet.Domain.Contracts;
using OfficeNet.Domain.Entities;
using OfficeNet.Migrations;

namespace OfficeNet.Service.Survey
{
    public interface ISurveyDetailsService
    {
        Task<SurveyDetails> SaveSurveyDetailsAsync(SurveyDetails surveyDetails, List<string> userList,SurveyQuestion surveyQuestion, List<string> questionType);
        Task<SurveyAuthenticateUser> CreateSurveyAuthenticateUserAsync(SurveyAuthenticateUser surveyAuthenticateUser);
        Task <List<SurveyList>> GetSurveyListAsync(int pageNo, int pageSize);
        Task<SurveyDetailWithUserDto> GetSurveyDetailById(int surveyId);
        Task<List<SurveyQuestionDto>> GetQuestionById(int surveyId);
        Task<SurveyUpdateDto> UpdateSurveyDetailsAsync(SurveyUpdateDto surveyUpdateDto);
        Task<SurveyList> DeactivateSurvey(SurveyList surveyUpdateDto);
        Task<int> DeleteQuestion(int quesitonId);
        Task<SurveyQuestionResponse> SaveQuestionResponse(SurveyQuestionResponse questionResponse);
        Task<(List<GetSurveyUserList> Users, int TotalCount)> GetSurveyUserListsWithCount(int surveyId, bool isSubmitted, int pageNo, int pageSize);
        Task<List<UsersSurveyList>> UserSurveyList(string userId,bool IsSubmitted, int pageNo, int pageSize);
        Task <SurveyResponse> GetSurveyFlatResult(int surveyId, string userId);
        Task<SaveSurveyResponse> SaveSurveyFlatResultByUserIdAsync(SaveSurveyResponse surveyResponse, string userId);
        Task<List<SurveyResult>> GetSurveyResult(int surveyId);
    }
}
