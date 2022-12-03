using AllJobsApi.Models.Model;

namespace AllJobsApi.Models.Interface
{
    public interface IDAO
    {
        List<Produto> BuscaProdutos();
        List<Produto> BuscaProdutosPeloPedido(int pedido);
        List<Atendente> BuscaAtendente();
        List<Mesa> BuscaMesasOcupadas();
        int AbrirComanda(Mesa m);
        bool AdicionarPedidoMesa(Mesa m);
        bool VerificaSenhaBanco(string senha);
    }
}
