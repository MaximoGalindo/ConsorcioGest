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
using Microsoft.EntityFrameworkCore;

namespace BusinessService.Services
{
    public class ClaimsService : BaseService<Reclamo>
    {
        public readonly ConsorcioGestContext _context;
        public ClaimsService(ConsorcioGestContext consorcioGestContext)
        {
            this._context = consorcioGestContext;
        }

        public async Task<bool> SaveClaimGestion(SaveClaimGestionDTO saveClaimGestionDTO)
        {
            try
            {
                Reclamo reclamo =    _context.Reclamos
                    .Where(r => r.Id == saveClaimGestionDTO.IdClaim)
                    .FirstOrDefault();

                reclamo.IdEstadoReclamo = saveClaimGestionDTO.StateSelectedID;
                DBUpdate(reclamo, _context);

                ReclamoDetalle reclamoDetalle = new ReclamoDetalle();
                reclamoDetalle.Observacion = saveClaimGestionDTO.Observation;
                reclamoDetalle.Fecha = DateTime.Now.Date;
                reclamoDetalle.IdReclamo = reclamo.Id;
                reclamoDetalle.Estado = ((ClaimsStatesEnum)Enum.ToObject(typeof(ClaimsStatesEnum), saveClaimGestionDTO.StateSelectedID)).ToString();
                DBAdd(reclamoDetalle, _context);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> SaveClaimByUser(SaveClaimDTO claim) 
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

        public List<ListItemDTO> GetStatesClaim()
        {
            return _context.EstadoReclamos
                .Select(c => new ListItemDTO
                {
                    ID = c.Id,
                    Name = c.Nombre
                }).ToList();
        }

        public List<HistoryClaimDTO> GetHistoryClaim(int claimID)
        {
            return _context.ReclamoDetalles
                    .Where(c => c.IdReclamo == claimID)
                    .Select(c => new HistoryClaimDTO
                    {
                        IdClaim = claimID,
                        Observation = c.Observacion,
                        Date = c.Fecha.ToString(),
                        State = c.Estado
                    }).ToList();
        }



        public List<ClaimDTO> GetAllClaimsByState(int idState)
        {
            List<ClaimDTO> claimDTOs = _context.Reclamos
                        .Where(c => c.IdUsuarioNavigation.ConsorcioUsuarios.Any(x => x.IdConsorcio == LoginService.CurrentConsortium.Id)
                            && (idState != 0 ? c.IdEstadoReclamo == idState : true))
                        .Select(c => new ClaimDTO
                        {
                            Id = c.Id,
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

        public List<ClaimDTO> GetClaimsByUserID(int userID)
        {
            List<ClaimDTO> claimDTOs = _context.Reclamos
                        .Where(c => c.IdUsuario == userID)
                        .Select(c => new ClaimDTO
                        {
                            Id = c.Id,
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
            var allStates = _context.EstadoReclamos
                .Select(e => new { e.Id, e.Nombre })
                .ToList();

            var claimCounts = _context.Reclamos
                .Where(c => c.IdUsuarioNavigation.ConsorcioUsuarios.Any(x => x.IdConsorcio == LoginService.CurrentConsortium.Id))
                .GroupBy(c => c.IdEstadoReclamo)
                .Select(g => new
                {
                    StateID = g.Key,
                    Count = g.Count()
                })
                .ToList();

            var result = allStates
                .GroupJoin(
                    claimCounts,
                    state => state.Id,
                    claimCount => claimCount.StateID,
                    (state, claimCount) => new StatesClaimCountDTO
                    {
                        StateID = state.Id,
                        Count = claimCount.FirstOrDefault()?.Count ?? 0
                    })
                .ToList();

            return result;
        }


        public IEnumerable<ImagesDTO> GetImagesByReclamoId(int reclamoId)
        {
            return _context.Imagenes
                   .Where(img => img.IdReclamo == reclamoId)
                   .Select(img => new ImagesDTO {
                       Id = img.Id,
                       Nombre = img.Nombre,
                       DatosImagen = Convert.ToBase64String(img.DatosImagen),
                       IdReclamo = img.IdReclamo
                   })
                   .ToList();
        }
    }
}
