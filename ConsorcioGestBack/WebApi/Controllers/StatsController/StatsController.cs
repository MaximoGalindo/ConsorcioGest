using BusinessService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace WebApi.Controllers.StatsController
{
    [Route("stats")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly StatsService statsService;

        public StatsController(StatsService statsService)
        {
            this.statsService = statsService;
        }

        [HttpGet("get-most-frequent-coplaints-by-cause-complaint")]
        public IActionResult GetMostFrequentComplaintsByCauseOfComplaint([FromQuery] DateTime? dateFrom, [FromQuery] DateTime? dateTo)
        {
            return Ok(statsService.GetMostFrequentComplaintsByCauseOfComplaint(dateFrom,dateTo));
        }

        [HttpGet("get-number-of-claims-per-months")]
        public IActionResult GetNumberOfClaimsPerMonths()
        {
            return Ok(statsService.GetNumberOfClaimsPerMonths());
        }

        [HttpGet("get-number-of-gestion-claims-per-month/{month},{year}")]
        public IActionResult GetNumberOfGestionClaimsPerMonth(int month,int year)
        {
            return Ok(statsService.GetNumberOfGestionClaimsPerMonth(month,year));
        }
    }
}
