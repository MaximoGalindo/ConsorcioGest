﻿using BusinessService.DTO;
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

        [HttpGet("get-reservations/{commonSpaceID}")]
        public IActionResult GetReservations(int commonSpaceID)
        {
            return Ok(reservationsService.GetReservations(commonSpaceID));
        }

        [HttpGet("get-reservations-by-user-id")]
        public IActionResult GetReservationsByUserID()
        {
            return Ok(reservationsService.GetReservationByUser());
        }

        [HttpPost("update-state-reservation")]
        public IActionResult UpdateStateReservation(UpdateStateReservationDTO updateStateReservation)
        {
            return Ok(reservationsService.UpdateStateReservation(updateStateReservation));
        }

    }
}
