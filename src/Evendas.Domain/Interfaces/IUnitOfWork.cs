namespace Evendas.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        bool Commit();
    }
}
