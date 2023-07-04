using Agdata.Bootcamp._2022.SRS.Domain.ViewModel;
using AGDATA.Bootcamp._2022.SRS.Api.Controllers;
using Xunit;
using Moq;
using Agdata.Bootcamp._2022.SRS.Services.Contract;
using System.Net;

namespace AGDATA.Bootcamp._2022.SRS.RG.Controller.tests
{
    public class SeatsControllerTest
    {

        private readonly SeatsController _seatsController;

        private readonly Mock<ISeatManagementService> _seatManagementService = new Mock<ISeatManagementService>();

        public SeatsControllerTest()
        {
            _seatsController = new SeatsController(_seatManagementService.Object);
        }

        [Fact]
        public async Task CreateSeat_ExpectSeatToBeCreated_WhenSeatPasssed()
        {
            //Arrange
            SeatViewModel seat = new SeatViewModel()
            {
                SeatName = "A1"
            };

            _seatManagementService.Setup(seatManagement => seatManagement.AddSeat(seat)).ReturnsAsync(true);

            //Act
            var status = await _seatsController.Post(seat);

            //status.StatusCode = System.Net.HttpStatusCode.Created;
            var expected = HttpStatusCode.Created.ToString();

            //Assert            
            Assert.Equal(expected, status.StatusCode.ToString());

        }
        [Fact]
        public async Task Create_ExpectSeatNotCreated_WhenAlreadyPresentSeatPasssed()
        {
            //Arrange
            SeatViewModel seat = new SeatViewModel()
            {
                SeatName = "A1"
            };
            SeatViewModel seat2 = new SeatViewModel()
            {
                SeatName = "A1"
            };


            //List<SeatViewModel> seatList = new List<SeatViewModel>();
            //seatList.Add(seat);
            _seatManagementService.Setup(seatManagement => seatManagement.AddSeat(seat)).ReturnsAsync(true);
            _seatManagementService.Setup(seatManagement => seatManagement.AddSeat(seat2)).ReturnsAsync(false);


            //Act
            var status = await _seatsController.Post(seat2);

            //status.StatusCode = System.Net.HttpStatusCode.Created;
            var expected = HttpStatusCode.InternalServerError.ToString();

            //Assert            
            Assert.Equal(expected, status.StatusCode.ToString());

        }

        [Fact]
        public async Task Delete_ExpectSeatToBeDeleted_WhenSeatIdPassed()
        {
            //Arrange
            SeatViewModel seat = new SeatViewModel()
            {
                SeatId =1,
                SeatName = "A1"
            };
            SeatViewModel seat2 = new SeatViewModel()
            {
                SeatId = 2,
                SeatName = "A1"
            };
            List<int> seatList = new List<int>()
            {
                1
            };

            _seatManagementService.Setup(seatManagement => seatManagement.AddSeat(seat)).ReturnsAsync(true);
            //_seatManagementService.Verify(mock => mock.CreateSeat(seat), Times.Once());
            _seatManagementService.Setup(seatManagement => seatManagement.AddSeat(seat2)).ReturnsAsync(true);

            _seatManagementService.Setup(seatManagement => seatManagement.DeleteSeat(seatList)).ReturnsAsync(true);

            //Act
            var status = await _seatsController.Delete(seatList);

            //Assert
            var expected = HttpStatusCode.OK.ToString();
            Assert.Equal(expected, status.StatusCode.ToString());

        }

        [Fact]
        public async Task Delete_ExpectReturnsBadRequest_WhenSeatIdPassedWhichIsNotPresent()
        {
            //Arrange
            SeatViewModel seat = new SeatViewModel()
            {
                SeatId = 1,
                SeatName = "A1"
            };
            SeatViewModel seat2 = new SeatViewModel()
            {
                SeatId = 2,
                SeatName = "A1"
            };
            List<int> seatList = new List<int>()
            {
                3
            };

            _seatManagementService.Setup(seatManagement => seatManagement.AddSeat(seat)).ReturnsAsync(true);
            _seatManagementService.Setup(seatManagement => seatManagement.AddSeat(seat2)).ReturnsAsync(true);

            _seatManagementService.Setup(seatManagement => seatManagement.DeleteSeat(seatList)).ReturnsAsync(false);

            //Act
            var status = await _seatsController.Delete(seatList);

            //Assert
            var expected = HttpStatusCode.NotFound.ToString();
            Assert.Equal(expected, status.StatusCode.ToString());

        }



    }

}
