using Agdata.Bootcamp._2022.SRS.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agdata.Bootcamp._2022.SRS.Domain
{
    public interface ISRSDbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Seat> Seats { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        
    }
}
