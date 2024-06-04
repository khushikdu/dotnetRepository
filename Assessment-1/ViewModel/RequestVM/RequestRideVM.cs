using System.ComponentModel.DataAnnotations;

namespace Assessment_1.ViewModel.RequestVM
{
    public class RequestRideVM
    {
        [Required]
        [MinLength(3)]
        public string PickUp { get; set; }

        [Required]
        [MinLength(3)]
        public string Drop { get; set; }

        [Required]
        [MinLength(3)]
        public string Vehicle { get; set; }
    }
}
