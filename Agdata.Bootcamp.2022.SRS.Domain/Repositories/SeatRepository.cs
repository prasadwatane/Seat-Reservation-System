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
    public class SeatRepository : ISeatRepository

    {
        private readonly ISRSDbContext _srsDbContext;
        private readonly IMapper _mapper;
        

        public SeatRepository(ISRSDbContext srsDbContext, IMapper mapper)
        {
            _srsDbContext = srsDbContext;
            _mapper = mapper;
        }

        public async Task<int> Add(SeatViewModel seat)
        {
            _srsDbContext.Seats.Add(_mapper.Map<Seat>(seat));
            return await _srsDbContext.SaveChangesAsync();          
        }

        public async Task<int> AddRange(List<SeatViewModel> seatsList)
        {
            _srsDbContext.Seats.AddRange(_mapper.Map<List<Seat>>(seatsList));
            return await _srsDbContext.SaveChangesAsync();
        }

        public async Task<int> Delete(SeatViewModel seat)
        {
            Seat seat1= await _srsDbContext.Seats.SingleAsync(x=>x.SeatId==seat.SeatId);
            _srsDbContext.Seats.Remove(seat1);
            return await _srsDbContext.SaveChangesAsync();
        }

        public async Task<List<SeatViewModel>> GetAll()
        {
            var SeatVMList = _mapper.Map<List<Seat>, List<SeatViewModel>>(await _srsDbContext.Seats.ToListAsync());
            return SeatVMList;  
        }

        public async Task<SeatViewModel> GetBySeatId(int seatId)
        {
            var seat = _mapper.Map<SeatViewModel>(await _srsDbContext.Seats.FindAsync(seatId));
            return seat;
        }

        public async Task<SeatViewModel?> GetBySeatName(string seatName)
        {
            var seatList = await _srsDbContext.Seats.ToListAsync();
            foreach(var seat in seatList)
            {
                if (seat.SeatName == seatName)
                {
                    var seatVm =_mapper.Map<SeatViewModel>(seat);
                    return seatVm;

                }                    
            }
            return null;           
        }

        public async Task<List<SeatViewModel>> GetUnreserved()
        {
            var SeatVMList = _mapper.Map<List<Seat>, List<SeatViewModel>>(await _srsDbContext.Seats
                .GroupJoin(_srsDbContext.Reservations,
                            l => l.SeatId, r => r.SeatId
                            , (x, y) => new { Seat = x, Reserves = y })
                .SelectMany(x => x.Reserves.DefaultIfEmpty(), (x, y) => new { seat = x.Seat, reserve = y })
                .Where(s => s.reserve == null)
                .Select(s => s.seat)
                .ToListAsync());
            return SeatVMList;
        }

    }
}
