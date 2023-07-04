using Agdata.Bootcamp._2022.SRS.Domain;
using Agdata.Bootcamp._2022.SRS.Domain.ViewModel;
using Agdata.Bootcamp._2022.SRS.Services.Contract;
using System;


namespace Agdata.Bootcamp._2022.SRS.Services.Implementations
{
    public class ReservationManagementService : IReservationManagementService
    {
        private readonly IReservationRepository _reservbationRepository;
        private readonly ISeatRepository _seatRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public ReservationManagementService(IReservationRepository reservationRepository, ISeatRepository seatRepository, IEmployeeRepository employeeRepository)
        {
            _reservbationRepository = reservationRepository;
            _seatRepository = seatRepository;
            _employeeRepository = employeeRepository;
        }


        public async Task<List<SeatViewModel>> UnreserveSeatList(DateTime date)
        {
            var reservationList = await ListOfReservationVM(date);
            var seats = await _seatRepository.GetAll();
            var seatList = (from seat in seats
                            where !reservationList.Any(x => x.SeatId == seat.SeatId)
                            select new SeatViewModel
                            {
                                SeatId = seat.SeatId,
                                SeatName = seat.SeatName,
                            }).ToList();
            return seatList;

        }

        public async Task<bool> ReserveSeat(ReservationViewModel reservationViewModel)
        {  

            if(reservationViewModel.BookingDate  < DateTime.Today)
            {
                return false;
            }
            else
            {
                if (await IsSeatExists(reservationViewModel) == true)
                {
                    if (await IsSeatAlreadyReserved(reservationViewModel) == true)
                        return false;
                    else
                    {
                        var employee = await IsEmployeeAlreadyExists(reservationViewModel);

                        if (employee == null)
                        {
                            EmployeeViewModel employee1 = new EmployeeViewModel()
                            {
                                EmployeeName = reservationViewModel.EmployeeName
                            };
                            await _employeeRepository.Add(employee1);
                            await IsEmployeeAlreadyExists(reservationViewModel);
                        }
                        else
                        {
                            reservationViewModel.EmployeeId = employee.EmployeeId;
                        }
                        await _reservbationRepository.Add(reservationViewModel);
                        return true;


                    }
                }
                else
                    return false;
            }
        }

        public async Task<bool> CancelSeat(int reservationId)
        {
            var reservation = await _reservbationRepository.GetById(reservationId);

            if (reservation != null)
            {
                await _reservbationRepository.Delete(reservationId);
                return true;
            }
            else
                return false;
        } 


        public async Task<List<ReservationViewModel>> ListOfReservationVM(DateTime reservationDate)
        {
            var reservationList= await _reservbationRepository.GetAll();
            List<ReservationViewModel> reservationsOnThatDate = new List<ReservationViewModel>();
            foreach (var reservation in reservationList)
            {
                if(reservation.BookingDate  == reservationDate)
                {
                    reservation.EmployeeName = await GetEmployeeNameFromEmployeeTable(reservation.EmployeeId);
                    reservation.SeatName = await GetSeatNameFromSeatTable(reservation.SeatId);
                    reservationsOnThatDate.Add(reservation);
                }
            }
            return reservationsOnThatDate;
        }

        private async Task<string> GetEmployeeNameFromEmployeeTable(int employeeId)
        {
            EmployeeViewModel employee =await _employeeRepository.GetByEmployeeId(employeeId);
            if(employee != null)
                return employee.EmployeeName;
            return null;           
        }

        private async Task<string> GetSeatNameFromSeatTable(int seatId)
        {
            var seat = await _seatRepository.GetBySeatId(seatId);
            return seat != null ? seat.SeatName : null;
        }


        private async Task<EmployeeViewModel?> IsEmployeeAlreadyExists(ReservationViewModel reservationViewModel)
        {
            var employee = await _employeeRepository.GetByEmployeeName(reservationViewModel.EmployeeName);
            if(employee != null)
            {
                reservationViewModel.EmployeeId = employee.EmployeeId;
                return employee;
            }
            return null;
        }

        private async Task<bool> IsSeatExists(ReservationViewModel reservationViewModel)
        {
            var seat = await _seatRepository.GetBySeatName(reservationViewModel.SeatName);
            if(seat == null)
                return false;

            if(seat.SeatName == reservationViewModel.SeatName)
            {
                reservationViewModel.SeatId = seat.SeatId;
                return true;
            }
            return false;        
        }

        private async Task<bool> IsSeatAlreadyReserved(ReservationViewModel reservationViewModel)   
        {
            var reservationList = await _reservbationRepository.GetAll();   
            foreach (var reservation in reservationList)
            {

                if (reservation.SeatId == reservationViewModel.SeatId && reservation.BookingDate == reservationViewModel.BookingDate)
                    return true;
            }
            return false;
        }


    }
}
