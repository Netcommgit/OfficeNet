using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using NuGet.DependencyResolver;
using OfficeNet.Domain.Contracts;
using OfficeNet.Domain.Entities;
using OfficeNet.Infrastructure.Context;
using OfficeNet.Migrations;
using OfficeNet.Service.UserService;
using System.Globalization;
using System.Linq;

namespace OfficeNet.Service.Survey
{
    public class SurveyDetailsImple : ISurveyDetailsService
    {
        private readonly ILogger<SurveyDetailsImple> _logger;
        private readonly ApplicationDbContext _context;

        public SurveyDetailsImple(ILogger<SurveyDetailsImple> logger, ApplicationDbContext context)
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
                    surveyQuestion.QuestionOrder = _context.SurveyQuestions.Select(u => u.QuestionOrder).DefaultIfEmpty(0).Max()+1; // Assuming you want to set the order based on existing questions
                    _context.SurveyQuestions.Add(surveyQuestion);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Survey Auth saved successfully");

                    if (surveyQuestion.QuestionId != 0 && surveyQuestion.QuestionId != null)
                    {
                        for (int i = 0; i < questionType.Count; i++)
                        {
                            var surveyOption = new SurveyOption();
                            surveyOption.QuestionId = surveyQuestion.QuestionId;
                            surveyOption.OptionText = questionType[i];
                            surveyOption.OptionOrder = i + 1;
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
            catch (Exception ex)
            {
                var errors = ex.Message;
                _logger.LogError($"Failed to Save Survey :{errors}", errors);
                throw new Exception($"Failed to save Survey :{errors}");
            }
        }

        public Task<SurveyAuthenticateUser> CreateSurveyAuthenticateUserAsync(SurveyAuthenticateUser surveyAuthenticateUser)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SurveyList>> GetSurveyListAsync()
        {
            try
            {
                var result = _context.SurveyListData.FromSqlRaw("EXEC SP_GetSurveyList").ToList();
                return result;
            }
            catch (Exception exx)
            {
                _logger.LogError("Some error Occuredd", exx.Message);
                throw new Exception("Some error Occuredd");
            }
        }

        public async Task<SurveyDetailWithUserDto> GetSurveyDetailById(int surveyId)
        {
            try
            {
                //var result = await _context.SurveyDetail.FirstOrDefaultAsync(x => x.SurveyId == surveyId);

                var result = await (from survey in _context.SurveyDetail
                                    where survey.SurveyId == surveyId
                                    select new SurveyDetailWithUserDto
                                    {
                                        SurveyId = survey.SurveyId,
                                        SurveyName = survey.SurveyName,
                                        SurveyStart = survey.SurveyStart,
                                        SurveyEnd = survey.SurveyEnd,
                                        SurveyInstruction = survey.SurveyInstruction,
                                        SurveyConfirmation = survey.SurveyConfirmation,
                                        SurveyView = survey.SurveyView,
                                        AuthView = survey.AuthView,
                                        PlantId = survey.PlantId,
                                        DepartmentId = survey.DepartmentId,
                                        //IsExcel = survey.IsExcel,
                                        //SurveyStatus = survey.SurveyStatus,
                                        //Archieve = survey.Archieve,
                                        //CreatedBy = survey.CreatedBy,
                                        //CreatedOn = survey.CreatedOn,
                                        //ModifiedBy = survey.ModifiedBy,
                                        //ModifiedOn = survey.ModifiedOn,

                                        userLists = (from su in _context.SurveyAuthenticateUsers
                                                     join u in _context.Users on su.UserId equals u.Id
                                                     where su.SurveyId == survey.SurveyId
                                                     select new SurveyUserList
                                                     {
                                                         Id = u.Id,
                                                         FirstName = u.FirstName,
                                                         LastName = u.LastName,
                                                     }).ToList(),
                                    }).FirstOrDefaultAsync();

                if (result == null)
                {
                    _logger.LogWarning($"SurveyDetail with ID {surveyId} not found.");
                    throw new KeyNotFoundException($"SurveyDetail with ID {surveyId} not found.");
                }
                _logger.LogInformation($"Survey Data fetched successfuly {surveyId}");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception($"Some Error Occured{ex}");
            }

        }

        public async Task<List<SurveyQuestionDto>> GetQuestionById(int surveyId)
        {
            try
            {
                var questionsWithOptions = await _context.SurveyQuestions.Where(q => q.SurveyId == surveyId).Select(q => new SurveyQuestionDto
                {
                    QuestionId = q.QuestionId,
                    SurveyId = q.SurveyId,
                    QuestionText = q.QuestionText,
                    //QuestionRequierd = q.QuestionRequierd,
                    //QuestionErrorMsg = q.QuestionErrorMsg,
                    //QuestionViewReport = q.QuestionViewReport,
                    QuestionType = q.QuestionType,
                    //QuestionOrder = q.QuestionOrder,
                    //Status = q.Status,
                    //Archieve = q.Archieve,
                    Options = _context.SurveyOptions.Where(o => o.QuestionId == q.QuestionId)
                        .Select(o => new SurveyOptionDto
                        {
                            OptionId = o.OptionId,
                            OptionText = o.OptionText,
                            OptionOrder = o.OptionOrder,
                            Status = o.Status,
                            Archive = o.Archive
                        }).ToList()
                }).ToListAsync();

                if (questionsWithOptions == null)
                {
                    _logger.LogWarning($"Survey Question with ID {surveyId} not found.");
                    throw new KeyNotFoundException($"Survey Question with ID {surveyId} not found.");
                }
                _logger.LogInformation($"Survey Question fetched successfuly {surveyId}");
                return questionsWithOptions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception($"Some Error Occured{ex}");
            }
        }

        public async Task<SurveyUpdateDto> UpdateSurveyDetailsAsync(SurveyUpdateDto surveyUpdateDto)
        {

            try
            {
                // 1. Get existing SurveyDetails from DB
                var existingSurveyDetails = await _context.SurveyDetail
                    .FirstOrDefaultAsync(s => s.SurveyId == surveyUpdateDto.surveyId);

                if (existingSurveyDetails == null)
                {
                    throw new Exception($"SurveyDetails with SurveyId {surveyUpdateDto.surveyId} not found.");
                }
                var updatedSurveyDetails = surveyUpdateDto.surveyDetails;
                if (updatedSurveyDetails != null)
                {
                    if (!string.IsNullOrEmpty(updatedSurveyDetails.surveyName))
                        existingSurveyDetails.SurveyName = updatedSurveyDetails.surveyName;
                    var parsedStart = DateHelper.ParseDate(updatedSurveyDetails.surveyStart);
                    if (parsedStart != DateTime.MinValue)
                        existingSurveyDetails.SurveyStart = parsedStart;

                    var parsedEnd = DateHelper.ParseDate(updatedSurveyDetails.surveyEnd);
                    if (parsedEnd != DateTime.MinValue)
                        existingSurveyDetails.SurveyEnd = parsedEnd;
                    if (!string.IsNullOrEmpty(updatedSurveyDetails.surveyInstruction))
                        existingSurveyDetails.SurveyInstruction = updatedSurveyDetails.surveyInstruction;
                    if (!string.IsNullOrEmpty(updatedSurveyDetails.surveyConfirmation))
                        existingSurveyDetails.SurveyConfirmation = updatedSurveyDetails.surveyConfirmation;
                    if (updatedSurveyDetails.surveyView.HasValue)
                        existingSurveyDetails.SurveyView = updatedSurveyDetails.surveyView.Value;
                    if (updatedSurveyDetails.authView.HasValue)
                        existingSurveyDetails.AuthView = updatedSurveyDetails.authView.Value;
                    if (updatedSurveyDetails.plantId.HasValue)
                        existingSurveyDetails.PlantId = updatedSurveyDetails.plantId.Value;
                    if (updatedSurveyDetails.departmentId.HasValue)
                        existingSurveyDetails.DepartmentId = updatedSurveyDetails.departmentId.Value;

                    existingSurveyDetails.ModifiedBy = updatedSurveyDetails.modifiedBy;
                    existingSurveyDetails.ModifiedOn = updatedSurveyDetails.modifiedOn;
                }
                //var surveyAuthenticateUser = new List<SurveyAuthenticateUser>();
                var updateUserList = surveyUpdateDto.userList;
                if (updateUserList != null)
                {
                    var surveyAuthenticateUser = updateUserList
                        .Select(uid => new SurveyAuthenticateUser
                        {
                            SurveyId = surveyUpdateDto.surveyId,
                            UserId = uid,
                            PlantId = existingSurveyDetails.PlantId,
                            CreatedOn = DateTime.Now,
                            CreatedBy = surveyUpdateDto.surveyDetails.modifiedBy
                        })
                        .ToList();


                    var existingAuthUsers = _context.SurveyAuthenticateUsers
                        .Where(u => u.SurveyId == surveyUpdateDto.surveyId).ToList();

                    var updatedUserIds = surveyAuthenticateUser.Select(u => u.UserId).ToHashSet();

                    // Remove users not in the updated list
                    var usersToRemove = existingAuthUsers
                        .Where(u => !updatedUserIds.Contains(u.UserId)).ToList();
                    _context.SurveyAuthenticateUsers.RemoveRange(usersToRemove);

                    // Add new users that don't exist yet
                    var existingUserIds = existingAuthUsers.Select(u => u.UserId).ToHashSet();
                    var usersToAdd = surveyAuthenticateUser
                        .Where(u => !existingUserIds.Contains(u.UserId))
                        .ToList();
                    _context.SurveyAuthenticateUsers.AddRange(usersToAdd);
                }

                var updatedSurveyQuestion = surveyUpdateDto.surveyQuestion;
                if (updatedSurveyQuestion != null)
                {

                    var existingQuestion = await _context.SurveyQuestions
                        .FirstOrDefaultAsync(q => q.SurveyId == surveyUpdateDto.surveyId);

                    if (existingQuestion != null)
                    {
                        // Update existing question fields
                        if (!string.IsNullOrEmpty(updatedSurveyQuestion.questionText))
                            existingQuestion.QuestionText = updatedSurveyQuestion.questionText;
                        if (updatedSurveyQuestion.questionType.HasValue)
                            existingQuestion.QuestionType = updatedSurveyQuestion.questionType;

                        //existingQuestion.QuestionOrder = updatedSurveyQuestion.questionOrder.Value;
                    }
                    else
                    {
                        // Add new question if none exists
                        var AddSurveyQuestion = new SurveyQuestion
                        {
                            SurveyId = surveyUpdateDto.surveyId,
                            QuestionText = surveyUpdateDto.surveyQuestion.questionText,
                            QuestionType = surveyUpdateDto.surveyQuestion.questionType,
                            QuestionOrder = _context.SurveyQuestions
                                .Where(q => q.SurveyId == surveyUpdateDto.surveyId)
                                .Select(q => q.QuestionOrder)
                                .DefaultIfEmpty(0)
                                .Max() + 1, // Set order based on existing questions
                        };

                        _context.SurveyQuestions.Add(AddSurveyQuestion);
                        await _context.SaveChangesAsync();
                        existingQuestion = AddSurveyQuestion;
                    }



                    var updatedQuestionTypes = surveyUpdateDto.surveyQuestion.options;

                    var existingOptions = _context.SurveyOptions
                        .Where(o => o.QuestionId == existingQuestion.QuestionId)
                        .ToList();

                    // Extract option texts from the updated list
                    var updatedOptionTexts = updatedQuestionTypes
                        .Select(opt => opt.optionText)
                        .ToHashSet();

                    // Find old options that are no longer present
                    var optionsToRemove = existingOptions
                        .Where(o => !updatedOptionTexts.Contains(o.OptionText))
                        .ToList();

                    _context.SurveyOptions.RemoveRange(optionsToRemove);



                    var existingOptionTexts = existingOptions.Select(o => o.OptionText).ToHashSet();

                    var optionsToAdd = updatedQuestionTypes
                        .Where(opt => !existingOptionTexts.Contains(opt.optionText))
                        .Select((optText, idx) => new SurveyOption
                        {
                            QuestionId = existingQuestion.QuestionId,
                            OptionText = optText.optionText,
                            OptionOrder = idx + 1,
                            Status = true,
                            Archive = false
                        }).ToList();

                    _context.SurveyOptions.AddRange(optionsToAdd);
                }

                var questionOrder = surveyUpdateDto.questionOrder;
                if (questionOrder != null)
                {
                    // 1. Extract all questionIds from the provided list
                    var providedQuestionIds = questionOrder.Select(q => q.questionId).ToList();

                    // 2. Get all existing questions for the given survey
                    int surveyId = questionOrder.FirstOrDefault()?.surveyId ?? 0;
                    var existingQuestions = await _context.SurveyQuestions
                        .Where(q => q.SurveyId == surveyId)
                        .ToListAsync();

                    // 3. Identify questions to remove (not in the provided list)
                    var questionsToRemove = existingQuestions
                        .Where(q => !providedQuestionIds.Contains(q.QuestionId))
                        .ToList();

                    // 4. If any questions are to be removed, also remove their related options
                    if (questionsToRemove.Any())
                    {
                        var questionIdsToRemove = questionsToRemove.Select(q => q.QuestionId).ToList();

                        var optionsToRemove = await _context.SurveyOptions
                            .Where(o => questionIdsToRemove.Contains(o.QuestionId.Value))
                            .ToListAsync();

                        if (optionsToRemove.Any())
                        {
                            _context.SurveyOptions.RemoveRange(optionsToRemove);
                        }

                        _context.SurveyQuestions.RemoveRange(questionsToRemove);
                    }

                    // 5. Update order of remaining questions based on input
                    int orderCounter = 0;
                    foreach (var order in questionOrder)
                    {
                        var existingQuestion = existingQuestions.FirstOrDefault(q => q.QuestionId == order.questionId);
                        if (existingQuestion != null)
                        {
                            existingQuestion.QuestionOrder = ++orderCounter;
                            _context.SurveyQuestions.Update(existingQuestion);
                        }
                    }

                    // 6. Save all changes
                    await _context.SaveChangesAsync();

                    //int orderCounter = 0;
                    //foreach (var order in questionOrder)
                    //{
                    //    var existingQuestion = await _context.SurveyQuestions
                    //        .FirstOrDefaultAsync(q => q.QuestionId == order.questionId && q.SurveyId == order.surveyId);
                    //    if (existingQuestion != null)
                    //    {
                    //        existingQuestion.QuestionOrder = orderCounter+1;
                    //        _context.SurveyQuestions.Update(existingQuestion);
                    //    }
                    //}
                }
                // Save all changes
                await _context.SaveChangesAsync();

                _logger.LogInformation("Survey details patched successfully.");

                //return existingSurveyDetails;
                return new SurveyUpdateDto
                {
                    surveyId = surveyUpdateDto.surveyId
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to patch Survey: {ex.Message}", ex);
                throw;
            }
        }

        public async Task<SurveyList> DeactivateSurvey(SurveyList surveyUpdateDto)
        {
            try
            {
                if (surveyUpdateDto.SurveyId == null)
                {
                    throw new ArgumentException("SurveyId cannot be null.");
                }
                await _context.SurveyDetail
                    .Where(s => s.SurveyId == surveyUpdateDto.SurveyId)
                    .ExecuteUpdateAsync(s => s.SetProperty(e => e.Archieve, surveyUpdateDto.Archieve));

                var updatedSurvey = await _context.SurveyDetail
                    .Where(s => s.SurveyId == surveyUpdateDto.SurveyId)
                    .Select(s => new SurveyList
                    {
                        Archieve = s.Archieve,
                        SurveyStatus = s.SurveyStatus
                        // add others as needed
                    })
                    .FirstOrDefaultAsync();
                return updatedSurvey;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to deactivate survey: {ex.Message}", ex);
                throw new Exception($"Failed to deactivate survey: {ex.Message}");
            }
        }

        public async Task<int> DeleteQuestion(int quesitonId)
        {
            try { 
            var question = await _context.SurveyQuestions.FirstOrDefaultAsync(q => q.QuestionId == quesitonId);
            if (question != null)
            {
                var options = await _context.SurveyOptions
                    .Where(o => o.QuestionId == quesitonId)
                    .ToListAsync();
                _context.SurveyOptions.RemoveRange(options);
                _context.SurveyQuestions.Remove(question);
                var result = await _context.SaveChangesAsync();
                _logger.LogInformation("Question Deleted successfully.");
                return result;
            }
            else
            {
                    _logger.LogWarning($"Question with ID {quesitonId} not found.");
                    return 0;
                }
            }
            catch(Exception e)
            {
                _logger.LogError($"Error while deleting question");
                throw new Exception("An error occurred while deleting the question.", e);
            }
        }

 
    }
}
