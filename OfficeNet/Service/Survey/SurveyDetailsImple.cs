using Microsoft.AspNetCore.Mvc;
using OfficeNet.Domain.Entities;
using OfficeNet.Infrastructure.Context;
using OfficeNet.Migrations;
using OfficeNet.Service.UserService;

namespace OfficeNet.Service.Survey
{
    public class SurveyDetailsImple : ISurveyDetailsService
    {
        private readonly ILogger<SurveyDetailsImple> _logger;
        private readonly ApplicationDbContext _context;
        
        public SurveyDetailsImple(ILogger<SurveyDetailsImple> logger , ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

       
        public async Task<SurveyDetails> SaveSurveyDetailsAsync(SurveyDetails surveyDetails)
        {
            try
            {
                _context.SurveyDetail.Add(surveyDetails);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Survey details saved Succesfully");

                _logger.LogInformation("Survey details saved successfully.");
                return surveyDetails;
            }
            catch (Exception ex) {
                var errors = ex.Message;
                _logger.LogError($"Failed to Save Survey :{errors}", errors);
                throw new Exception($"Failed to save Survey :{errors}");
            }
        }

        public Task<SurveyAuthenticateUser> CreateSurveyAuthenticateUserAsync(SurveyAuthenticateUser surveyAuthenticateUser)
        {
            throw new NotImplementedException();
        }
    }
}
