using Assessment_1.Entitites;
using Assessment_1.Enums;
using Assessment_1.Models.Request;

namespace Assessment_1.Mappers
{
    public static class UserMapper
    {
        public static User ToUser(this AddUser addUser)
        {
            return new User
            {
                UserId = Guid.NewGuid(),
                Name = addUser.Name,
                Email = addUser.Email,
                Phone = addUser.Phone,
                Password = addUser.Password,
                UserType = UserType.Rider
            };
        }
    }
}
