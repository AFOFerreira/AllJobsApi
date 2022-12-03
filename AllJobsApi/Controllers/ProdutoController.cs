using AllJobsApi.Models.Interface;
using AllJobsApi.Models.Model;
using AllJobsApi.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AllJobsApi.Controllers
{
    [ApiController]
    [Route("Produtos")]
    public class ProdutoController : ControllerBase
    {
        private readonly IDAO _repository;
        public ProdutoController(IDAO _dao)
        {
            _repository = _dao;
        }

        [HttpGet]
        public IActionResult BuscarProdutos()
        {
            try
            {
                var lista = _repository.BuscaProdutos();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("BuscarPeloPedido/{pedido}")]
        public IActionResult BuscarProdutosPeloPedido(int pedido)
        {
            try
            {
                var lista = _repository.BuscaProdutosPeloPedido(pedido);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public bool EnviarProdutos([FromBody] Mesa mesa)
        {
            try
            {
                return _repository.AdicionarPedidoMesa(mesa);

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
