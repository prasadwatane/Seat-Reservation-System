using Agdata.Bootcamp._2022.SRS.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agdata.Bootcamp._2022.SRS.Domain
{
    public interface IReservationRepository
    {
        Task<int> Add(ReservationViewModel reservation);

        Task<int> Delete(int reservationId);

        Task<ReservationViewModel> GetById(int reservationId);

        Task<List<ReservationViewModel>> GetAll();

        Task<int> AddRange(List<ReservationViewModel> reservations);
    }
}
