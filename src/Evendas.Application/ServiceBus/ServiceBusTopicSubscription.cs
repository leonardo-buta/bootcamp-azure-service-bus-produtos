using Evendas.Application.Interfaces;
using Evendas.Application.RequestsModel;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Evendas.Application.ServiceBus
{
    public class ServiceBusTopicSubscription : IServiceBusTopicSubscription
    {
        private readonly IConfiguration _configuration;
        private readonly IProdutoAppService _produtoAppService;
        private SubscriptionClient _subscriptionClient;

        public ServiceBusTopicSubscription(IConfiguration configuration, IProdutoAppService produtoAppService)
        {
            _configuration = configuration;
            _produtoAppService = produtoAppService;
        }

        public void RegisterMessageHandlerCreateProduto()
        {
            _subscriptionClient = new SubscriptionClient(_configuration.GetConnectionString("ServiceBusConnectionString"), "produtocriado", "ProdutoCriadoServicoVendas");

            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            _subscriptionClient.RegisterMessageHandler(ProcessCreateProdutoMessagesAsync, messageHandlerOptions);
        }

        public void RegisterMessageHandlerUpdateProduto()
        {
            _subscriptionClient = new SubscriptionClient(_configuration.GetConnectionString("ServiceBusConnectionString"), "produtoeditado", "ProdutoEditadoServicoVendas");

            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            _subscriptionClient.RegisterMessageHandler(ProcessUpdateProdutoMessagesAsync, messageHandlerOptions);
        }

        public void RegisterMessageHandlerProdutoVendido()
        {
            _subscriptionClient = new SubscriptionClient(_configuration.GetConnectionString("ServiceBusConnectionString"), "produtovendido", "ProdutoVendidoServicoEstoque");

            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            _subscriptionClient.RegisterMessageHandler(ProcessProdutoVendidoMessagesAsync, messageHandlerOptions);
        }

        private async Task ProcessCreateProdutoMessagesAsync(Message message, CancellationToken token)
        {
            var produto = JsonConvert.DeserializeObject<CreateProdutoRequest>(Encoding.UTF8.GetString(message.Body));
            await _produtoAppService.Create(produto);
            await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private async Task ProcessUpdateProdutoMessagesAsync(Message message, CancellationToken token)
        {
            var produto = JsonConvert.DeserializeObject<UpdateProdutoRequest>(Encoding.UTF8.GetString(message.Body));
            await _produtoAppService.Update(produto.CodProduto, produto);
            await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private async Task ProcessProdutoVendidoMessagesAsync(Message message, CancellationToken token)
        {
            var produto = JsonConvert.DeserializeObject<VendaProdutoRequest>(Encoding.UTF8.GetString(message.Body));
            await _produtoAppService.VenderProduto(produto.CodProduto, produto.Quantidade);
            await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            return Task.CompletedTask;
        }
    }
}
