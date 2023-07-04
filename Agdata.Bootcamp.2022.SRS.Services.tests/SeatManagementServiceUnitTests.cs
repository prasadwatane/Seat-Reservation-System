using Agdata.Bootcamp._2022.SRS.Services.Implementations;
using Agdata.Bootcamp._2022.SRS.Services.Contract;
using Agdata.Bootcamp._2022.SRS.Domain.Model;
using Agdata.Bootcamp._2022.SRS.Domain.ViewModel;
using Xunit;
using Agdata.Bootcamp._2022.SRS.Domain;
using Moq;

namespace Agdata.Bootcamp._2022.SRS.Services.tests
{
    public class SeatManagementServiceUnitTests
    {

        private readonly SeatManagementService _seatManagementService;
        private readonly Mock<ISeatRepository> _seatRepositoryMock = new Mock<ISeatRepository>();

        public SeatManagementServiceUnitTests()
        {
            _seatManagementService = new SeatManagementService(_seatRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateSeat_ExpectSeatToBeCreated_WhenSeatIsAdded()
        {
            //Arrange
            SeatViewModel seat = new SeatViewModel()
            {
                SeatName = "A1"
            };

            List<SeatViewModel> seatList = new List<SeatViewModel>();

            _seatRepositoryMock.Setup(seat1 => seat1.GetAll()).ReturnsAsync(seatList);
            _seatRepositoryMock.Setup(seat1 => seat1.Add(seat)).ReturnsAsync(1);


            //Act
            var seatStatus = await _seatManagementService.AddSeat(seat);


            //Assert
            Assert.True(seatStatus);
        }

        [Fact]
        public async Task CreateSeat_ExpectSeatNotAdded_WhenSeatIsAlreadyAdded()
        {
            //Arrange
            SeatViewModel seat = new SeatViewModel()
            {
                SeatName = "A1"
            };

            List<SeatViewModel> seatList = new List<SeatViewModel>();
            seatList.Add(seat);

            _seatRepositoryMock.Setup(seat1 => seat1.GetAll()).ReturnsAsync(seatList);
            _seatRepositoryMock.Setup(seat1 => seat1.Add(seat));


            //Act
            var seatStatus = await _seatManagementService.AddSeat(seat);


            //Assert
            Assert.False(seatStatus);
        }

        [Fact]
        public async Task DeleteSeat_ExpectSeatToBeDeleted_WhenSeatIsAvailable()
        {
            //Arrange
            SeatViewModel seat = new SeatViewModel()
            {
                SeatId = 1,
                SeatName = "A1"
            };

            List<SeatViewModel> seatList = new List<SeatViewModel>();
            seatList.Add(seat);
            List<int> list = new List<int>()
            {
                1
            };

            _seatRepositoryMock.Setup(seat1 =>seat1.GetBySeatId(seat.SeatId)).ReturnsAsync(seat);
            _seatRepositoryMock.Setup(seat1 => seat1.Delete(seat));


            //Act
            var seatStatus = await _seatManagementService.DeleteSeat(list);

            //Assert
            Assert.True(seatStatus);
        }

        [Fact]
        public async Task DeleteSeat_ExpectSeatNotDelete_WhenSeatIsNotAvailable()
        {
            //Arrange
            SeatViewModel seat = new SeatViewModel()
            {
                SeatName = "A1"
            };

            List<SeatViewModel> seatList = new List<SeatViewModel>();
            seatList.Add(seat);

            List<int> list = new List<int>()
            {
                5
            };

            _seatRepositoryMock.Setup(seat1 => seat1.GetBySeatId(seat.SeatId)).ReturnsAsync(seat);
            _seatRepositoryMock.Setup(seat1 => seat1.Delete(seat));


            //Act
            var seatStatus = await _seatManagementService.DeleteSeat(list);

            //Assert
            Assert.False(seatStatus);
        }

        [Fact]
        public async Task GetAllSeats_ExpectAllSeats()
        {
            //Arrange
            SeatViewModel seat1 = new SeatViewModel()
            {
                SeatName = "A1"
            };
            SeatViewModel seat2 = new SeatViewModel()
            {
                SeatName = "A2"
            };

            List<SeatViewModel> seatList = new List<SeatViewModel>();
            seatList.Add(seat1);
            seatList.Add(seat1);

            _seatRepositoryMock.Setup(seat => seat.GetAll()).ReturnsAsync(seatList);


            //Act
            var seatVMList =await _seatManagementService.GetAllSeats();

            //Assert
            Assert.Equal(seatList.Count, seatVMList.Count);
            for(int i = 0; i < seatVMList.Count; i++)
            {
                Assert.Equal(seatList[1].SeatName, seatVMList[i].SeatName);
            }
            
        }



    }
}