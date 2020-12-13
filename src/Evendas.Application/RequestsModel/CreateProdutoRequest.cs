using System.ComponentModel.DataAnnotations;

namespace Evendas.Application.RequestsModel
{
    public class CreateProdutoRequest
    {
        [MaxLength(100, ErrorMessage ="{0} não deve ser maior que {1}")]
        public string CodProduto { get; set; }

        [MaxLength(100, ErrorMessage = "{0} não deve ser maior que {1}")]
        public string Nome { get; set; }
        
        [Range(typeof(decimal), "1", "999999", ErrorMessage = "{0} deve ser maior que zero")]
        public decimal Preco { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "{0} deve ser maior que zero")]
        public int QtdEstoque { get; set; }
    }
}
