using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agdata.Bootcamp._2022.SRS.Domain.Model;
using Agdata.Bootcamp._2022.SRS.Domain.ViewModel;

namespace Agdata.Bootcamp._2022.SRS.Services.Contract
{
    public interface IReservationManagementService
    {
        Task<bool> ReserveSeat(ReservationViewModel reservationVM);

        Task<bool> CancelSeat(int seatId);

        Task<List<ReservationViewModel>> ListOfReservationVM(DateTime reservationDate);

        Task<List<SeatViewModel>> UnreserveSeatList(DateTime date);

    }
}
