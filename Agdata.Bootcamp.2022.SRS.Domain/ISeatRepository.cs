using Agdata.Bootcamp._2022.SRS.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agdata.Bootcamp._2022.SRS.Domain
{
    public interface ISeatRepository
    {
        Task<int> Add(SeatViewModel seat);

        Task<int> Delete(SeatViewModel seat);

        Task<List<SeatViewModel>> GetAll();

        Task<SeatViewModel> GetBySeatId(int seatId);

        Task<SeatViewModel?> GetBySeatName(string seatName);

        Task<int> AddRange(List<SeatViewModel> seats);

        Task<List<SeatViewModel>> GetUnreserved();
    }
}
