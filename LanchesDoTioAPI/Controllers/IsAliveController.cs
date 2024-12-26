using LanchesDoTioAPI.Data;
using LanchesDoTioAPI.DTO;
using LanchesDoTioAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LanchesDoTioAPI.Controllers
{
    [Route("")]
    [ApiController]
    public class IsAliveController : ControllerBase
    {
        private readonly ILogger _logger;

        public IsAliveController(ILogger<IsAliveController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<Object> Get()
        {
            _logger.LogInformation("Log started.",
            DateTime.UtcNow.ToLongTimeString());

            return Ok(new { status = "Alive!" });
        }
    }
}
