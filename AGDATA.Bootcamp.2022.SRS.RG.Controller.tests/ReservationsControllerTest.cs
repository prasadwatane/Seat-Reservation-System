using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Agdata.Bootcamp._2022.SRS.Services.Contract;
using AGDATA.Bootcamp._2022.SRS.Api.Controllers;
using Agdata.Bootcamp._2022.SRS.Domain.ViewModel;
using System.Net;

namespace AGDATA.Bootcamp._2022.SRS.RG.Controller.tests
{
    public class ReservationsControllerTest
    {
        private readonly ReservationsController _reservationController;

        private readonly Mock<IReservationManagementService> _reservationManagementMock = new Mock<IReservationManagementService>();

        public ReservationsControllerTest()
        {
            _reservationController = new ReservationsController(_reservationManagementMock.Object);
        }

        [Fact]
        public async Task CreateReservation_ExpectReservationToBeCreated_WhenReservationOnEmptySeat()
        {
            //Arrange
            ReservationViewModel reservationVm = new ReservationViewModel()
            {
                EmployeeId = 1,
                EmployeeName = "Rahul",
                SeatId = 1,
                SeatName = "A1",
                BookingDate = new DateTime(2022 - 09 - 11)
            };

            _reservationManagementMock.Setup(reservation => reservation.ReserveSeat(reservationVm)).ReturnsAsync(true);

            //Act
           var status = await _reservationController.CreateReservation(reservationVm);


            //Assert
            var expected = HttpStatusCode.Created.ToString();
            Assert.Equal(expected, status.StatusCode.ToString());
        }

        [Fact]
        public async Task CreateReservation_ExpectBadRequest_WhenSameReservationPassed()
        {
            //Arrange
            ReservationViewModel reservationVm = new ReservationViewModel()
            {
                EmployeeId = 1,
                EmployeeName = "Rahul",
                SeatId = 1,
                SeatName = "A1",
                BookingDate = new DateTime(2022 - 09 - 11)
            };
            ReservationViewModel reservationVm2 = new ReservationViewModel()
            {
                EmployeeId = 1,
                EmployeeName = "Rahul",
                SeatId = 1,
                SeatName = "A1",
                BookingDate = new DateTime(2022 - 09 - 11)
            };

            _reservationManagementMock.Setup(reservation => reservation.ReserveSeat(reservationVm)).ReturnsAsync(true);
            _reservationManagementMock.Setup(reservation => reservation.ReserveSeat(reservationVm)).ReturnsAsync(false);

            //Act
            var status = await _reservationController.CreateReservation(reservationVm2);


            //Assert
            var expected = HttpStatusCode.InternalServerError.ToString();
            Assert.Equal(expected, status.StatusCode.ToString());
        }

        [Fact]
        public async Task DeleteReservationById_ExpectReservationToBeDeleted_WhenReservationIdPassed()
        {
            //Arrange
            var reservationId = 1;
            ReservationViewModel reservationVm = new ReservationViewModel()
            {
                ReservationId = reservationId,
                EmployeeId = 1,
                EmployeeName = "Rahul",
                SeatId = 1,
                SeatName = "A1",
                BookingDate = new DateTime(2022 - 09 - 11)
            };

            _reservationManagementMock.Setup(reservation => reservation.ReserveSeat(reservationVm)).ReturnsAsync(true);
            _reservationManagementMock.Setup(reservation => reservation.CancelSeat(reservationId)).ReturnsAsync(true);

            //Act
            var status = await _reservationController.DeleteReservation(reservationId);

            //Assert
            var expected = HttpStatusCode.OK.ToString();
            Assert.Equal(expected, status.StatusCode.ToString());
        }

        [Fact]
        public async Task DeleteReservationById_ExpectBadRequest_WhenReservationIdPassedWhichIsNotPresent()
        {
            //Arrange
            var reservationId = 1;
            ReservationViewModel reservationVm = new ReservationViewModel()
            {
                ReservationId = reservationId,
                EmployeeId = 1,
                EmployeeName = "Rahul",
                SeatId = 1,
                SeatName = "A1",
                BookingDate = new DateTime(2022 - 09 - 11)
            };

            _reservationManagementMock.Setup(reservation => reservation.ReserveSeat(reservationVm)).ReturnsAsync(true);
            _reservationManagementMock.Setup(reservation => reservation.CancelSeat(5)).ReturnsAsync(false);

            //Act
            var status = await _reservationController.DeleteReservation(reservationId);

            //Assert
            var expected = HttpStatusCode.InternalServerError.ToString();
            Assert.Equal(expected, status.StatusCode.ToString());
        }




    }
}
