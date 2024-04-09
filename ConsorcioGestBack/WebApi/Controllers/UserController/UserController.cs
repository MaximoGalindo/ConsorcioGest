using BusinessService.DTO;
using BusinessService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.UserController
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;

        public UserController(UserService userService) 
        {
            this.userService = userService;
        }

        [HttpPost("/register")]
        public IActionResult createUser(RegisterUserDTO user)
        {
            return Ok(userService.CreateUser(user));
        }

        [HttpGet("/getDocumentTypes")]
        public IActionResult getDocumentTypes()
        {
            return Ok(userService.GetDocumentTypes());
        }

        [HttpGet("/getConsortiums")]
        public IActionResult GetConsortiums()
        {
            return Ok(userService.GetConsortiums());
        }

    }

}
