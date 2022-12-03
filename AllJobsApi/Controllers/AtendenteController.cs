using AllJobsApi.Models.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AllJobsApi.Controllers
{
    [ApiController]
    [Route("Atendente")]
    public class AtendenteController : ControllerBase
    {
        private readonly IDAO _repository;
        public AtendenteController(IDAO _dao)
        {
            _repository = _dao;
        }

        [HttpGet]
        public IActionResult BuscarAtendentes()
        {
            try
            {
                var lista = _repository.BuscaAtendente();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    
    }
}
