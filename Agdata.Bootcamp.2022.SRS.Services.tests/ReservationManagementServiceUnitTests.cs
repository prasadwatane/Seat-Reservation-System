using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agdata.Bootcamp._2022.SRS.Services.Implementations;
using Agdata.Bootcamp._2022.SRS.Services.Contract;
using Agdata.Bootcamp._2022.SRS.Domain.Model;
using Xunit;
using Agdata.Bootcamp._2022.SRS.Domain.ViewModel;
using Moq;
using Agdata.Bootcamp._2022.SRS.Domain;

namespace Agdata.Bootcamp._2022.SRS.Services.tests
{
    public class ReservationManagementServiceUnitTests
    {

        private readonly ReservationManagementService _reservationManagementService;

        private readonly Mock<IReservationRepository> _reservationRepositoryMock = new Mock<IReservationRepository>();
        private readonly Mock<ISeatRepository> _seatRepositoryMock = new Mock<ISeatRepository>();
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock = new Mock<IEmployeeRepository>();

        public ReservationManagementServiceUnitTests()
        {
            _reservationManagementService = new ReservationManagementService(_reservationRepositoryMock.Object, _seatRepositoryMock.Object,_employeeRepositoryMock.Object);
        }


        [Fact]
        public async Task ReserveSeat_ExpectSeatToBeReserved_WhenSeatIsAvailable()
        {
            //Arrange
            var seatName = "A1";
            var employeeName = "Rahul";
            SeatViewModel seat = new SeatViewModel()
            {
                SeatId = 1,
                SeatName = seatName
            };
            EmployeeViewModel employee = new EmployeeViewModel()
            {
                EmployeeId = 1,
                EmployeeName = employeeName
            };

            ReservationViewModel reservationVm = new ReservationViewModel()
            {
                EmployeeId = 1,
                EmployeeName = employeeName,
                SeatId = 1,
                SeatName = seatName,
                BookingDate = new DateTime(2022 - 09 - 11)

            };


            List<SeatViewModel> seatList = new List<SeatViewModel>();
            List<ReservationViewModel> reservationList = new List<ReservationViewModel>();
            List<EmployeeViewModel> employeeList = new List<EmployeeViewModel>();
            seatList.Add(seat);
            employeeList.Add(employee);

            _seatRepositoryMock.Setup(seatRepository => seatRepository.GetAll()).ReturnsAsync(seatList);
            _seatRepositoryMock.Setup(seat => seat.GetBySeatName(seatName)).ReturnsAsync(seat);
            _reservationRepositoryMock.Setup(reservation => reservation.GetAll()).ReturnsAsync(reservationList);
            _employeeRepositoryMock.Setup(employeeRepository => employeeRepository.GetByEmployeeId(employee.EmployeeId)).ReturnsAsync(employee);
            _employeeRepositoryMock.Setup(employee1 => employee1.GetByEmployeeName(employeeName)).ReturnsAsync(employee);
            _employeeRepositoryMock.Setup(employee1 => employee1.Add(employee)).ReturnsAsync(1);
            _reservationRepositoryMock.Setup(reservation => reservation.Add(reservationVm)).ReturnsAsync(1);


            //Act
            var status = await _reservationManagementService.ReserveSeat(reservationVm);

            //Assert
            Assert.True(status);

        }

