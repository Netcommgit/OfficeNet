using OfficeNet.Domain.Entities;

namespace OfficeNet.Service.Survey
{
    public interface ISurveyDetailsService
    {
        Task<SurveyDetails> SaveSurveyDetailsAsync(SurveyDetails surveyDetails);
    }
}
