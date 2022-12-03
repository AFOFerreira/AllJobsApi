namespace AllJobsApi.Models.Model
{
    public class Mesa
    {
        public int? Numero { get; set; }
        public string? Nome { get; set; }
        public int? Atendente { get; set; }
        public int? Pedido { get; set; }
        public List<Produto>? Produtos { get; set; }
    }
}
