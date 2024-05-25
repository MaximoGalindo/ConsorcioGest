using BusinessService.DTO;
using BusinessService.Services;
using BusinessService.Templates;
using DataAccess.Data;
using DataAccess.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace WebApi.Controllers.ClaimController
{
    [Route("claims")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        private readonly ClaimsService claimsService;
        private readonly EmailService emailService;
        public ClaimsController(ClaimsService claimsService, EmailService emailService)
        {
            this.claimsService = claimsService;
            this.emailService = emailService;
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
            return Ok(claimsService.SaveClaimGestion(saveClaimGestion));
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

        [HttpGet("get-claims-by-state/{idState}")]
        public IActionResult GetClaimsByState(int idState)
        {
            return Ok(claimsService.GetAllClaimsByState(idState));
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

        [HttpGet("get-claims-by-user/{userID}")]
        public IActionResult GetClaimsByUserID(int userID)
        {
            return Ok(claimsService.GetClaimsByUserID(userID));
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail(string to)
        {
            string claimNumber = "12345";
            string claimEmailBody = EmailTemplateService.GetTemplate("EmialComplaintReceived", ("claimNumber", claimNumber));
            await emailService.SendEmailAsync(to, "Reclamo Recibido", claimEmailBody);
            return Ok("Correo Enviado");
        }

    }
}
