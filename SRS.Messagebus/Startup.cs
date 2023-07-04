using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Agdata.Bootcamp._2022.SRS.Services.Contract;
using Agdata.Bootcamp._2022.SRS.Services.Implementations;
using MassTransit;
using MediatR;

namespace SRS.Messagebus
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));
            services.AddSingleton<ISeatManagementService, SeatManagementService>();
            services.AddSingleton<IReservationManagementService, ReservationManagementService>();
        }
    }
}