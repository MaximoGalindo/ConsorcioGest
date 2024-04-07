using BusinessService.DTO;
using BusinessService.Enums;
using BusinessService.Models.AuxModel;
using BusinessService.ResponseDTO;
using DataAccess.Data;
using DataAccess.Data.Models;
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
                        IdEstadoUsuario = (int)UserStates.PendienteDeAprobacion,
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
    }
}
