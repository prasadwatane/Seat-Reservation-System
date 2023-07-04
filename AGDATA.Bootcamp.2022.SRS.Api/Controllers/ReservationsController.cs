using Agdata.Bootcamp._2022.SRS.Domain.ViewModel;
using Agdata.Bootcamp._2022.SRS.Services.Contract;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static SRS.Messagebus.Commands.CreateReservationCommand;

namespace AGDATA.Bootcamp._2022.SRS.Api.Controllers
{
    [Route("api/Reservations")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationManagementService _reservationManagementService;
        private readonly IMediator _mediator;

        public ReservationsController(IMediator mediator, IReservationManagementService reservationManagementService)
        {
            _reservationManagementService = reservationManagementService;
            _mediator = mediator;
        }

        [HttpPost("reservation")]
        public async Task<HttpResponseMessage> CreateReservation([FromBody] ReservationViewModel reservationViewModel)
        {
            await _mediator.Send(new AddReservationCommand(reservationViewModel));
            //var status = await _reservationManagementService.ReserveSeat(reservationViewModel);
            //if (status == true)
            //{
            return new HttpResponseMessage(HttpStatusCode.Created);
            //}
            //else
            //{
            //    return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            //}
        }

        [HttpDelete("reservationId")]
        public async Task<HttpResponseMessage> DeleteReservation(int reservationId)
        {
            try
            {
                var result = await _reservationManagementService.CancelSeat(reservationId);
                if (result == true)
                    return new HttpResponseMessage(HttpStatusCode.OK);
                else
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError);

            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }


        [HttpGet("Date")]
        public async Task<ActionResult> GetReservationByDate(DateTime date)
        {
            var reservationListByDate = await _reservationManagementService.ListOfReservationVM(date);

            if (reservationListByDate != null)
                return Ok(reservationListByDate);
            else
                return BadRequest();
        }


        [HttpGet("GetSeatsByDate")]
        public async Task<ActionResult> GetUnreservedSeatsByDate(DateTime date)
        {
            var seats = await _reservationManagementService.UnreserveSeatList(date);
            if (seats != null)
            {
                return new OkObjectResult(seats);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
