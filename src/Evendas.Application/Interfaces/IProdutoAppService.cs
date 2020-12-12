using Evendas.Application.RequestsModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Evendas.Application.Interfaces
{
    public interface IProdutoAppService
    {
        Task Create(CreateProdutoRequest createProductRequest);
        Task Update(UpdateProdutoRequest updateProductRequest);
        IEnumerable<ProdutoResult> GetAll();        
    }
}