        [Fact]
        public async Task ReserveSeat_ExpectSeatNotReserved_WhenSeatIsNotAvaialable()
        {
            //Arrange
            var seatName = "A1";
            var employeeName = "Rahul";
            SeatViewModel seat = new SeatViewModel()
            {
                SeatId = 1,
                SeatName = seatName
            };
            EmployeeViewModel employee = new EmployeeViewModel()
            {
                EmployeeId = 1,
                EmployeeName = employeeName
            };

            ReservationViewModel reservationVm = new ReservationViewModel()
            {
                EmployeeId = 1,
                EmployeeName = employeeName,
                SeatId = 1,
                SeatName = seatName,
                BookingDate = new DateTime(2022 - 09 - 11)

            };
            List<SeatViewModel> seatList = new List<SeatViewModel>();
            List<ReservationViewModel> reservationList = new List<ReservationViewModel>();
            List<EmployeeViewModel> employeeList = new List<EmployeeViewModel>();
            employeeList.Add(employee);

            _seatRepositoryMock.Setup(seatRepository => seatRepository.GetAll()).ReturnsAsync(seatList);
            _seatRepositoryMock.Setup(seat => seat.GetBySeatName(seatName)).ReturnsAsync(() => null);
            _reservationRepositoryMock.Setup(reservation => reservation.GetAll()).ReturnsAsync(reservationList);
            _employeeRepositoryMock.Setup(employeeRepository => employeeRepository.GetByEmployeeId(employee.EmployeeId)).ReturnsAsync(employee);
            _employeeRepositoryMock.Setup(employee1 => employee1.GetByEmployeeName(employeeName)).ReturnsAsync(employee);
            _employeeRepositoryMock.Setup(employee1 => employee1.Add(employee)).ReturnsAsync(1);
            _reservationRepositoryMock.Setup(reservation => reservation.Add(reservationVm)).ReturnsAsync(1);


            //Act
            var status = await _reservationManagementService.ReserveSeat(reservationVm);

            //Assert
            Assert.False(status);

        }

         [Fact]
        public async Task ReserveSeat_ExpectSeatNotReserved_WhenReservationAlreadyExists()
        {
            //Arrange
            var seatName = "A1";
            var employeeName = "Rahul";
            SeatViewModel seat = new SeatViewModel()
            {
                SeatId = 1,
                SeatName = seatName
            };
            EmployeeViewModel employee = new EmployeeViewModel()
            {
                EmployeeId = 1,
                EmployeeName = employeeName
            };

            ReservationViewModel reservationVm = new ReservationViewModel()
            {
                EmployeeId = 1,
                EmployeeName = employeeName,
                SeatId = 1,
                SeatName = seatName,
                BookingDate = new DateTime(2022 - 09 - 11)

            };
            List<SeatViewModel> seatList = new List<SeatViewModel>();
            List<ReservationViewModel> reservationList = new List<ReservationViewModel>();
            reservationList.Add(reservationVm);
            List<EmployeeViewModel> employeeList = new List<EmployeeViewModel>();
            employeeList.Add(employee);

            _seatRepositoryMock.Setup(seatRepository => seatRepository.GetAll()).ReturnsAsync(seatList);
            _seatRepositoryMock.Setup(seat => seat.GetBySeatName(seatName)).ReturnsAsync(seat);
            _reservationRepositoryMock.Setup(reservation => reservation.GetAll()).ReturnsAsync(reservationList);
            _employeeRepositoryMock.Setup(employeeRepository => employeeRepository.GetByEmployeeId(employee.EmployeeId)).ReturnsAsync(employee);
            _employeeRepositoryMock.Setup(employee1 => employee1.GetByEmployeeName(employeeName)).ReturnsAsync(employee);
            _employeeRepositoryMock.Setup(employee1 => employee1.Add(employee)).ReturnsAsync(1);
            _reservationRepositoryMock.Setup(reservation => reservation.Add(reservationVm)).ReturnsAsync(1);


            //Act
            var status = await _reservationManagementService.ReserveSeat(reservationVm);

            //Assert
            Assert.False(status);

        }

