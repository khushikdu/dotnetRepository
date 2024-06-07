using Assessment_1.Models.Request;

namespace Assessment_1.Interfaces.IService
{
    public interface IAuthorizeService
    {
        string AddRider(AddUser addUser);
        string AddDriver(AddDriver addDriver);
        string Login(UserLogin userLogin);
    }
}