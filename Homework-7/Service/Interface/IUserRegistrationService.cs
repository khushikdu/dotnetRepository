using Homework_7.Entity;
using Homework_7.ViewModel;

namespace Homework_7.Service.Interface
{
    /// <summary>
    /// Interface for user registration service.
    /// </summary>
    public interface IUserRegistrationService
    {
        void Save(UserVM user);
        List<UserVM> GetAllUsers();
    }
}
