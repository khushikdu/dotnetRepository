using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assessment_1.Entity
{
    public class Ratings
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(Ride))]
        public int RideId { get; set; }
        public int RatingValue { get; set; }

    }
}
