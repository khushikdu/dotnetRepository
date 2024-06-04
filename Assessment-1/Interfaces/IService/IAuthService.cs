using Assessment_1.ViewModel.RequestVM;

namespace Assignment_2.Services.Interface
{
    public interface IAuthService
    {
        string Authenticate(LoginRiderVM loginRequest);

    }
}
