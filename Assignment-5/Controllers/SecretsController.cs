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

        /// <summary>
        /// Creates a new secret in the Key Vault.
        /// </summary>
        /// <param name="addSecret">The secret details to add.</param>
        /// <returns>The result of the operation.</returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateSecret([FromBody] AddSecret addSecret)
        {
            var result = await _keyVaultService.CreateSecretAsync(addSecret.Name, addSecret.Value);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves the value of a secret from the Key Vault.
        /// </summary>
        /// <param name="name">The name of the secret to retrieve.</param>
        /// <returns>The value of the secret.</returns>
        [HttpGet("retrieve/{name}")]
        public async Task<IActionResult> RetrieveSecret(string name)
        {
            var result = await _keyVaultService.RetrieveSecretAsync(name);
            return Ok(result);
        }

        /// <summary>
        /// Deletes a secret from the Key Vault.
        /// </summary>
        /// <param name="deleteSecret">The name of the secret to delete.</param>
        /// <returns>The result of the operation.</returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteSecret([FromBody] DeleteSecret deleteSecret)
        {
            var result = await _keyVaultService.DeleteSecretAsync(deleteSecret.Name);
            return Ok(result);
        }

        /// <summary>
        /// Purges a deleted secret from the Key Vault.
        /// </summary>
        /// <param name="name">The name of the secret to purge.</param>
        /// <returns>The result of the operation.</returns>
        [HttpDelete("purge/{name}")]
        public async Task<IActionResult> PurgeSecret(string name)
        {
            var result = await _keyVaultService.PurgeSecretAsync(name);
            return Ok(result);
        }
    }
}
