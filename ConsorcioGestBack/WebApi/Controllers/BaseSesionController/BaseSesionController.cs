using BusinessService.Models;
using DataAccess.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Controllers.BaseSesionController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseSesionController : ControllerBase
    {
        private readonly ConsorcioGestContext context;
        private readonly IConfiguration _config;

        public UserModel CurrentUser { get; private set; }

        public BaseSesionController(ConsorcioGestContext context, IConfiguration configuration)
        {
            this.context = context;
            this._config = configuration;
        }

        public string Login(LoginUser loginUser)
        {
            var user = Authenticate(loginUser);

            if (user != null)
            {
                var token = Generate(user);
                return token;
            }
            return "Error";
        }

        private UserModel Authenticate(LoginUser loginUser)
        {
            var currentUser = context.Usuarios
                .Include(u => u.IdPerfilNavigation)
                .Where(u => u.Email == loginUser.Email && u.Contrasenia == loginUser.Password)
                .Select(u => new UserModel
                {
                    Email = u.Email,
                    Name = u.Nombre,
                    LastName = u.Apellido,
                    Phone = u.Telefono,
                    Password = u.Contrasenia,
                    IsOcupant = u.Espropietario,
                    IsOwner = u.Esinquilino,
                    Profile = u.IdPerfilNavigation.Nombre

                }).FirstOrDefault();

            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }

        private string Generate(UserModel userModel)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, userModel.Email),
                new Claim(ClaimTypes.GivenName, userModel.Name),
                new Claim(ClaimTypes.Surname, userModel.LastName),
                new Claim(ClaimTypes.Role, userModel.Profile)
            };

            var token = new JwtSecurityToken(
                            _config["Jwt:Issuer"],
                            _config["Jwt:Audience"],
                            claims,
                            expires: DateTime.Now.AddHours(2),
                            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
