﻿using Assessment_1.Enums;
using Assessment_1.Validations;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Assessment_1.Models.Request
{
    public class RideRequest
    {
        [Required(ErrorMessage = "Pickup location is required")]
        public string PickupLocation { get; set; }

        [Required(ErrorMessage = "Drop location is required")]
        public string DropLocation { get; set; }

        [Required(ErrorMessage = "Type of ride is required")]
        [VehicleTypeValidation]
        public string TypeOfRide { get; set; }
    }
}
