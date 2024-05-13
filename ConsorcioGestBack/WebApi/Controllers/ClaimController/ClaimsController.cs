using BusinessService.DTO;
using BusinessService.Services;
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
        public ClaimsController(ClaimsService claimsService)
        {
            this.claimsService = claimsService;
        }
        //PONERLE AUTHORIZE A TODOS
        [HttpPost("save-claim-user")]
        public async Task<IActionResult> SaveClaim([FromForm] SaveClaimDTO saveClaimDTO)
        {
            return Ok(claimsService.SaveClaim(saveClaimDTO));
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


        /*[HttpGet("{id}")]
        public IActionResult ObtenerImagen(int id)
        {
            var imagen = consorcioGestContext.Imagenes.FirstOrDefault(i => i.Id == id);
            if (imagen == null)
            {
                return NotFound("No se encontró ninguna imagen con el ID especificado.");
            }

            return File(imagen, "image/jpeg"); // Ajusta el tipo de contenido según el formato de tus imágenes
        }*/

    }
}
