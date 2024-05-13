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

        public async Task<string> SaveClaim(SaveClaimDTO claim) 
        {
            try
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
                var result2 = await UploadImages(reclamo.Id, claim.Files);
                return reclamo.NroReclamo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private string GenerateClaimNumber()
        {
            var lastID = _context.Reclamos.OrderByDescending(r => r.Id).Select(r => r.Id).FirstOrDefault();
            lastID = lastID == null ? 0 : lastID;
            string claimNumber = (lastID + 1).ToString().PadLeft(6, '0');
            return claimNumber;
        }


        private async Task<bool> UploadImages(int idClaim, List<IFormFile> imageDTO)
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

        public List<ClaimDTO> GetAllClaimsByState(int idState)
        {
            List<ClaimDTO> claimDTOs = _context.Reclamos
                        .Where(c => c.IdUsuarioNavigation.ConsorcioUsuarios.Any(x => x.IdConsorcio == LoginService.CurrentConsortium.Id)
                            && (idState != 0 ? c.IdEstadoReclamo == idState : true))
                        .Select(c => new ClaimDTO
                        {
                            ClaimNumber = c.NroReclamo,
                            StateId = c.IdEstadoReclamo,
                            State = c.IdEstadoReclamoNavigation.Nombre,
                            CauseClaimID = c.IdCausaProblema,
                            CauseClaim = c.IdCausaProblemaNavigation.Nombre,
                            Date = c.Fecha.ToString(),
                            Condominium = c.IdUsuarioNavigation.IdCondominioNavigation.Torre + ' ' + c.IdUsuarioNavigation.IdCondominioNavigation.NumeroDepartamento,
                            AffectedSpaceID = c.IdEspacioAfectado,
                            AffectedSpace = c.IdEspacioAfectadoNavigation.Nombre,
                            UserID = c.IdUsuario,
                            ProblemDetail = c.Comentario
                        }).ToList();
            return claimDTOs;
        }

        public List<StatesClaimCountDTO> GetCountClaimsByState()
        {
            return _context.Reclamos
                .Where(c => c.IdUsuarioNavigation.ConsorcioUsuarios.Any(x => x.IdConsorcio == LoginService.CurrentConsortium.Id))
                .GroupBy(c => c.IdEstadoReclamo)
                .Select(g => new StatesClaimCountDTO
                {
                    StateID = g.Key,
                    Count = g.Count()
                })
                .ToList();

        }
    }
}
