using BusinessService.DTO;
using BusinessService.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.ReservationsController
{
    [Route("survey")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly SurveyService surveyService;

        public SurveyController(SurveyService surveyService)
        {
            this.surveyService = surveyService;
        }

        [HttpGet("check-survey-completed/{surveyID}")]
        public IActionResult CheckSurveyCompleted(int surveyID)
        {
            return Ok(surveyService.CheckIfSurveyCompleted(surveyID));
        }

        [HttpGet("get-survey-questions-options")]
        public IActionResult GetQuestionSurvey()
        {
            return Ok(surveyService.GetQuestionSurvey());
        }

        [HttpPost("save-reply-survey")]
        public IActionResult SaveReplySurvey(ReplySurveyDTO replySurvey)
        {
            return Ok(surveyService.SaveReplySurvey(replySurvey));
        }

        [HttpGet("get-surveys")]
        public IActionResult GetSurveys([FromQuery] FilterSurveyDTO filterSurvey)
        {
            return Ok(surveyService.GetSurveys(filterSurvey));
        }

        [HttpGet("get-survey-detail/{surveyID}")]
        public IActionResult GetSurveyDetailById(int surveyID)
        {
            return Ok(surveyService.GetSurveyDetailById(surveyID));
        }

        [HttpGet("get-survey-states")]
        public IActionResult GetSurveyStates()
        {
            return Ok(surveyService.GetSurveyStates());
        }
    }
}
