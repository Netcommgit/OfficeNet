using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeNet.Domain.Entities;
using OfficeNet.Filters;
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
        public async Task<IActionResult> SaveSurveyAsync(SurveyDetails surveyDetails)
        {
            var userId =  _currentUserService.GetUserId();
            surveyDetails.CreatedBy = userId;
            surveyDetails.ModifiedBy = userId;
            surveyDetails.CreatedOn = DateTime.Now;
            surveyDetails.ModifiedOn = DateTime.Now;
            if (surveyDetails == null)
            {
                return BadRequest();
            }
            var result =  await _surveyDetailsService.SaveSurveyDetailsAsync(surveyDetails);
            return Ok(result);
        }
    }
}
