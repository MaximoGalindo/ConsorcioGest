using BusinessService.DTO;
using BusinessService.Enums;
using BusinessService.Models.AuxModel;
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

        public UserService(ConsorcioGestContext consorcio)
        {
            this.context = consorcio;
        }

        public async Task<bool> CreateUser(RegisterUserDTO userDTO)
        {
            if (userDTO != null)
            {
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
                    IdTipoDocumento = userDTO.DocumentType
                };

                await context.AddAsync(usuario);
                var result = await context.SaveChangesAsync();
                if (result > 0)
                    return true;
                else
                    return false;
            }
            else
              return false;
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
