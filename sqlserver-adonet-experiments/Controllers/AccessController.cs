using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace sqlserver_adonet_experiments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly DataContext dataContext;

        public AccessController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult Test()
        {
            dataContext.Connection();

            return Ok(new
            {
                Result = "Okay"
            });
        }
    }
}
