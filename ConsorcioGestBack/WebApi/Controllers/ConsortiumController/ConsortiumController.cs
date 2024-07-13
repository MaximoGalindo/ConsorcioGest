using BusinessService.Models;
using BusinessService.Services;
using BusinessService.Services.Consortium;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers.LoginController
{
    [Route("consortium")]
    [ApiController]
    public class ConsortiumController : ControllerBase
    {
        public readonly ConsortiumService consortiumService;

        public ConsortiumController(ConsortiumService consortiumService)
        {
            this.consortiumService = consortiumService;
        }

        [HttpPost("generate-logic-configuration")]
        [Authorize(Roles = "Admin")]
        public IActionResult GenerateLogicConfiguration(Tower config)
        {
            return Ok(consortiumService.GenerateLogicDepartments(config));
        }

        [HttpGet("get-consortiums")]
        [Authorize]
        public IActionResult GetAllConsortiums() {

            return Ok(consortiumService.GetAllConsortiums());

        }

        [HttpPost("save-consortium")]
        [Authorize(Roles = "Admin")]
        public IActionResult SaveConsortium(ConsortiumConfig consortiumConfiguration)
        {
            return Ok(consortiumService.SaveConsortium(consortiumConfiguration));
        }

        [HttpGet("get-towers")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetTowers()
        {
            return Ok(consortiumService.GetTowers());
        }

        [HttpGet("get-condominiums")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetCondominiums(string Tower)
        {
            return Ok(consortiumService.GetCondominiums(Tower));
        }

        [HttpGet("get-common-spaces")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetCommonSpaces()
        {
            return Ok(consortiumService.GetCommonSpaces());
        }

        [HttpPost("delete-consortium")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConsortium(int consortiumID)
        {
            return Ok(consortiumService.DeleteConsortium(consortiumID));
        }

        [HttpGet("get-consortium-configurations-by-id/{consortiumID}")]
        public IActionResult GetConfigurationsByID(int consortiumID)
        {
            return Ok(consortiumService.GetConfigurationsByID(consortiumID));
        }

        [HttpPut("edit-consortium")]
        public IActionResult EditConsortium(EditConsortiumDTO consortiumDTO)
        {
            return Ok(consortiumService.EditConsortium(consortiumDTO));
        }

        [HttpGet("search-consortium")]
        public IActionResult SearchConsortium(string name)
        {
            return Ok(consortiumService.SearchConsortium(name));
        }
    }

}