        [Fact]
        public async Task ReserveSeat_ExpectSeatNotReserved_WhenSeatIsAlreadyReserved()
        {
            //Arrange
            var seatName = "A1";
            var employeeName = "Rahul";
            SeatViewModel seat = new SeatViewModel()
            {
                SeatId = 1,
                SeatName = seatName
            };
            EmployeeViewModel employee = new EmployeeViewModel()
            {
                EmployeeId = 1,
                EmployeeName = employeeName
            };

            ReservationViewModel reservationVm = new ReservationViewModel()
            {
                EmployeeId = 1,
                EmployeeName = employeeName,
                SeatId = 1,
                SeatName = seatName,
                BookingDate = new DateTime(2022 - 09 - 11)

            };
            ReservationViewModel reservationVm2 = new ReservationViewModel()
            {
                EmployeeId = 2,
                EmployeeName = "Rohan",
                SeatId = 1,
                SeatName = seatName,
                BookingDate = new DateTime(2022 - 09 - 11)

            };
            List<SeatViewModel> seatList = new List<SeatViewModel>();
            List<ReservationViewModel> reservationList = new List<ReservationViewModel>();
            reservationList.Add(reservationVm);
            List<EmployeeViewModel> employeeList = new List<EmployeeViewModel>();
            employeeList.Add(employee);

            _seatRepositoryMock.Setup(seatRepository => seatRepository.GetAll()).ReturnsAsync(seatList);
            _seatRepositoryMock.Setup(seat => seat.GetBySeatName(seatName)).ReturnsAsync(seat);
            _reservationRepositoryMock.Setup(reservation => reservation.GetAll()).ReturnsAsync(reservationList);
            _employeeRepositoryMock.Setup(employeeRepository => employeeRepository.GetByEmployeeId(employee.EmployeeId)).ReturnsAsync(employee);
            _employeeRepositoryMock.Setup(employee1 => employee1.GetByEmployeeName(employeeName)).ReturnsAsync(employee);
            _employeeRepositoryMock.Setup(employee1 => employee1.Add(employee)).ReturnsAsync(1);
            _reservationRepositoryMock.Setup(reservation => reservation.Add(reservationVm2)).ReturnsAsync(1);

            //Act
            var status = await _reservationManagementService.ReserveSeat(reservationVm);

            //Assert
            Assert.False(status);


        }

        [Fact]
        public async Task ReserveSeat_ExpectSeatToBeReserved_WhenDateIsDifferent()
        {
            //Arrange
            var seatName = "A1";
            var employeeName = "Rahul";
            SeatViewModel seat = new SeatViewModel()
            {
                SeatId = 1,
                SeatName = seatName
            };
            EmployeeViewModel employee = new EmployeeViewModel()
            {
                EmployeeId = 1,
                EmployeeName = employeeName
            };

            ReservationViewModel reservationVm = new ReservationViewModel()
            {
                EmployeeId = 1,
                EmployeeName = employeeName,
                SeatId = 1,
                SeatName = seatName,
                BookingDate = new DateTime(2022 - 09 - 11)

            };
            ReservationViewModel reservationVm2 = new ReservationViewModel()
            {
                EmployeeId = 1,
                EmployeeName = employeeName,
                SeatId = 1,
                SeatName = seatName,
                BookingDate = new DateTime(2022 - 09 - 12)

            };


            List<SeatViewModel> seatList = new List<SeatViewModel>();
            List<ReservationViewModel> reservationList = new List<ReservationViewModel>();
            reservationList.Add(reservationVm);
            List<EmployeeViewModel> employeeList = new List<EmployeeViewModel>();
            seatList.Add(seat);
            employeeList.Add(employee);

            _seatRepositoryMock.Setup(seatRepository => seatRepository.GetAll()).ReturnsAsync(seatList);
            _seatRepositoryMock.Setup(seat => seat.GetBySeatName(seatName)).ReturnsAsync(seat);
            _reservationRepositoryMock.Setup(reservation => reservation.GetAll()).ReturnsAsync(reservationList);
            _employeeRepositoryMock.Setup(employeeRepository => employeeRepository.GetByEmployeeId(employee.EmployeeId)).ReturnsAsync(employee);
            _employeeRepositoryMock.Setup(employee1 => employee1.GetByEmployeeName(employeeName)).ReturnsAsync(employee);
            _employeeRepositoryMock.Setup(employee1 => employee1.Add(employee)).ReturnsAsync(1);
            _reservationRepositoryMock.Setup(reservation => reservation.Add(reservationVm)).ReturnsAsync(1);


            //Act
            var status = await _reservationManagementService.ReserveSeat(reservationVm2);

            //Assert
            Assert.True(status);

        }

