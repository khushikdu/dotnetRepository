using Assessment_1.Entitites;
using Assessment_1.Enums;
using System;

namespace Assessment_1.Utilities
{
    public static class RatingsMapper
    {
        public static Ratings MapToRatings(Guid rideId, int rating, UserType userType)
        {
            return new Ratings
            {
                RatingId = Guid.NewGuid(),
                RideId = rideId,
                RatedBy = userType,
                RatingValue = rating
            };
        }
    }
}
