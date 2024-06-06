using Assessment_1.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Assessment_1.Entitites
{
    public class VehicleAndAvailability
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public VehicleType VehicleType { get; set; }

        [Required]
        [MaxLength(20)]
        public string VehicleNumber { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
