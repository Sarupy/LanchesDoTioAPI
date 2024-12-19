using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LanchesDoTioAPI.Controllers
{
    [Route("")]
    [ApiController]
    public class IsAliveController : ControllerBase
    {
        [HttpGet]
        public ActionResult<Object> Get()
        {
            using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            ILogger logger = factory.CreateLogger<Program>();
            logger.LogInformation("Hello World! Logging is fun.");
            return Ok(new { status = "Alive!" });
        }
    }
}