        [Fact]
        public async Task UnreserveSeat_ExpectSeatToBeUnreserved_WhenSeatIsAlreadyReserved()
        {
            //Arrange
            ReservationViewModel reservation1 = new ReservationViewModel()
            {
                ReservationId = 1,
                EmployeeId = 1,
                EmployeeName = "Rahul",
                SeatId = 1,
                SeatName = "A1",
                BookingDate = new DateTime(2022 - 09 - 11)

            };
            ReservationViewModel reservation2 = new ReservationViewModel()
            {
                ReservationId = 2,
                EmployeeId = 2,
                EmployeeName = "Rohan",
                SeatId = 2,
                SeatName = "A2",
                BookingDate = new DateTime(2022 - 09 - 11)

            };

            List<ReservationViewModel> reservationList = new List<ReservationViewModel>();
            reservationList.Add(reservation1);
            reservationList.Add(reservation2);

            _reservationRepositoryMock.Setup(reservation => reservation.GetById(1)).ReturnsAsync(reservation1);
            _reservationRepositoryMock.Setup(reservation => reservation.Delete(1)).ReturnsAsync(1);

            //Act
            var status =await _reservationManagementService.CancelSeat(1);

            //Assert
            Assert.True(status);
        }

        [Fact]
        public async Task UnreserveSeat_ExpectSeatNotUnreserved_WhenReservationIsNotExists()
        {
            //Arrange
            ReservationViewModel reservation1 = new ReservationViewModel()
            {
                ReservationId = 1,
                EmployeeId = 1,
                EmployeeName = "Rahul",
                SeatId = 1,
                SeatName = "A1",
                BookingDate = new DateTime(2022 - 09 - 11)

            };
            ReservationViewModel reservation2 = new ReservationViewModel()
            {
                ReservationId = 2,
                EmployeeId = 2,
                EmployeeName = "Rohan",
                SeatId = 2,
                SeatName = "A2",
                BookingDate = new DateTime(2022 - 09 - 11)

            };

            List<ReservationViewModel> reservationList = new List<ReservationViewModel>();
            reservationList.Add(reservation1);
            reservationList.Add(reservation2);

            _reservationRepositoryMock.Setup(reservation => reservation.GetById(3)).ReturnsAsync(() => null);
            _reservationRepositoryMock.Setup(reservation => reservation.Delete(3)).ReturnsAsync(1);

            //Act
            var status = await _reservationManagementService.CancelSeat(3);

            //Assert
            Assert.False(status);

        }


