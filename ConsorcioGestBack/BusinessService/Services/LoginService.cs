using BusinessService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Data.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;

namespace BusinessService.Services
{
    public class LoginService
    {
        private readonly ConsorcioGestContext context;
        private readonly IConfiguration _config;

        public static UserModel CurrentUser {  get; private set; }

        public LoginService(ConsorcioGestContext context, IConfiguration configuration)
        {
            this.context = context;
            this._config = configuration;
        }

        public string Login(LoginUser loginUser)
        {
            var user = Authenticate(loginUser);

            if (user != null)
            {
                var token = GenerateToken(user);
                return token;
            }
            return "Usuario no Encontrado";
        }

        private UserModel Authenticate(LoginUser loginUser)
        {
            var currentUser = context.Usuarios
                .Include(u => u.IdPerfilNavigation)
                .Where(u => u.Email == loginUser.Email && u.Contrasenia == loginUser.Password)
                .Select(u => new UserModel
                {
                    Document = u.Documento,
                    Email = u.Email,
                    Profile = new ProfileModel { Id = u.Id, Name = u.IdPerfilNavigation.Nombre}
                })
                .FirstOrDefault();

            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }

        private string GenerateToken(UserModel userModel)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, userModel.Email),
                new Claim(ClaimTypes.Role, userModel.Profile.Name.ToString()),
                new Claim("Document", userModel.Document.ToString()),
            };

            var token = new JwtSecurityToken(
                            _config["Jwt:Issuer"],
                            _config["Jwt:Audience"],
                            claims,
                            expires: DateTime.Now.AddHours(2),
                            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
        public UserModel GetCurrentUser(ClaimsIdentity identity)
        {
            var userClaims = identity.Claims;
            int documentUser = Convert.ToInt32(userClaims.FirstOrDefault(n => n.Type == "Document")?.Value);
            var email = userClaims.FirstOrDefault(n => n.Type == ClaimTypes.Email)?.Value;

            var user = context.Usuarios
                .Include(u => u.IdPerfilNavigation)
                .Include(u => u.IdEstadoUsuarioNavigation)
                .Where(u => u.Documento == documentUser
                    && u.Email == email)
                .FirstOrDefault();

            CurrentUser = new UserModel
            {
                Id = user.Id,
                Name = user.Nombre,
                LastName = user.Apellido,                    
                Document = user.Documento,
                Phone = user.Telefono,
                Email = user.Email,
                IsOwner = user.Espropietario,
                IsOcupant = user.Esinquilino,
                IdCondominium = user.IdCondominio,                
                IdDocumentType = user.IdTipoDocumento,
                Profile = new ProfileModel { Id = user.IdPerfil.Value, Name = user.IdPerfilNavigation.Nombre },
                UserState = new StateModel { Id = user.IdEstadoUsuario.Value, Name = user.IdEstadoUsuarioNavigation.Nombre},
            };
            return CurrentUser;
        }

        public bool LogOut()
        {
            CurrentUser = null;
            return true;
        }
    }
}
