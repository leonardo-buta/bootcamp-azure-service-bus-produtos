using System.Threading.Tasks;

namespace Evendas.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
    }
}
