using Assessment_1.Entity;
using Assessment_1.ViewModel.RequestVM;
using Assessment_1.ViewModel.ResponseVM;

namespace Assessment_1.Interfaces.IRepository
{
    public interface IRiderRepository
    {
        int AddRider(AddRiderVM riderVM);
        Rider GetRiderByRiderEmail(string email); 
        int BookRide(Ride ride);


    }
}
