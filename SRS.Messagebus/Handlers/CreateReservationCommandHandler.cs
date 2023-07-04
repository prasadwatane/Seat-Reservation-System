using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRS.Messagebus.Commands;
using Agdata.Bootcamp._2022.SRS.Domain.Model;
using Agdata.Bootcamp._2022.SRS.Domain.ViewModel;
using Agdata.Bootcamp._2022.SRS.Domain;

namespace SRS.Messagebus.Handlers
{
    public class CreateReservationCommandHandler : IRequestHandler<AddReservationCommand, int>
    {
        private readonly IReservationRepository _repository;

        public CreateReservationCommandHandler(IReservationRepository repository)
        {
            this._repository = repository;
        }

        public Task<int> Handle(AddReservationCommand request, CancellationToken cancellationToken)
        {
            return _repository.Add(request.ReservationViewModel);
            //throw new NotImplementedException();
        }
    }
}
