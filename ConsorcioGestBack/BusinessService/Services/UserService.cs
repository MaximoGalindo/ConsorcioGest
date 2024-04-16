using BusinessService.DTO;
using BusinessService.Enums;
using BusinessService.Models;
using BusinessService.Models.AuxModel;
using BusinessService.ResponseDTO;
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
    public class UserService
    {
        private readonly ConsorcioGestContext context;

        public UserService(ConsorcioGestContext context)
        {
            this.context = context;
        }

        public SuccessResponseDTO CreateUser(RegisterUserDTO userDTO)
        {
            try
            {
                if (userDTO != null)
                {
                    var userExist = context.Usuarios.Where(u => u.Email == userDTO.Email && u.Documento == Convert.ToInt32(userDTO.Document));

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

                    context.Add(usuario);
                    var result = context.SaveChanges();
                    if (result > 0)
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
            List<DocumentTypeModel> documentTypeList = context.TipoDocumentos
                    .Select(t => new DocumentTypeModel
                    {
                        Id = t.Id,
                        Name = t.Nombre,
                    }).ToList();

            return documentTypeList;
        }

        public List<ConsortiumModel> GetConsortiums()
        {
            List<ConsortiumModel> consortiumList = context.Consorcios
                    .Select(t => new ConsortiumModel
                    {
                        Id = t.Id,
                        Name = t.Nombre,
                        Location = t.Ubicacion
                    }).ToList();

            return consortiumList;
        }

        public List<UserModelDTO> GetAllUsers()
        {
            List<UserModelDTO> userModelDTOs = context.Usuarios
                .Where(u => 
                u.ConsorcioUsuarios.Any(cu => cu.IdConsorcio == LoginService.CurrentConsortium.Id)
                && u.IdPerfil != 1)
               .Select(u => new UserModelDTO 
               {
                   Name = u.Nombre + ' ' + u.Apellido,
                   Phone = u.Telefono,
                   Email = u.Email,
                   Document = u.Documento,
                   Profile = u.IdPerfilNavigation != null ? u.IdPerfilNavigation.Nombre : "",
                   Property = u.Esinquilino == true ? "Inquilino" : "Dueño",
                   State = u.IdEstadoUsuarioNavigation.Nombre,
                   //A CHEKAR LA PARTE ESTA DEL ARMADO DEL CONDOMINIO
                   Condominium =
                    u.IdCondominioNavigation != null ?
                            u.IdCondominioNavigation.Torre
                    + ' ' + u.IdCondominioNavigation.Piso
                    + ' ' + u.IdCondominioNavigation.Departamento
                    : "",

               }).ToList();
            return userModelDTOs;
        }

        public UserModelDTO GetUserByDocument(int documentUser)
        {
            UserModelDTO userModelDTO = context.Usuarios
                .Where(u => u.Documento == documentUser)
                .Select(u => new UserModelDTO
                {
                    Name = u.Nombre + ' ' + u.Apellido,
                    Phone = u.Telefono,
                    Email = u.Email,
                    Document = u.Documento,
                    Profile = u.IdPerfilNavigation != null ? u.IdPerfilNavigation.Nombre : "",
                    Property = u.Esinquilino == true ? "Inquilino" : "Dueño",
                    State = u.IdEstadoUsuarioNavigation != null ? u.IdEstadoUsuarioNavigation.Nombre : "",
                    //A CHEKAR LA PARTE ESTA DEL ARMADO DEL CONDOMINIO
                    Condominium =
                    u.IdCondominioNavigation != null ?
                            u.IdCondominioNavigation.Torre
                    + ' ' + u.IdCondominioNavigation.Piso
                    + ' ' + u.IdCondominioNavigation.Departamento
                    : "",

                }).First();
            return userModelDTO != null ? userModelDTO : null;
        }

        public int UpdateUser(int userDocument,UpdateUserDTO userDTO)
        {
            Usuario user = context.Usuarios
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

            context.Update(user);
            int result = context.SaveChanges();

            return result;
        }
    }
}
