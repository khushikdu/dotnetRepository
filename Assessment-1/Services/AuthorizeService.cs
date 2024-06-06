using Assessment_1.DBContext;
using Assessment_1.Entitites;
using Assessment_1.Enums;
using Assessment_1.Interfaces.IService;
using Assessment_1.Mappers;
using Assessment_1.Models.Request;
using Assessment_1.Constants;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Assessment_1.Services
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly TaxiService _context;
        private readonly IConfiguration _configuration;

        public AuthorizeService(TaxiService context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }

        public string AddRider(AddUser addUser)
        {
            var existingUser = _context.Users
                .SingleOrDefault(u => (u.Email == addUser.Email || u.Phone == addUser.Phone) && u.UserType == UserType.Rider);

            if (existingUser != null)
            {
                return ErrorMessages.RiderExists;
            }

            var user = addUser.ToUser();
            _context.Users.Add(user);
            _context.SaveChanges();
            return "Rider created successfully";
        }

        public string AddDriver(AddDriver addDriver)
        {
            var existingDriver = _context.Users
                .SingleOrDefault(u => (u.Email == addDriver.Email || u.Phone == addDriver.Phone) && u.UserType == UserType.Driver);

            if (existingDriver != null)
            {
                return ErrorMessages.DriverExists;
            }

            var (user, vehicle) = addDriver.ToDriverAndVehicle();
            _context.Users.Add(user);
            _context.VehiclesAndAvailability.Add(vehicle);
            _context.SaveChanges();
            return "Driver created successfully";
        }

        public string Login(UserLogin userLogin)
        {
            var userType = userLogin.UserType.ToLower() switch
            {
                "rider" => UserType.Rider,
                "driver" => UserType.Driver
            };
            var user = _context.Users.FirstOrDefault(u => (u.Email == userLogin.EmailOrPhone || u.Phone == userLogin.EmailOrPhone) && u.Password == userLogin.Password &&u.UserType==userType);

            if (user == null)
            {
                return ErrorMessages.InvalidPhoneEmailOrPassword;
            }

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserType.ToString())
            };

            JwtSecurityToken token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
