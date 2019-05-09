using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SimpleApiWithAuth.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PrivateValueController : ControllerBase
    {
        // GET
        [HttpGet("")]
        public IActionResult Index()
        {
            return new OkResult();
        }
    }
}