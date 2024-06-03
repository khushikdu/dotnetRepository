using Assignment_2.DTO;

namespace Assignment_2.Services.Interface
{
    public interface IAuthService
    {
        string Authenticate(LoginUser loginRequest);

    }
}
