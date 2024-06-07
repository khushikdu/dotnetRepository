namespace Assessment_1.Constants
{
    public static class ErrorMessages
    {
        public const string UserNotRider = "User not a Rider.";
        public const string UserNotDriver = "User not a Driver.";
        public const string RiderExists = "Rider already exists.";
        public const string DriverExists = "Driver already exists.";
        public const string NoAvailableDriver = "No available driver found.";
        public const string RideNotFound = "Ride not found.";
        public const string RiderNotFound = "Rider not found.";
        public const string DriverNotFound = "Driver not found.";
        public const string CannotRateRideNotEnded = "Cannot rate a ride that has not ended.";
        public const string AlreadyRatedDriver = "You have already rated this driver.";
        public const string AlreadyRatedRider = "You have already rated this rider.";
        public const string InvalidRating = "Rating must be between 1 and 5.";
        public const string CannotEndRideNotStarted = "Cannot end a ride that has not started.";
        public const string CannotCancelRideStarted = "Cannot cancel a ride that has already started.";
        public const string InvalidPhoneEmailOrPassword = "Invalid Email/Phone or Password.";
        public const string CannotToggeState = "Cannot Toggle availability for Pending or Ongoing Rides"; 
        public const string NoOngoingRideAvailable = "No ongoing ride available.";
        public const string InvalidOTP = "Invalid OTP.";
        public const string RideNotInNotStartedStatus = "Ride is not in 'NotStarted' status.";
        public const string CannotToggleState = "Cannot toggle availability while having an ongoing or not started ride.";
        public const string NoVehicleAssigned = "No vehicle assigned to this driver.";
        public const string RideNotAllowed = "You are not allowed to request for another ride.";
    }
}
