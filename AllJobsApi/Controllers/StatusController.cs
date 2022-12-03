using Microsoft.AspNetCore.Mvc;

namespace AllJobsApi.Controllers
{
    [ApiController]
    [Route("Status")]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        public IActionResult VerificaStatus()
        {
            try
            {
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(false);
            }
        }
      
    }
}
