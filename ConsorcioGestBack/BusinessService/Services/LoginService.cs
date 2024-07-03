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
using BusinessService.Enums;
using BusinessService.ResponseDTO;

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

        public ResponseModel<UserModel> Login(LoginUser loginUser)
        {
            var response = new ResponseModel<UserModel>();

            if (loginUser != null)
            {
                var authResponse = Authenticate(loginUser);

                if (authResponse.Success)
                {
                    var token = GenerateToken(authResponse.Data);
                    GetCurrentUser(token);
                    response.Success = true;
                    response.Data = CurrentUser;
                    return response;
                }
                else
                {
                    return authResponse;
                }
            }

            response.Success = false;
            response.Message = "LoginUser no puede ser nulo";
            return response;
        }

        private ResponseModel<UserModel> Authenticate(LoginUser loginUser)
        {
            var response = new ResponseModel<UserModel>();

            var currentUser = context.Usuarios
                .Where(u => u.Email == loginUser.Email && u.Contrasenia == loginUser.Password)
                .Select(u => new UserModel
                {
                    Id = u.Id,
                    Document = u.Documento,
                    Email = u.Email,
                    Profile = new ProfileModel { Id = u.Id, Name = u.IdPerfilNavigation.Nombre}
                })
                .FirstOrDefault();

            if (currentUser != null)
            {
                var user = context.Usuarios
                    .Where(u => u.Id == currentUser.Id)
                    .Select(u => new
                    {
                        ConsorcioUsuarios = u.ConsorcioUsuarios
                            .Select(cu => new
                            {
                                cu.IdConsorcioNavigation.ExpirationDate
                            }).ToList(),
                        Estado = u.IdEstadoUsuario
                    })
                    .FirstOrDefault();

                if (user.ConsorcioUsuarios.Any(c => c.ExpirationDate != null))
                {
                    response.Success = false;
                    response.Message = "Hay un error con su consorcio. Contecte con el Administrador";
                    return response;
                }
                if (user.Estado != 1)
                {
                    response.Success = false;
                    response.Message = "El usuario no se encuentra habilitado para ingresar";
                    return response;
                }

                response.Success = true;
                response.Data = currentUser;
                return response;
            }
            else
            {
                response.Success = false;
                response.Message = "Las credenciales son invalidas";
            }
            return response;
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
                        .Include(u => u.IdCondominioNavigation)
                        .Include(u => u.ConsorcioUsuarios)
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
                            ConsortiumID = user.ConsorcioUsuarios.Where(cu => cu.IdUsuario == user.Id).Select(cu => cu.IdConsorcio).FirstOrDefault(),
                            IdCondominium = user.IdCondominio,
                            Condominio = user.IdCondominioNavigation != null ? user.IdCondominioNavigation.Torre + ' ' + user.IdCondominioNavigation.NumeroDepartamento : string.Empty,
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

        public bool RemoveCurrentConsortium()
        {
            CurrentConsortium = null;
            return true;
        }

        public bool Logout()
        {
            CurrentUser = null;
            CurrentConsortium = null;
            return true;
        }
    }
}
