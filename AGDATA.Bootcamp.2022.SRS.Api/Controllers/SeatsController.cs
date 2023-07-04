using Agdata.Bootcamp._2022.SRS.Domain;
using Agdata.Bootcamp._2022.SRS.Domain.ViewModel;
using Agdata.Bootcamp._2022.SRS.Services.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AGDATA.Bootcamp._2022.SRS.Api.Controllers
{
    [Route("api/Seats")]
    [ApiController]
    public class SeatsController : ControllerBase
    {
        private readonly ISeatManagementService _seatManagementService;
        private readonly ISeatRepository _seatRepository;

        public SeatsController(ISeatManagementService seatManagementService, ISeatRepository seatRepository)
        {
            _seatManagementService = seatManagementService;
            _seatRepository = seatRepository;
        }

        [HttpPost("seat")]
        public async Task<HttpResponseMessage> Post([FromBody] SeatViewModel seatViewModel)
        {
            try
            {
                var status = await _seatManagementService.AddSeat(seatViewModel);
                if (status == true)
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Created);
                    return response;
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        [HttpPost("addseats")]
        public async Task<HttpResponseMessage> AddRange([FromBody] List<string> seatViewModels)
        {
            try
            {
                var status = await _seatManagementService.AddSeats(seatViewModels);
                if (status == 1)
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Created);
                    return response;
                }
                else if (status == 2){
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    return response;

                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        [HttpDelete("ids")]
        public async Task<HttpResponseMessage> Delete([FromBody] List<int> seatIds)
        {
            try
            {
                
                var status = await _seatManagementService.DeleteSeat(seatIds);
                
                if (status == true)
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    return response;
                }
                else
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                    return response;
                }
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }


        [HttpGet("seats")]
        public async Task<ActionResult> GetAll()
        {

            var seatVMList = await _seatManagementService.GetAllSeats();
            if (seatVMList.Count > 0)
            {
                return new OkObjectResult(seatVMList);
            }
            else
            {
                return new NotFoundObjectResult("Seats Not Found");
            }
        }


        [HttpGet("get")]
        public async Task<ActionResult> GetUnreservedSeats()
        {
            var seatVMList = await _seatRepository.GetUnreserved();
            if (seatVMList.Count > 0)
            {
                return new OkObjectResult(seatVMList);
            }
            else
            {
                return new NotFoundObjectResult("Seats Not Found");
            }
        }
    }
}
