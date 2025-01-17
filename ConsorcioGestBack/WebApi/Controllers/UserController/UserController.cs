﻿using BusinessService.DTO;
using BusinessService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.UserController
{
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("register")]
        public IActionResult createUser([FromBody] RegisterUserDTO userDTO)
        {
            return Ok(userService.CreateUser(userDTO));
        }

        [HttpGet("get-document-types")]
        public IActionResult getDocumentTypes()
        {
            return Ok(userService.GetDocumentTypes());
        }

        [HttpGet("get-users")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllUsers([FromQuery] FilterUserDTO filterUser)
        {
            return Ok(userService.GetAllUsers(filterUser));
        }

        [HttpGet("get-admin-users")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllAdminUsers()
        {
            return Ok(userService.GetAllAdminUsers());
        }


        [HttpGet("get-user-by-document/{documentUser}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUserByDocument(int documentUser)
        {
            return Ok(userService.GetUserByDocument(documentUser));
        }

        [HttpGet("get-user-by-id/{userID}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUserByID(int userID)
        {
            return Ok(userService.GetUserBydID(userID));
        }

        [HttpPut("update-user/{documentUser}")]
        [Authorize]
        public IActionResult UpdateUser(int documentUser, [FromBody] UpdateUserDTO userDTO)
        {
            return Ok(userService.UpdateUser(documentUser, userDTO));
        }

        [HttpGet("get-profiles")]
        public IActionResult GetProfiles()
        {
            return Ok(userService.GetProfiles());
        }

        [HttpGet("get-states")]
        public IActionResult GetStatus()
        {
            return Ok(userService.GetStatus());
        }
    }

}
