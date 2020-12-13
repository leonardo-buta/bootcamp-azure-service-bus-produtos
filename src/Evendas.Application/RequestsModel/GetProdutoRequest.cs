namespace Evendas.Application.RequestsModel
{
    public class GetProdutoRequest
    {
        public long Id { get; set; }

        public string CodProduto { get; set; }

        public string Nome { get; set; }

        public decimal Preco { get; set; }

        public int QtdEstoque { get; set; }
    }
}
