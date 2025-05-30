using Microsoft.AspNetCore.Mvc;
using OfficeNet.Domain.Contracts;
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

       
        public async Task<SurveyDetails> SaveSurveyDetailsAsync(SurveyDetails surveyDetails, List<string> userList, SurveyQuestion surveyQuestion, List<string> questionType)
        {
            try
            {
                 _context.SurveyDetail.Add(surveyDetails);
                 await _context.SaveChangesAsync();
                _logger.LogInformation("Survey details saved successfully.");
                //var authUser = new List<SurveyAuthenticateUser>();
                if (surveyDetails.SurveyId != null && surveyDetails.SurveyId != 0)
                {
                    for (int i = 0; i < userList.Count; i++)
                    {
                        var surveyAuthUser = new SurveyAuthenticateUser();
                        surveyAuthUser.SurveyId = surveyDetails.SurveyId;
                        surveyAuthUser.PlantId = surveyDetails.PlantId;
                        surveyAuthUser.UserId = userList[i];
                        _context.SurveyAuthenticateUsers.Add(surveyAuthUser);
                    }
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Survey Auth saved successfully");

                    surveyQuestion.SurveyId = surveyDetails.SurveyId;
                    _context.SurveyQuestions.Add(surveyQuestion);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Survey Auth saved successfully");

                    if (surveyQuestion.QuestionId != 0 && surveyQuestion.QuestionId != null)
                    {
                        for (int i = 0; i < questionType.Count; i++)
                        {
                            var surveyOption =  new SurveyOption();
                            surveyOption.QuestionId =  surveyQuestion.QuestionId;
                            surveyOption.OptionText = questionType[i];
                            surveyOption.OptionOrder = i+1;
                            surveyOption.Status = true;
                            surveyOption.Archive = false;
                            _context.SurveyOptions.Add(surveyOption);
                        }
                        await _context.SaveChangesAsync();
                        _logger.LogInformation("Survey option saved successfully");
                    }

                }
               
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
