using Agdata.Bootcamp._2022.SRS.Services.Contract;
using Agdata.Bootcamp._2022.SRS.Domain.Model;
using Agdata.Bootcamp._2022.SRS.Domain.ViewModel;
using Agdata.Bootcamp._2022.SRS.Domain;


namespace Agdata.Bootcamp._2022.SRS.Services.Implementations
{
    public class SeatManagementService : ISeatManagementService
    {
        private readonly ISeatRepository _seatRepository;

        public SeatManagementService(ISeatRepository seatRepository )
        {
            _seatRepository = seatRepository;
        }
       
        public async Task<int> AddSeats(List<string> seatVMs)
        {   int seatcount=seatVMs.Count();
            int addedcount = 0;
            var seatsViewModelList = await _seatRepository.GetAll();
            foreach (var seatVM in seatVMs)
            {
                SeatViewModel temporary = new SeatViewModel()
                {
                    SeatId = 0,
                    SeatName = seatVM

                };
                var flag = 0;
                foreach (var seat in seatsViewModelList)
                {
                    if (seat.SeatName == seatVM)
                    {
                        flag = 1;
                    }
                }
                if(flag == 0)
                {
                    await _seatRepository.Add(temporary);
                    addedcount=addedcount+1;
                }
            }

           if(seatcount == addedcount)
            {
                return 1;
            }
           if(seatcount > addedcount && addedcount!=0)
            {
                return 2;
            }
            if (addedcount == 0)
            {
                return 0;
            }
            return 0;
        }
        public async Task<bool> AddSeat(SeatViewModel seatVM)
        {
            var seatsViewModelList = await _seatRepository.GetAll();
            foreach (var seat in seatsViewModelList)
            {
                if (seat.SeatName == seatVM.SeatName)
                    return false;
            }
            await _seatRepository.Add(seatVM);
            return true;
        }

        public async Task<bool> DeleteSeat(List<int> seatIdList)
        {
            for (int i=0; i < seatIdList.Count;i++)
            {
                var seat = await _seatRepository.GetBySeatId(seatIdList[i]);
                if (seat != null)
                {
                    await _seatRepository.Delete(seat);
                }
                else
                    return false;
            }
            return true;
            
        }

        public async Task<List<SeatViewModel>> GetAllSeats()
        {
            return await _seatRepository.GetAll();
        }


    }
}