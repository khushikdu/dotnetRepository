using Assessment_1.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Assessment_1.Entitites
{
    public class Ratings
    {
        [Key]
        public Guid RatingId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid RideId { get; set; }

        [ForeignKey("RideId")]
        public Ride Ride { get; set; }

        [Required]
        public UserType RatedBy { get; set; }

        [Required]
        [Range(1, 5)]
        public int RatingValue { get; set; }
    }
}
