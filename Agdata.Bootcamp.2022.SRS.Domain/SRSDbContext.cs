using Agdata.Bootcamp._2022.SRS.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agdata.Bootcamp._2022.SRS.Domain
{
    public class SRSDbContext : DbContext, ISRSDbContext
    {
        public SRSDbContext(DbContextOptions<SRSDbContext> options)
            : base(options) 
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Employee> Employees { get; set; } 

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Seat> Seats { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if(!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Data Source=INDLAP-RGAVHANE;Initial Catalog=SeatReservationSystem;Integrated Security=True");
        //    }
        //    base.OnConfiguring(optionsBuilder); 
        //}

    }
}
