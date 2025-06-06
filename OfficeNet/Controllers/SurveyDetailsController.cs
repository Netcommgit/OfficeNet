﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeNet.Domain.Contracts;
using OfficeNet.Domain.Entities;
using OfficeNet.Filters;
using OfficeNet.Migrations;
using OfficeNet.Service;
using OfficeNet.Service.Survey;

namespace OfficeNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyDetailsController : ControllerBase
    {
        private readonly ISurveyDetailsService _surveyDetailsService;
        private readonly ICurrentUserService _currentUserService;

        public SurveyDetailsController(ISurveyDetailsService surveyDetailsService,ICurrentUserService currentUserService)
        {
            _surveyDetailsService = surveyDetailsService;
            _currentUserService = currentUserService;
        }

        [HttpPost("Save")]
        [Authorize]
        public async Task<IActionResult> SaveSurveyAsync(ServiceSaveRequest surveyData)
        {
            try
            {
                var surveyDetails = surveyData.SurveyDetails;
                var userList = surveyData.UserList;
                var surveyQuestion =  surveyData.surveyQuestion;
                var questionType = surveyData.questionType;

                var userId = _currentUserService.GetUserId();
                surveyDetails.CreatedBy = userId;
                surveyDetails.ModifiedBy = userId;
                surveyDetails.CreatedOn = DateTime.Now;
                surveyDetails.ModifiedOn = DateTime.Now;
                if (surveyDetails == null)
                {
                    return BadRequest();
                }
                var result = await _surveyDetailsService.SaveSurveyDetailsAsync(surveyDetails, userList, surveyQuestion, questionType);
                if (result.SurveyId > 0)
                {
                    return StatusCode(201, new
                    {
                        status = "success",
                        message = "Survey created.",
                        //data = result
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        status = "error",
                        message = "Survey creation failed.",
                        //data = result
                    });
                }
            }
            catch(Exception exx)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "An unexpected error occurred while saving the survey.",
                    Error = exx.Message // you can remove this in production for security reasons
                });
            }
            
        }
        [HttpGet("GetSurveyList")]
        [Authorize]
        public async Task<IActionResult> GetSurveyList()
        {
            try
            {
                var result = await _surveyDetailsService.GetSurveyListAsync();

                if (result != null && result.Any())
                {
                    return Ok(new
                    {
                        status = "success",
                        message = "Survey list retrieved successfully.",
                        data = result
                    });
                }

                return NotFound(new
                {
                    status = "error",
                    message = "No survey data found."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    status = "error",
                    message = "An unexpected error occurred while getting survey data.",
                    error = ex.Message // Optional: remove in production
                });
            }
        }

        [HttpGet("GetSurveyById")]
        [Authorize]
        public async Task<IActionResult> GetSurveyById(int surveyId)
        {
            var survey = await _surveyDetailsService.GetSurveyDetailById(surveyId);
            if(survey != null)
            {
                var questionList =   await  _surveyDetailsService.GetQuestionById(surveyId);
                return StatusCode(200, new{
                    message = "Survey data fetched successfuly.",
                    survey = survey,
                    questionList = questionList
                });
            }

            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "An unexpected error occurred while saving the survey.",
                });
            }
        }

        [HttpPatch("UpdateSurvey")]
        [Authorize]
        public async Task<IActionResult> UpdateSurveyResultAsync(SurveyUpdateDto updateSurvey)
        {
            var surveyId = updateSurvey.surveyId;
            //updateSurvey.surveyDetails.modifiedOn = DateTime.Now;
            var result = await _surveyDetailsService.UpdateSurveyDetailsAsync(updateSurvey);
            return StatusCode(200, new
            {
                message = "Survey data Updated successfuly.",
                statusCode = 200,
                //result = result,
            });
        }
        
        [HttpPost("DeactivateSurvey")]
        [Authorize]
        public async Task<IActionResult> DeactivateSurvey(SurveyList surveyUpdateDto)
        {
            var result = await _surveyDetailsService.DeactivateSurvey(surveyUpdateDto);
            if (result != null)
            {
                return StatusCode(200, new
                {
                    message = "Survey deactivated successfully.",
                    statusCode = 200,
                });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "An unexpected error occurred while deactivating the survey.",
                });
            }
        }
    }
}
