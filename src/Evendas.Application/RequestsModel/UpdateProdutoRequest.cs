using System.ComponentModel.DataAnnotations;

namespace Evendas.Application.RequestsModel
{
    public class UpdateProdutoRequest
    {
        public long Id { get; set; }

        [MaxLength(100)]
        public string CodProduto { get; set; }

        [MaxLength(100)]
        public string Nome { get; set; }

        public decimal Preco { get; set; }

        public int QtdEstoque { get; set; }
    }
}
