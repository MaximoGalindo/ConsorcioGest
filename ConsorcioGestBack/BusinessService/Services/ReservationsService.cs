using BusinessService.DTO;
using BusinessService.Enums;
using BusinessService.Models;
using BusinessService.Services.BaseService;
using BusinessService.Services.Consortium;
using DataAccess.Data;
using DataAccess.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Services
{
    public class ReservationsService : BaseService<Reserva>
    {
        private readonly ConsorcioGestContext _context;
        public ReservationsService(ConsorcioGestContext consorcioGestContext) 
        {
            this._context = consorcioGestContext;
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
                    CommonSpaceName = cs.IdEspacioComunNavigation.Nombre
                })
                .ToList();
            return commonSpaces;
        }



        public async Task<bool> SaveReservation(ReservationDTO reservationDTO)
        {
            Reserva reserva = new Reserva
            {
                IdUsuario = LoginService.CurrentUser.Id,
                HoraDesde = reservationDTO.HourFrom,
                HoraHasta = reservationDTO.HourTo,
                Fecha = DateTime.Now,
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
                .Where(e => e.Id == commonSpaceID && e.IdConsorcio == 6 /*LoginService.CurrentUser.ConsortiumID*/)
                .Select(e => new
                {
                    e.HoraDesde,
                    e.HoraHasta,
                    e.CantidadReservasDisponibles
                })
                .FirstOrDefault();

            List<string> schedulesAvailable = GetHourlyIntervals(commonSpace.HoraDesde, commonSpace.HoraHasta);

            var reservations = _context.Reservas
                .Where(r => r.Fecha == dateTime && r.IdConsorcio == 6)
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
