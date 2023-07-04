using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agdata.Bootcamp._2022.SRS.Domain.ViewModel;

namespace SRS.Messagebus.Queries
{
    public record GetSeatListQuery(): IRequest<List<SeatViewModel>>;
    //public class GetSeatListQueryClass : IRequest<List<SeatViewModel>> { }


}
