using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace sqlserver_adonet_experiments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        [HttpGet]
        public IActionResult Test()
        {
            return Ok(new
            {
                Result = "Okay"
            });
        }
    }
}
