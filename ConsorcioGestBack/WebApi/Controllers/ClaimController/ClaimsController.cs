using BusinessService.DTO;
using BusinessService.Services;
using BusinessService.Templates;
using DataAccess.Data;
using DataAccess.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace WebApi.Controllers.ClaimController
{
    [Route("claims")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        private readonly ClaimsService claimsService;
        private readonly EmailService emailService;
        private readonly SurveyService surveyService;
        public ClaimsController(ClaimsService claimsService, EmailService emailService, SurveyService surveyService)
        {
            this.claimsService = claimsService;
            this.emailService = emailService;
            this.surveyService = surveyService;
        }
        //PONERLE AUTHORIZE A TODOS
        [HttpPost("save-claim-user")]
        public async Task<IActionResult> SaveClaim([FromForm] SaveClaimDTO saveClaimDTO)
        {
            return Ok(claimsService.SaveClaimByUser(saveClaimDTO));
        }
        [HttpPost("save-claim-gestion")]
        public async Task<IActionResult> SaveClaimGestion(SaveClaimGestionDTO saveClaimGestion)
        {
            return Ok(await claimsService.SaveClaimGestion(saveClaimGestion));
        }

        [HttpGet("histoty-claim-list/{claimID}")]
        public IActionResult GetHistoryClaim(int claimID)
        {
            return Ok(claimsService.GetHistoryClaim(claimID));
        }

        [HttpGet("cause-claims-list")]
        public IActionResult GetCauseClaims()
        {
            return Ok(claimsService.GetCauseClaims());
        }

        [HttpGet("affected-space-list")]
        public IActionResult GetAffectedSpace()
        {
            return Ok(claimsService.GetAffectedSpace());
        }

        [HttpGet("states-claims-list")]
        public IActionResult GetStatesClaims()
        {
            return Ok(claimsService.GetStatesClaim());
        }

        [HttpGet("get-all-claims")]
        public IActionResult GetAllClaims([FromQuery] FilterClaimDTO filterClaim)
        {
            return Ok(claimsService.GetAllClaims(filterClaim));
        }

        [HttpGet("get-claims-count-by-state")]
        public IActionResult GetCountClaimsByState()
        {
            return Ok(claimsService.GetCountClaimsByState());
        }

        [HttpGet("{id}")]
        public IActionResult GetImagesByClaimID(int id)
        {
            return Ok(claimsService.GetImagesByReclamoId(id));
        }

        [HttpGet("get-claims-by-user")]
        public IActionResult GetClaimsByUser([FromQuery] FilterClaimUserDTO filterClaimUser)
        {
            return Ok(claimsService.GetClaimsByUser(filterClaimUser));
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail(string to)
        {
            var surveyLink = $"http://localhost:4200/claim-survey/1";

            var bodyEmail = EmailTemplateService.GetTemplate("EmailReplySurvey", ("claimNumber", "123453"), ("surveyLink", surveyLink));
            ;
            await emailService.SendEmailAsync("galimaximo@gmail.com", "Reclamo Resuelto", bodyEmail);
            return Ok("Correo Enviado");
        }

        [HttpPut("delete-claim")]
        public IActionResult DeleteClaim(int claimID)
        {
            return Ok(claimsService.DeleteClaim(claimID));
        }
    }
}
