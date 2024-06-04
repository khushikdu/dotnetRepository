using Assessment_1.Entity;
using Assessment_1.Interfaces.IRepository;
using Assessment_1.ViewModel.RequestVM;
using Assignment_2.CustomExceptions;
using Assignment_2.Repository.Interface;
using Assignment_2.Services.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace Assessment_1.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IRiderRepository _riderRepository;
        private readonly IDriverRepository _driverRepository;

        private readonly IConfiguration _config;

        public AuthService(IAuthRepository authRepository, IConfiguration config, IRiderRepository riderRepository, IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
            _authRepository = authRepository;
            _config = config;
            _riderRepository = riderRepository;
            _driverRepository = driverRepository;
        }

        public string Authenticate(LoginRiderVM loginRequest)
        {
            Rider user = _riderRepository.GetRiderByRiderEmail(loginRequest.Email);

            if (user == null)
            {
                throw new InvalidCredentialsException("Invalid username or password");
            }

            if (user.Password != loginRequest.Password)
            {
                throw new InvalidCredentialsException("Incorrect password");
            }

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Claim[] claims = new[]
            //{
            //    new Claim(ClaimTypes.Email, user.Username),
            //    new Claim(ClaimTypes.Role, user.Role)
            //};

            JwtSecurityToken token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string Authenticate(LoginDriverVM loginRequest)
        {
            Driver user = _driverRepository.GetDriverByRiderEmail(loginRequest.Email);

            if (user == null)
            {
                throw new InvalidCredentialsException("Invalid username or password");
            }

            if (user.Password != loginRequest.Password)
            {
                throw new InvalidCredentialsException("Incorrect password");
            }

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Claim[] claims = new[]
            //{
            //    new Claim(ClaimTypes.Email, user.Username),
            //    new Claim(ClaimTypes.Role, user.Role)
            //};

            JwtSecurityToken token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
