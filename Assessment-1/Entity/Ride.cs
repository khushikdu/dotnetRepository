using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assessment_1.Entity
{
    public class Ride
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RideId { get; set; }

        [ForeignKey(nameof(Rider))]
        public int RiderId { get; set; }

        [ForeignKey(nameof(Driver))]
        public int DriverId { get; set; }
        public string Pickup { get; set; }
        public string Drop { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }

    }
}
