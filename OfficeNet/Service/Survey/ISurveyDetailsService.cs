using OfficeNet.Domain.Contracts;
using OfficeNet.Domain.Entities;

namespace OfficeNet.Service.Survey
{
    public interface ISurveyDetailsService
    {
        Task<SurveyDetails> SaveSurveyDetailsAsync(SurveyDetails surveyDetails, List<string> userList,SurveyQuestion surveyQuestion, List<string> questionType);
        Task<SurveyAuthenticateUser> CreateSurveyAuthenticateUserAsync(SurveyAuthenticateUser surveyAuthenticateUser);
        Task<List<SurveyList>> GetSurveyListAsync();
    }
}
