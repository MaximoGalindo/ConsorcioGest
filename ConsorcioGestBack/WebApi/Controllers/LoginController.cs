using BusinessService.Models;
using BusinessService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
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

        [HttpPost]
        public IActionResult Login(LoginUser loginUser)
        {
            return Ok(loginService.Login(loginUser));
        }

        [HttpGet]
        [Authorize(Roles =("Admin"))]
        public IActionResult Logout()
        {
            return Ok("Hola entre");
        }
      
    }
}
