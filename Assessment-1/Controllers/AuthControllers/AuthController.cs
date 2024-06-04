using Assessment_1.Interfaces.IService;
using Assessment_1.ViewModel.RequestVM;
using Assignment_2.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Assessment_1.Controllers.AuthControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _config;
        private readonly IRiderService _riderService;
        private readonly IAuthService _authService;
        public AuthController(IConfiguration config, IRiderService riderService, IAuthService authService)
        {
            _riderService = riderService;
            _config = config;
            _authService = authService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] LoginRiderVM loginRequest)
        {
            var token = _authService.Authenticate(loginRequest);
            return Ok(token);
        }
    }
}
