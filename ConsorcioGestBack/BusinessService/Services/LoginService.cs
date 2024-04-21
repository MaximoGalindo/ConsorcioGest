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
using Newtonsoft.Json.Linq;
using BusinessService.Models.AuxModel;

namespace BusinessService.Services
{
    public class LoginService
    {
        private readonly ConsorcioGestContext context;
        private readonly IConfiguration _config;

        public static UserModel CurrentUser {  get; private set; }
        public static ConsortiumModel CurrentConsortium { get; private set; }

        public LoginService(ConsorcioGestContext context, IConfiguration configuration)
        {
            this.context = context;
            this._config = configuration;
        }

        public UserModel Login(LoginUser loginUser)
        {
            if(loginUser != null)
            {
                var user = Authenticate(loginUser);

                if (user != null)
                {
                    var token = GenerateToken(user);
                    GetCurrentUser(token);
                    return CurrentUser;
                }
            }
            return null;
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

        public void GetCurrentUser(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadToken(token) as JwtSecurityToken;

                if (tokenS != null)
                {
                    var claims = tokenS.Claims;
                    var documentUser = Convert.ToInt32(claims.FirstOrDefault(n => n.Type == "Document")?.Value);
                    var email = claims.FirstOrDefault(n => n.Type == ClaimTypes.Email)?.Value;

                    var user = context.Usuarios
                        .Include(u => u.IdPerfilNavigation)
                        .Include(u => u.IdEstadoUsuarioNavigation)
                        .Where(u => u.Documento == documentUser && u.Email == email)
                        .FirstOrDefault();

                    if (user != null)
                    {
                        CurrentUser = new UserModel
                        {
                            Id = user.Id,
                            Name = user.Nombre,
                            LastName = user.Apellido,
                            Document = user.Documento,
                            Phone = user.Telefono,
                            Email = user.Email,
                            IsOwner = user.Espropietario != null ? true : false,
                            IsOcupant = user.Esinquilino != null ? true : false,
                            IdCondominium = user.IdCondominio,
                            IdDocumentType = user.IdTipoDocumento,
                            Profile = new ProfileModel { Id = user.IdPerfil.Value, Name = user.IdPerfilNavigation.Nombre },
                            UserState = new StateModel { Id = user.IdEstadoUsuario.Value, Name = user.IdEstadoUsuarioNavigation.Nombre },
                            Token = token
                        };
                    }
                }
            }
        }

        public ConsortiumModel SetCurrentConsortium(int consortiumID)
        {
            CurrentConsortium = context.Consorcios
                .Where(c => c.Id == consortiumID)
                .Select(c => new ConsortiumModel
                {
                    Id = c.Id,
                    Name = c.Nombre,
                    Location = c.Ubicacion
                }).First();

            return CurrentConsortium != null ? CurrentConsortium : null;
        }
    }
}
