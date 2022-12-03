namespace AllJobsApi.Models.Model
{
    public class Produto
    {
        public int Id { get; set; }
        public string NomeProduto { get; set; }
        public double Valor { get; set; }
        public int Quantidade{ get; set; }
        public string? Observacao { get; set; }
    }
}
