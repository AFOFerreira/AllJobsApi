using AllJobsApi.Models.Interface;
using AllJobsApi.Models.Model;
using Microsoft.AspNetCore.Mvc;

namespace AllJobsApi.Controllers
{
    [ApiController]
    [Route("Mesa")]
    public class MesaController : ControllerBase
    {
        private readonly IDAO _repository;
        public MesaController(IDAO _dao)
        {
            _repository = _dao;
        }

        [HttpGet]
        public IActionResult BuscarMesasOcupadas()
        {
            try
            {
                var lista = _repository.BuscaMesasOcupadas();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public int AbrirComanda([FromBody] Mesa mesa)
        {
            try
            {
                return _repository.AbrirComanda(mesa);
        
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
