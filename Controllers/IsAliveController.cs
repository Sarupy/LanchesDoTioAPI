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
            return Ok(new { status = "Alive!" });
        }
    }
}
