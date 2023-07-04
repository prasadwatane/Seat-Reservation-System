using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Agdata.Bootcamp._2022.SRS.Domain.Model
{
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReservationId { get; set; }

        [ForeignKey ("EmployeeId")]
        public int EmployeeId { get; set; }

        [ForeignKey("SeatId")]
        public int SeatId { get; set; }

        [Column("BookingDate")]
        public DateTime BookingDate { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Seat Seat { get; set; }



    }
}
