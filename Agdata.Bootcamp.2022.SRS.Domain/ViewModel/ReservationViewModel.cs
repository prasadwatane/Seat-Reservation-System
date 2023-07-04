using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agdata.Bootcamp._2022.SRS.Domain.ViewModel
{
    public class ReservationViewModel
    {
        public int ReservationId { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public int SeatId { get; set; }

        public string SeatName { get; set; }

        public DateTime BookingDate { get; set; }

    }
}
