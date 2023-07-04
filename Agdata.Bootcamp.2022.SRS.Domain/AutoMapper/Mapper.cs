using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Agdata.Bootcamp._2022.SRS.Domain.Model;
using Agdata.Bootcamp._2022.SRS.Domain.ViewModel;

namespace Agdata.Bootcamp._2022.SRS.Domain.AutoMapper
{
    public class Mapper : Profile
    {

        public Mapper()
        {
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
            CreateMap<Reservation, ReservationViewModel>().ReverseMap();
            CreateMap<Seat, SeatViewModel>().ReverseMap();
        }

    }
}

