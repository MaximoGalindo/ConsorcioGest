using BusinessService.DTO;
using BusinessService.Enums;
using BusinessService.Models;
using BusinessService.Models.AuxModel;
using BusinessService.ResponseDTO;
using BusinessService.Services.BaseService;
using DataAccess.Data;
using DataAccess.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Services
{
    public class UserService : BaseService<Usuario>
    {
        private readonly ConsorcioGestContext _context;

        public UserService(ConsorcioGestContext context)
        {
            this._context = context;
        }

        public SuccessResponseDTO CreateUser(RegisterUserDTO userDTO)
        {
            try
            {
                if (userDTO != null)
                {
                    var userExist = _context.Usuarios.Where(u => u.Email == userDTO.Email && u.Documento == Convert.ToInt32(userDTO.Document)).FirstOrDefault();

                    if (userExist != null)
                        return new SuccessResponseDTO { Success = false, Message = "Este usuario ya existe" }; 

                    Usuario usuario = new Usuario
                    {
                        Nombre = userDTO.Name,
                        Apellido = userDTO.LastName,
                        Telefono = userDTO.Phone,
                        Email = userDTO.Email,
                        Contrasenia = userDTO.Password,
                        Espropietario = userDTO.UserType == "IsOwner" ? true : false,
                        Esinquilino = userDTO.UserType == "IsOcupant" ? true : false,
                        IdEstadoUsuario = (int)UserStatesEnum.PendienteDeAprobacion,
                        Documento = Convert.ToInt32(userDTO.Document),
                        IdTipoDocumento = userDTO.DocumentType,
                        IdCondominio = null,
                        IdPerfil = null
                    };

                    var result = DBAdd(usuario, _context);

                    ConsorcioUsuario consorcioUsuario = new ConsorcioUsuario
                    {
                        IdUsuario = usuario.Id,
                        IdConsorcio = userDTO.ConsortiumID,
                    };

                    result = DBAdd(consorcioUsuario, _context);

                    if (result)
                        return new SuccessResponseDTO { Success = true, Message = "Usuario creado correctamente" }; 
                    else
                        return new SuccessResponseDTO { Success = false, Message = "Error al crear el usuario" }; ;
                }
                else
                    return new SuccessResponseDTO { Success = false, Message = "Los datos no se enviaron correctamente" }; ; ;
            }catch(Exception e)
            {
                return new SuccessResponseDTO { Success = false, Message = e.Message };
            }
            
        }

        public List<DocumentTypeModel> GetDocumentTypes()
        {
            List<DocumentTypeModel> documentTypeList = _context.TipoDocumentos
                    .Select(t => new DocumentTypeModel
                    {
                        Id = t.Id,
                        Name = t.Nombre,
                    }).ToList();

            return documentTypeList;
        }

        public List<UserModelDTO> GetAllUsers(FilterUserDTO filterUser)
        {
            List<UserModelDTO> userModelDTOs = _context.Usuarios
                .Where(u => (u.ConsorcioUsuarios.Any(cu => cu.IdConsorcio == LoginService.CurrentConsortium.Id)
                && u.IdPerfil != 1)
                && (filterUser.Document != 0 ? u.Documento == filterUser.Document: true)
                && (!string.IsNullOrEmpty(filterUser.Tower) ? u.IdCondominioNavigation.Torre == filterUser.Tower : true)
                && (!string.IsNullOrEmpty(filterUser.Condominium) ? u.IdCondominioNavigation.NumeroDepartamento == filterUser.Condominium : true)
                && (filterUser.UserStateID != 0 ? u.IdEstadoUsuario == filterUser.UserStateID : true))
               .Select(u => new UserModelDTO 
               {
                   Name = u.Nombre + ' ' + u.Apellido,
                   Phone = u.Telefono,
                   Email = u.Email,
                   Document = u.Documento,
                   Profile = u.IdPerfilNavigation != null ? 
                            new ProfileModel 
                            { 
                                Id = u.IdPerfilNavigation.Id, 
                                Name = u.IdPerfilNavigation.Nombre 
                            }: new ProfileModel(),
                   Property = u.Esinquilino == true ? "Inquilino" : "Propietario",
                   State = u.IdEstadoUsuarioNavigation != null ?
                             new StateModel
                             {
                                 Id = u.IdEstadoUsuarioNavigation.Id,
                                 Name = u.IdEstadoUsuarioNavigation.Nombre
                             }: new StateModel(),
                   Condominium =
                    u.IdCondominioNavigation != null ?
                            u.IdCondominioNavigation.Torre                    
                    + ' ' + u.IdCondominioNavigation.NumeroDepartamento
                    : "",

               }).ToList();
            return userModelDTOs;
        }

        public UserModelByDocumentDTO GetUserByDocument(int documentUser)
        {
            UserModelByDocumentDTO userModelDTO = _context.Usuarios
                .Where(u => u.Documento == documentUser)
                .Select(u => new UserModelByDocumentDTO
                {
                    Name = u.Nombre + ' ' + u.Apellido,
                    Phone = u.Telefono,
                    Email = u.Email,
                    Document = u.Documento,
                    Profile = u.IdPerfilNavigation != null ?
                            new ProfileModel
                            {
                                Id = u.IdPerfilNavigation.Id,
                                Name = u.IdPerfilNavigation.Nombre
                            } : new ProfileModel(),
                    Property = u.Esinquilino == true ? "Inquilino" : "Propietario",
                    State = u.IdEstadoUsuarioNavigation != null ?
                             new StateModel
                             {
                                 Id = u.IdEstadoUsuarioNavigation.Id,
                                 Name = u.IdEstadoUsuarioNavigation.Nombre
                             } : new StateModel(),
                    Tower = u.IdCondominioNavigation != null ? u.IdCondominioNavigation.Torre : "",
                    Condominium =
                    u.IdCondominioNavigation != null ? u.IdCondominioNavigation.NumeroDepartamento
                    : "",

                }).First();
            return userModelDTO != null ? userModelDTO : null;
        }

        public int UpdateUser(int userDocument,UpdateUserDTO userDTO)
        {
            Usuario user = _context.Usuarios
                .Where(u => u.Documento == userDocument).First();

            if(LoginService.CurrentUser.Profile.Id == 2)
            {
                user.Telefono = userDTO.Phone != null ? userDTO.Phone : user.Telefono;
                user.Email = userDTO.Email != null ? userDTO.Email : user.Email;
            }
            else
            {
                user.Telefono = userDTO.Phone != null ? userDTO.Phone : user.Telefono;
                user.Email = userDTO.Email != null ? userDTO.Email : user.Email;
                user.IdPerfil = userDTO.IdProfile != null ? userDTO.IdProfile : user.IdPerfil;
                user.IdCondominio = userDTO.IdCondominium != null ? userDTO.IdCondominium : user.IdCondominio;
                user.IdEstadoUsuario = userDTO.IdUserState != null ? userDTO.IdUserState : userDTO.IdUserState;
            }

            _context.Update(user);
            int result = _context.SaveChanges();

            return result;
        }

        public List<ListItemDTO> GetProfiles()
        {
            return _context.Perfils
                    .Select(p => new ListItemDTO
                    {
                        ID = p.Id,
                        Name = p.Nombre,
                    })
                    .ToList();
        }

        public List<ListItemDTO> GetStatus()
        {
            return _context.EstadoUsuarios
                        .Select(e => new ListItemDTO
                        {
                            ID = e.Id,
                            Name = e.Nombre
                        })
                        .ToList();
        }
    }
}
