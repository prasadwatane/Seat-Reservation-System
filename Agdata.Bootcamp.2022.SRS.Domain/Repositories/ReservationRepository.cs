using Agdata.Bootcamp._2022.SRS.Domain.Model;
using Agdata.Bootcamp._2022.SRS.Domain.ViewModel;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agdata.Bootcamp._2022.SRS.Domain.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ISRSDbContext _srsDbContext;
        private readonly IMapper _mapper;

        public ReservationRepository(ISRSDbContext srsDbContext, IMapper mapper)
        {
            _srsDbContext=srsDbContext;
            _mapper=mapper;
        }

        public async Task<int> Add(ReservationViewModel reservation)
        {
            _srsDbContext.Reservations.Add(_mapper.Map<Reservation>(reservation));
            return await _srsDbContext.SaveChangesAsync();           
        }

        public async Task<int> AddRange(List<ReservationViewModel> reservationsList)
        {
            await _srsDbContext.Reservations.AddRangeAsync( _mapper.Map<List<ReservationViewModel>, List<Reservation>>(reservationsList));
            return await _srsDbContext.SaveChangesAsync();
        }

        public async Task<int> Delete(int reservationId)
        {
            var reservation = await _srsDbContext.Reservations.FindAsync(reservationId);
            if(reservation != null)
            {
                _srsDbContext.Reservations.Remove(reservation);
                
            }
            return await _srsDbContext.SaveChangesAsync();
        }

        public async Task<List<ReservationViewModel>> GetAll()
        {
            var resevationList = _mapper.Map<List<Reservation>, List<ReservationViewModel>>(await _srsDbContext.Reservations.ToListAsync());
            return resevationList;
        }

        public async Task<ReservationViewModel> GetById(int reservationId)
        {
            var reservation =_mapper.Map<ReservationViewModel>(await _srsDbContext.Reservations.FindAsync(reservationId));
            return reservation;       
        }

    }
}
