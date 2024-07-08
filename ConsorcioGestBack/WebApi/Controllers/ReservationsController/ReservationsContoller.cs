using BusinessService.DTO;
using BusinessService.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.ReservationsController
{
    [Route("reservations")]
    [ApiController]
    public class ReservationsContoller : ControllerBase
    {
        private readonly ReservationsService reservationsService;

        public ReservationsContoller(ReservationsService reservationsService)
        {
            this.reservationsService = reservationsService;
        }

        [HttpGet("get-common-spaces-by-user")]
        public IActionResult GetCommonSpacesByUser()
        {
            return Ok(reservationsService.GetCommonSpaces(LoginService.CurrentUser.ConsortiumID));
        }

        [HttpGet("get-common-spaces")]
        public IActionResult GetCommonSpaces()
        {
            return Ok(reservationsService.GetCommonSpaces(LoginService.CurrentConsortium.Id));
        }


        [HttpGet("get-common-space/{id}")]
        public IActionResult GetCommonSpace(int id)
        {
            return Ok(reservationsService.GetCommonSpaceByID(id));
        }

        [HttpPost("save-reservation")]
        public IActionResult SaveReservation(ReservationDTO reservationDTO)
        {
            return Ok(reservationsService.SaveReservation(reservationDTO));
        }

        [HttpGet("get-schedules-available/{commonSpaceID}")]
        public IActionResult GetSchedulesAvailable(string date, int commonSpaceID)
        {
            return Ok(reservationsService.GetSchedulesAvailable(date, commonSpaceID));
        }

        [HttpGet("get-reservations")]
        public IActionResult GetReservations([FromQuery] FilterReservationDTO filterReservation)
        {
            return Ok(reservationsService.GetReservations(filterReservation));
        }

        [HttpGet("get-reservations-by-user-id")]
        public IActionResult GetReservationsByUserID([FromQuery] FilterReservationUserDTO filterReservation)
        {
            return Ok(reservationsService.GetReservationByUser(filterReservation));
        }

        [HttpPost("update-state-reservation")]
        public async Task<IActionResult> UpdateStateReservation(UpdateStateReservationDTO updateStateReservation)
        {
            return Ok(await reservationsService.UpdateStateReservation(updateStateReservation));
        }

        [HttpPost("cancel-reservation-by-user")]
        public IActionResult CancelReservationByUser(int reservationID)
        {
            return Ok(reservationsService.CancelReservationByUser(reservationID));
        }

        [HttpPost("update-state-common-space")]
        public IActionResult UpdateStateCommonSpace(UpdateStateCommonSpaceDTO updateStateCommonSpaceDTO)
        {
            return Ok(reservationsService.UpdateStateCommonSpace(updateStateCommonSpaceDTO.CommonSpaceID,updateStateCommonSpaceDTO.State));
        }
    }
}
