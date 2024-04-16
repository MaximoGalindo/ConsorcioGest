using BusinessService.Models;
using BusinessService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers.LoginController
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginService loginService;

        public LoginController(LoginService loginService)
        {
            this.loginService = loginService;
        }

        [HttpPost()]
        public IActionResult Login(LoginUser loginUser)
        {
            var user = loginService.Login(loginUser);

            if (user != null)
                return Ok(user);
            else
                return BadRequest("Credenciales inválidas");
        }

        /*[HttpGet("/getUser")]
        [Authorize]
        public IActionResult GetCurrentUser()
        {
            return Ok(loginService.GetCurrentUser((ClaimsIdentity)HttpContext.User.Identity));
        }*/

        [HttpPost("/getConsortium")]
        [Authorize]
        public IActionResult GetCurrentConsortium(int consortiumID)
        {
                return Ok(loginService.GetCurrentConsortium(consortiumID));
        }

    }
}
