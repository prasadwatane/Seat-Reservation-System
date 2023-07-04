using Agdata.Bootcamp._2022.SRS.Services.Contract;
using Agdata.Bootcamp._2022.SRS.Services.Implementations;
using MediatR;
namespace AGDATA.Bootcamp._2022.SRS.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));
            services.AddSingleton<ISeatManagementService, SeatManagementService>();
            services.AddSingleton<IReservationManagementService, ReservationManagementService>();
        }
    }
}
