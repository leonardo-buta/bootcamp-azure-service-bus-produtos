using Evendas.Application.RequestsModel;
using System.Threading.Tasks;

namespace Evendas.Application.Interfaces
{
    public interface IServiceBusSender
    {
        Task SendCreateProdutoMessage(CreateProdutoRequest request);
        Task SendUpdateProdutoMessage(string codProduto, UpdateProdutoRequest request);
        Task SendProdutoVendidoMessage(string codProduto, VendaProdutoRequest request);
    }
}
