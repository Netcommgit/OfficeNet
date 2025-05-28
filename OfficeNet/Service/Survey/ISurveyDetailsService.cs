using OfficeNet.Domain.Entities;

namespace OfficeNet.Service.Survey
{
    public interface ISurveyDetailsService
    {
        Task<SurveyDetails> SaveSurveyDetailsAsync(SurveyDetails surveyDetails);
        Task<SurveyAuthenticateUser> CreateSurveyAuthenticateUserAsync(SurveyAuthenticateUser surveyAuthenticateUser);
    }
}
