using BankApplicationAPI.Models;
using BankApplicationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly ConfigurationService _configurationService;

        public ConfigurationController(ConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        // GET: api/Configuration
        [HttpGet]
        [Authorize(Roles = "admin")] // Only admins can view configurations
        public async Task<ActionResult<IEnumerable<Configuration>>> GetConfigurations()
        {
            try
            {
                var configurations = await _configurationService.GetConfigurationsAsync();
                return Ok(configurations);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving configurations.");
            }
        }

        // GET: api/Configuration/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin")] // Only admins can view a specific configuration
        public async Task<ActionResult<Configuration>> GetConfiguration(int id)
        {
            try
            {
                var configurations = await _configurationService.GetConfigurationByConfigurationIdAsync(id);
                if (configurations == null)
                    return NotFound("Configuration not found.");

                return Ok(configurations);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving configuration.");
            }
        }

        // POST: api/Configuration
        [HttpPost]
        [Authorize(Roles = "admin")] // Only admins can create a new configuration
        public async Task<ActionResult> CreateConfiguration([FromBody] Configuration configuration)
        {
            if (configuration == null)
                return BadRequest("Invalid configuration data.");

            try
            {
                var result = await _configurationService.CreateConfigurationAsync(configuration);
                if (result)
                    return CreatedAtAction(nameof(GetConfiguration), new { id = configuration.ConfigurationId }, configuration);

                return BadRequest("Failed to create configuration.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating configuration.");
            }
        }

        // PUT: api/Configuration/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")] // Only admins can update a configuration
        public async Task<ActionResult> UpdateConfiguration(int id, [FromBody] Configuration configuration)
        {
            if (configuration == null || configuration.ConfigurationId != id)
                return BadRequest("Data mismatch.");

            try
            {
                var updatedConfiguration = await _configurationService.UpdateConfigurationAsync(configuration);
                if (updatedConfiguration != null)
                    return Ok(updatedConfiguration);

                return NotFound("Configuration not found.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating configuration.");
            }
        }

        // DELETE: api/Configuration/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")] // Only admins can delete a configuration
        public async Task<ActionResult> DeleteConfiguration(int id)
        {
            try
            {
                var configuration = await _configurationService.GetConfigurationByConfigurationIdAsync(configurationId: id);
                if (configuration == null)
                    return NotFound("Configuration not found.");

                var result = await _configurationService.DeleteConfigurationAsync(configuration);
                if (result)
                    return NoContent();

                return BadRequest("Failed to delete configuration.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting configuration.");
            }
        }
    }
}
