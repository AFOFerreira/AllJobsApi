using AllJobsApi.Models.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AllJobsApi.Controllers
{
    [ApiController]
    [Route("Banco")]
    public class BancoController : ControllerBase
    {
        private readonly IDAO _repository;
        public BancoController(IDAO _dao)
        {
            _repository = _dao;
        }

        [HttpPost]
        public bool VerificaSenhaBanco(string senha)
        {
           
               return _repository.VerificaSenhaBanco(senha);
          
        }
    }
}
