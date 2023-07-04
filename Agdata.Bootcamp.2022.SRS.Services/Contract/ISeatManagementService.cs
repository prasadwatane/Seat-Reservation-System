using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agdata.Bootcamp._2022.SRS.Domain.ViewModel;

namespace Agdata.Bootcamp._2022.SRS.Services.Contract
{
    public interface ISeatManagementService
    {
        Task<bool> AddSeat(SeatViewModel seatVM);

        Task<bool> DeleteSeat(List<int> seatIdList);

        Task<List<SeatViewModel>> GetAllSeats();
        Task<int> AddSeats(List<string> seatVMs);


    }
}
