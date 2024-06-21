using BusinessService.DTO;
using BusinessService.Enums;
using BusinessService.Models;
using BusinessService.Services.BaseService;
using BusinessService.Services.Consortium;
using BusinessService.Templates;
using DataAccess.Data;
using DataAccess.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Services
{
    public class ReservationsService : BaseService<Reserva>
    {
        private readonly ConsorcioGestContext _context;
        private readonly EmailService emailService;
        public ReservationsService(ConsorcioGestContext consorcioGestContext,EmailService emailService) 
        {
            this._context = consorcioGestContext;
            this.emailService = emailService;
        }

        public List<CommonSpaceModel> GetCommonSpaces(int consortiumID)
        {
            List<CommonSpaceModel> commonSpaces = _context.EspacioComunConsorcios
                .Where(cs => cs.IdConsorcio == consortiumID)
                .Select(cs => new CommonSpaceModel
                {
                    Id = cs.Id,
                    UserLimit = cs.LimiteUsuarios,
                    HourFrom = cs.HoraDesde,
                    HourTo = cs.HoraHasta,
                    NumberReservationsAvailable = cs.CantidadReservasDisponibles,
                    CommonSpaceID = cs.IdEspacioComun,
                    CommonSpaceName = cs.IdEspacioComunNavigation.Nombre,
                    CountReservations = cs.Reservas.Count
                })
                .ToList();
            return commonSpaces;
        }



        public async Task<bool> SaveReservation(ReservationDTO reservationDTO)
        {
            DateTime dateTime = DateTime.Parse(reservationDTO.Date);

            Reserva reserva = new Reserva
            {
                IdUsuario = LoginService.CurrentUser.Id,
                HoraDesde = reservationDTO.HourFrom,
                HoraHasta = reservationDTO.HourTo,
                Fecha = dateTime,
                IdEstadoReserva = (int)ReservationsStatesEnum.RESERVATED,
                IdEspacioComunConsorcio = reservationDTO.CommonSpaceConsortiumID,
                IdConsorcio = LoginService.CurrentUser.ConsortiumID                
            };

            DBAdd(reserva, _context);
            return true;
        }



        public List<SchedulesAvailableDTO> GetSchedulesAvailable(string date, int commonSpaceID)
        {
            DateTime dateTime = DateTime.Parse(date);

            var commonSpace = _context.EspacioComunConsorcios
                .Where(e => e.Id == commonSpaceID && e.IdConsorcio == LoginService.CurrentUser.ConsortiumID)
                .Select(e => new
                {
                    e.HoraDesde,
                    e.HoraHasta,
                    e.CantidadReservasDisponibles
                })
                .FirstOrDefault();

            List<string> schedulesAvailable = GetHourlyIntervals(commonSpace.HoraDesde, commonSpace.HoraHasta);

            var reservations = _context.Reservas
                .Where(r => r.Fecha == dateTime && r.IdConsorcio == LoginService.CurrentUser.ConsortiumID && r.IdEstadoReserva == (int)ReservationsStatesEnum.RESERVATED)
                .Select(r => new
                {
                    r.HoraDesde,
                    r.HoraHasta
                })
                .ToList();

            List<string> schedulesReserved = new List<string>();
            foreach (var reservation in reservations)
            {
                schedulesReserved.AddRange(GetHourlyIntervals(reservation.HoraDesde, reservation.HoraHasta));
            }

            List<SchedulesAvailableDTO> schedulesAvailables = new List<SchedulesAvailableDTO>();
            foreach(var schedules in schedulesAvailable)
            {
                SchedulesAvailableDTO time = new SchedulesAvailableDTO();
                if (schedulesReserved.Contains(schedules))
                {
                    time.Available = false;
                    time.Hour = schedules;
                }
                else
                {
                    time.Available = true;
                    time.Hour = schedules;
                }
                schedulesAvailables.Add(time);
            }

            return schedulesAvailables;
        }


        public CommonSpaceModel GetCommonSpaceByID(int commonSpaceID)
        {
            CommonSpaceModel commonSpaces = _context.EspacioComunConsorcios
                .Where(cs => cs.Id == commonSpaceID)
                .Select(cs => new CommonSpaceModel
                {
                    Id = cs.Id,
                    UserLimit = cs.LimiteUsuarios,
                    HourFrom = cs.HoraDesde,
                    HourTo = cs.HoraHasta,
                    NumberReservationsAvailable = cs.CantidadReservasDisponibles,
                    CommonSpaceID = cs.IdEspacioComun,
                    CommonSpaceName = cs.IdEspacioComunNavigation.Nombre
                })
                .First();

            return commonSpaces;
        }

        public List<ReservationModel> GetReservations(FilterReservationDTO filterReservation)
        {
            List<Reserva> reservas = _context.Reservas.Where(r => r.IdConsorcio == LoginService.CurrentConsortium.Id).ToList();
            foreach (var r in reservas)
            {
                if(r.Fecha >= DateTime.Now.Date)
                {
                    r.IdEstadoReserva = (int)ReservationsStatesEnum.FINISHED;
                    DBUpdate(r, _context);
                }
            }

            List<ReservationModel> reservations = _context.Reservas
                .Where(r => r.IdConsorcio == LoginService.CurrentConsortium.Id
                         && r.IdEspacioComunConsorcio == filterReservation.CommonSpaceID        
                         && (filterReservation.Document != 0 ? r.IdUsuarioNavigation.Documento == filterReservation.Document : true)
                         && (filterReservation.DateFrom != null ? r.Fecha >= filterReservation.DateFrom : true)
                         && (filterReservation.DateTo != null ? r.Fecha <= filterReservation.DateTo : true))
                .Select(r => new ReservationModel
                {
                    Id = r.Id,
                    User = new UserModelDTO
                    {
                        Document = r.IdUsuarioNavigation.Documento,
                        Name = r.IdUsuarioNavigation.Nombre + ' ' + r.IdUsuarioNavigation.Apellido,
                        Condominium = r.IdUsuarioNavigation.IdCondominioNavigation.Torre + ' ' + r.IdUsuarioNavigation.IdCondominioNavigation.NumeroDepartamento,
                        Email = r.IdUsuarioNavigation.Email,
                        Phone = r.IdUsuarioNavigation.Telefono,
                    },
                    CommonSpaceConsortiumID = r.IdEspacioComunConsorcio,
                    HourFrom = r.HoraDesde,
                    HourTo = r.HoraHasta,
                    Date = r.Fecha.Date,
                    StateReservation = r.IdEstadoReservaNavigation.Nombre,
                })
                .OrderBy(r => r.Date)
                .ToList();

            return reservations;    
        }

        public List<ReservationUser> GetReservationByUser(FilterReservationUserDTO filterReservation)
        {
            List<Reserva> reservas = _context.Reservas.Where(r => r.IdConsorcio == LoginService.CurrentUser.Id).ToList();
            foreach (var r in reservas)
            {
                if (r.Fecha >= DateTime.Now.Date)
                {
                    r.IdEstadoReserva = (int)ReservationsStatesEnum.FINISHED;
                    DBUpdate(r, _context);
                }
            }

            List<ReservationUser> reservations = _context.Reservas
                .Where(r => r.IdUsuario == LoginService.CurrentUser.Id
                            && (filterReservation.CommonSpaceID != 0 ? r.IdEspacioComunConsorcio == filterReservation.CommonSpaceID : true)
                            && (filterReservation.DateFrom != null ? r.Fecha >= filterReservation.DateFrom : true)
                            && (filterReservation.DateTo != null ? r.Fecha <= filterReservation.DateTo : true))
                .Select(r => new ReservationUser
                {
                    Id = r.Id,
                    CommonSpaceConsortiumID = r.IdEspacioComunConsorcio,
                    HourFrom = r.HoraDesde,
                    HourTo = r.HoraHasta,
                    Date = r.Fecha.Date,
                    StateReservation = r.IdEstadoReservaNavigation.Nombre,
                    StateReservationID = r.IdEstadoReserva,
                    CommonSpaceConsortium = r.IdEspacioComunConsorcioNavigation.IdEspacioComunNavigation.Nombre
                })
                .OrderBy(r => r.StateReservationID)
                .ToList();
            return reservations;
        }
        
        public async Task<bool> UpdateStateReservation(UpdateStateReservationDTO updateStateReservation)
        {
            var reservation = _context.Reservas.Where(r => r.Id == updateStateReservation.ReservationID).FirstOrDefault();
            if (reservation != null)
            {
                if (updateStateReservation.StateReservationID == (int)ReservationsStatesEnum.CANCELLED)
                    await SendCancelEmailAsync(reservation.Id,updateStateReservation.Message);

                reservation.IdEstadoReserva = updateStateReservation.StateReservationID;
                return DBUpdate(reservation, _context);
            }            
            return false;          
        }

        private async Task<bool> SendCancelEmailAsync(int reservationID,string message)
        {
            var reservation = _context.Reservas.Where(r => r.Id == reservationID)
                                               .Select(r => new
                                               {
                                                   r.HoraDesde,
                                                   r.HoraHasta,
                                                   CommonSpaceName = r.IdEspacioComunConsorcioNavigation.IdEspacioComunNavigation.Nombre,
                                                   EmailUser = r.IdUsuarioNavigation.Email,
                                               })
                                               .FirstOrDefault();

            var bodyEmail = EmailTemplateService.GetTemplate("EmailCancelReservation",("commonSpace",reservation.CommonSpaceName), ("hourFrom", reservation.HoraDesde), ("hourTo", reservation.HoraHasta),("message",message));
            
            await emailService.SendEmailAsync(reservation.EmailUser, "Cancelacion de Reserva", bodyEmail);
            return true;
        }

        public bool CancelReservationByUser(int reservationID)
        {
            var reservation = _context.Reservas.Where(r => r.Id == reservationID).FirstOrDefault();
            if (reservation != null)
            {
                reservation.IdEstadoReserva = (int)ReservationsStatesEnum.CANCELLED;
                return DBUpdate(reservation, _context);
            }
            return false;
        }

        private List<string> GetHourlyIntervals(string startHour, string endHour)
        {
            List<string> intervals = new List<string>();
            TimeSpan startTime = TimeSpan.Parse(startHour);
            TimeSpan endTime = TimeSpan.Parse(endHour);

            if (startTime > endTime)
            {
                throw new ArgumentException("La hora de inicio debe ser menor o igual que la hora de fin.");
            }

            for (TimeSpan currentTime = startTime; currentTime <= endTime; currentTime = currentTime.Add(TimeSpan.FromHours(1)))
            {
                intervals.Add(currentTime.ToString(@"hh\:mm"));
            }

            return intervals;
        }




    }
}
