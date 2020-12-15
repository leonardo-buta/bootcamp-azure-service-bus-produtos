using Evendas.Application.Interfaces;
using Evendas.Application.RequestsModel;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Evendas.Application.ServiceBus
{
    public class ServiceBusSender : IServiceBusSender
    {
        private readonly IConfiguration _configuration;
        private TopicClient _topicClient;

        public ServiceBusSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendCreateProdutoMessage(CreateProdutoRequest request)
        {
            _topicClient = BuildTopicClient("produtocriado");
            await SendMesage(request);
        }

        public async Task SendUpdateProdutoMessage(string codProduto, UpdateProdutoRequest request)
        {
            _topicClient = BuildTopicClient("produtoeditado");
            request.CodProduto = codProduto;
            await SendMesage(request);
        }

        public async Task SendProdutoVendidoMessage(string codProduto, VendaProdutoRequest request)
        {
            _topicClient = BuildTopicClient("produtovendido");
            request.CodProduto = codProduto;
            await SendMesage(request);
        }

        private TopicClient BuildTopicClient(string topic)
        {
            return new TopicClient(_configuration.GetConnectionString("ServiceBusConnectionString"), topic);
        }

        private async Task SendMesage(object request)
        {
            string data = JsonConvert.SerializeObject(request);
            Message message = new Message(Encoding.UTF8.GetBytes(data))
            {
                ContentType = "application/json"
            };
            message.UserProperties.Add("CorrelationId", Guid.NewGuid().ToString());

            await _topicClient.SendAsync(message);
        }
    }
}
