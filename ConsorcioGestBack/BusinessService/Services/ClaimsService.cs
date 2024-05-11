using BusinessService.DTO;
using BusinessService.Enums;
using BusinessService.Services.BaseService;
using DataAccess.Data;
using DataAccess.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BusinessService.Services
{
    public class ClaimsService : BaseService<Reclamo>
    {
        public readonly ConsorcioGestContext _context;
        public ClaimsService(ConsorcioGestContext consorcioGestContext) 
        {
            this._context = consorcioGestContext;   
        }

        public int SaveClaim(SaveClaimDTO claim) 
        {
            Reclamo reclamo = new Reclamo
            {
                Fecha = DateTime.Now,
                Comentario = claim.ProblemDetail,
                IdUsuario = LoginService.CurrentUser.Id,
                IdEstadoReclamo = (int)ClaimsStatesEnum.PENDING,
                IdEspacioAfectado = claim.AffectedSpaceID,
                IdCausaProblema = claim.CauseClaimID,
                NroReclamo = GenerateClaimNumber()
            };
            var result = DBAdd(reclamo, _context);
            return reclamo.Id;
        }


        private string GenerateClaimNumber()
        {
            var lastID = _context.Reclamos.OrderByDescending(r => r.Id).Select(r => r.Id).FirstOrDefault();
            lastID = lastID == null ? 0 : lastID;
            string claimNumber = (lastID + 1).ToString().PadLeft(6, '0');
            return claimNumber;
        }


        public async Task<bool> UploadImage(int idClaim, List<IFormFile> imageDTO)
        {
            List<Imagene> images = new List<Imagene>();   
            using (var memoryStream = new MemoryStream())
            {
                foreach(var image in imageDTO)
                {
                    await image.CopyToAsync(memoryStream);
                    var datosImagen = memoryStream.ToArray();

                    var newImage = new Imagene
                    {
                        DatosImagen = datosImagen,
                        IdReclamo = idClaim
                    };
                    images.Add(newImage);
                }
            }
            var result = DBAddRange(images, _context);
            return result;
        }

        public List<ListItemDTO> GetCauseClaims()
        {
            return _context.CausaProblemas
                .Select(c => new ListItemDTO
                {
                    ID = c.Id,
                    Name = c.Nombre
                }).ToList();
        }
        public List<ListItemDTO> GetAffectedSpace()
        {
            return _context.EspacioAfectados
                .Select(c => new ListItemDTO
                {
                    ID = c.Id,
                    Name = c.Nombre
                }).ToList();
        }
    }
}
