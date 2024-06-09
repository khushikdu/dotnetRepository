using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Assignment_5.Interface.IService;
using Assignment_5.Model;

namespace Assignment_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecretsController : ControllerBase
    {
        private readonly IKeyVaultService _keyVaultService;

        public SecretsController(IKeyVaultService keyVaultService)
        {
            _keyVaultService = keyVaultService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateSecret([FromBody] SecretDto secretDto)
        {
            var result = await _keyVaultService.CreateSecretAsync(secretDto.Name, secretDto.Value);
            return Ok(result);
        }

        [HttpGet("retrieve/{name}")]
        public async Task<IActionResult> RetrieveSecret(string name)
        {
            var result = await _keyVaultService.RetrieveSecretAsync(name);
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteSecret([FromBody] DeleteSecretDto deleteSecretDto)
        {
            var result = await _keyVaultService.DeleteSecretAsync(deleteSecretDto.Name, deleteSecretDto.WaitDurationSeconds);
            return Ok(result);
        }

        [HttpDelete("purge/{name}")]
        public async Task<IActionResult> PurgeSecret(string name)
        {
            var result = await _keyVaultService.PurgeSecretAsync(name);
            return Ok(result);
        }
    }
}
