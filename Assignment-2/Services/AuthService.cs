using Assignment_2.CustomExceptions;
using Assignment_2.DTO;
using Assignment_2.Repository;
using Assignment_2.Repository.Interface;
using Assignment_2.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Assignment_2.Services
{
    /// <summary>
    /// Service responsible for user authentication.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        /// <summary>
        /// Constructor for AuthService.
        /// </summary>
        /// <param name="authRepository">The authentication repository.</param>
        /// <param name="config">The application configuration.</param>
        /// <param name="userRepository">The user repository.</param>
        public AuthService(IAuthRepository authRepository, IConfiguration config, IUserRepository userRepository)
        {
            _authRepository = authRepository;
            _config = config;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Authenticates a user and generates a JWT token.
        /// </summary>
        /// <param name="loginRequest">The login credentials provided by the user.</param>
        /// <returns>A JWT token upon successful authentication.</returns>
        public string Authenticate(LoginUser loginRequest)
        {
            UserModel user = _userRepository.GetUserByUsername(loginRequest.Username);

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

            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            JwtSecurityToken token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
