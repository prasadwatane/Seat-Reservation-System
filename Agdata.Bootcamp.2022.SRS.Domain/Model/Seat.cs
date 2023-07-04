using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agdata.Bootcamp._2022.SRS.Domain.Model
{
    public class Seat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SeatId { get; set; }

        [Column("SeatName")]
        public string SeatName { get; set; }

        //public virtual Reservation Reservation { get; set; }


    }
}
