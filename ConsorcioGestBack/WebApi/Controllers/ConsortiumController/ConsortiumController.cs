using BusinessService.Models;
using BusinessService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers.LoginController
{
    [Route("api/consortium")]
    [ApiController]
    public class ConsortiumController : ControllerBase
    {
        public readonly ConsortiumService consortiumService;

        public ConsortiumController(ConsortiumService consortiumService)
        {
            this.consortiumService = consortiumService; 
        }

        /*[HttpPost]
        public IActionResult GenerateLogicConfiguration(ConsortiumConfiguration consortiumConfiguration)
        {
            //Aca es la verdadera pegada a la api. DOnde tengo que mandar la lista de torres
            return null;
        }*/

        [HttpPost]
        public IActionResult GenerateLogicConfiguration(Tower config)
        {
            return Ok(consortiumService.GenerateLogicDepartments(config));
        }

    }
}
