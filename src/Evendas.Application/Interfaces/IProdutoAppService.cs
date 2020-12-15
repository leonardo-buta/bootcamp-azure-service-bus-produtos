using Evendas.Application.RequestsModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Evendas.Application.Interfaces
{
    public interface IProdutoAppService
    {
        Task<GetProdutoRequest> GetByIdAsync(long id);
        Task<GetProdutoRequest> GetByCodProdutoAsync(string codProduto);
        Task Create(CreateProdutoRequest createProductRequest);
        Task Update(string codProduto, UpdateProdutoRequest updateProductRequest);
        IEnumerable<GetProdutoRequest> GetAll();
        IEnumerable<GetProdutoRequest> GetAllWithStock();
        Task VenderProduto(string codProduto, int quantidade);
    }
}
