using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRS.Messagebus.Queries;
using Agdata.Bootcamp._2022.SRS.Domain.ViewModel;
using Agdata.Bootcamp._2022.SRS.Domain;

namespace SRS.Messagebus.Handlers
{
    public class GetSeatListHandler : IRequestHandler<GetSeatListQuery, List<SeatViewModel>>
    {  
        private readonly ISRSDbContext _context;
        public GetSeatListHandler(ISRSDbContext data )
        {
            _context = data;
        }
        public Task<List<SeatViewModel>> Handle(GetSeatListQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
