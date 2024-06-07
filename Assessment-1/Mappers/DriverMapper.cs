using Assessment_1.Entitites;
using Assessment_1.Enums;
using Assessment_1.Models.Request;

namespace Assessment_1.Mappers
{
    public static class DriverMapper
    {
        public static (User, VehicleAndAvailability) ToDriverAndVehicle(this AddDriver addDriver)
        {
            User user = new User
            {
                UserId = Guid.NewGuid(),
                Name = addDriver.Name,
                Email = addDriver.Email,
                Phone = addDriver.Phone,
                Password = addDriver.Password,
                UserType = UserType.Driver,
            };

            VehicleType vehicleType = addDriver.VehicleType.ToLower() switch
            {
                "bike" => VehicleType.Bike,
                "car" => VehicleType.Car,
                "auto" => VehicleType.Auto,
            };

            VehicleAndAvailability vehicle = new VehicleAndAvailability
            {
                Id = Guid.NewGuid(),
                VehicleType = vehicleType,
                VehicleNumber = addDriver.VehicleNumber,
                IsAvailable = true,
                UserId = user.UserId
            };

            return (user, vehicle);
        }
    }
}
