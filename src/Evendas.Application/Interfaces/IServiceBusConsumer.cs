namespace Evendas.Application.Interfaces
{
    public interface IServiceBusTopicSubscription
    {
        void RegisterMessageHandlerCreateProduto();
        void RegisterMessageHandlerUpdateProduto();
        void RegisterMessageHandlerProdutoVendido();
    }
}