        [Fact]
        public async Task ListOfReservationVm_ExceptListOfReservationsOnGivenDate_WhenAppropiateDatePassed()
        {
            //Arrange
            DateTime date = new DateTime(2022 - 09 - 12);
            ReservationViewModel reserve1 = new ReservationViewModel()
            {
                EmployeeId = 1,
                EmployeeName = "Rahul",
                SeatId = 1,
                SeatName = "A1",
                BookingDate = date
            };
            ReservationViewModel reserve2 = new ReservationViewModel()
            {
                EmployeeId = 2,
                EmployeeName = "Rohan",
                SeatId = 2,
                SeatName = "A2",
                BookingDate = date

            };
            EmployeeViewModel employee1 = new EmployeeViewModel()
            {
                EmployeeId = 1,
                EmployeeName = "Rahul"
            };
            EmployeeViewModel employee2 = new EmployeeViewModel()
            {
                EmployeeId = 2,
                EmployeeName = "Rohan"
            };
            SeatViewModel seat1 = new SeatViewModel()
            {
                SeatId = 1,
                SeatName = "A1"
            };
            SeatViewModel seat2 = new SeatViewModel()
            {
                SeatId = 2,
                SeatName = "A2"
            };
            List<ReservationViewModel> reservationList = new List<ReservationViewModel>();
            reservationList.Add(reserve1);
            reservationList.Add(reserve2);

            _reservationRepositoryMock.Setup(reservation => reservation.GetAll()).ReturnsAsync(reservationList);
            _employeeRepositoryMock.Setup(employee => employee.GetByEmployeeId(employee1.EmployeeId)).ReturnsAsync(employee1);
            _seatRepositoryMock.Setup(seat => seat.GetBySeatId(seat1.SeatId)).ReturnsAsync(seat1);

            //Act
            var list = await _reservationManagementService.ListOfReservationVM(date);


            //Assert
            Assert.Equal(reservationList.Count, list.Count);
            for(int i=0;i< reservationList.Count; i++)
            {
                Assert.Equal(reservationList[i].ReservationId, list[i].ReservationId);
                Assert.Equal(reservationList[i].EmployeeId , list[i].EmployeeId);
                Assert.Equal(reservationList[i].EmployeeName, list[i].EmployeeName);
                Assert.Equal(reservationList[i].SeatId, list[i].SeatId);
                Assert.Equal(reservationList[i].SeatName, list[i].SeatName);
                Assert.Equal(reservationList[i].BookingDate, list[i].BookingDate);
            }


        }

        [Fact]
        public async Task ListOfReservationVm_ExceptNullReservationList_WhenAppropiateDateIsNotPassed()
        {
            //Arrange
            DateTime date = new DateTime(2022 - 09 - 12);
            ReservationViewModel reserve1 = new ReservationViewModel()
            {
                EmployeeId = 1,
                EmployeeName = "Rahul",
                SeatId = 1,
                SeatName = "A1",
                BookingDate = date
            };
            ReservationViewModel reserve2 = new ReservationViewModel()
            {
                EmployeeId = 2,
                EmployeeName = "Rohan",
                SeatId = 2,
                SeatName = "A2",
                BookingDate = date

            };
            EmployeeViewModel employee1 = new EmployeeViewModel()
            {
                EmployeeId = 1,
                EmployeeName = "Rahul"
            };
            EmployeeViewModel employee2 = new EmployeeViewModel()
            {
                EmployeeId = 2,
                EmployeeName = "Rohan"
            };
            SeatViewModel seat1 = new SeatViewModel()
            {
                SeatId = 1,
                SeatName = "A1"
            };
            SeatViewModel seat2 = new SeatViewModel()
            {
                SeatId = 2,
                SeatName = "A2"
            };
            List<ReservationViewModel> reservationList = new List<ReservationViewModel>();
            //reservationList.Add(reserve1);
           // reservationList.Add(reserve2);

            _reservationRepositoryMock.Setup(reservation => reservation.GetAll()).ReturnsAsync(reservationList);
            _employeeRepositoryMock.Setup(employee => employee.GetByEmployeeId(employee1.EmployeeId)).ReturnsAsync(employee1);
            _seatRepositoryMock.Setup(seat => seat.GetBySeatId(seat1.SeatId)).ReturnsAsync(seat1);

            //Act
            var list = await _reservationManagementService.ListOfReservationVM(new DateTime(2022-09-15));


            //Assert
            Assert.Equal(reservationList.Count, list.Count);
            for (int i = 0; i < reservationList.Count; i++)
            {
                Assert.Equal(reservationList[i].ReservationId, list[i].ReservationId);
                Assert.Equal(reservationList[i].EmployeeId, list[i].EmployeeId);
                Assert.Equal(reservationList[i].EmployeeName, list[i].EmployeeName);
                Assert.Equal(reservationList[i].SeatId, list[i].SeatId);
                Assert.Equal(reservationList[i].SeatName, list[i].SeatName);
                Assert.Equal(reservationList[i].BookingDate, list[i].BookingDate);
            }


        }




    }
}
