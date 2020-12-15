using Evendas.Application.Interfaces;
using Evendas.Application.RequestsModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Evendas.API.ModuloVendas.Controllers
{
    [Route("api/vendas/[controller]")]
    public class ProdutoController : Controller
    {
        private readonly IProdutoAppService _produtoAppService;
        private readonly IServiceBusSender _serviceBusSender;

        public ProdutoController(IProdutoAppService produtoAppService, IServiceBusSender serviceBusSender)
        {
            _produtoAppService = produtoAppService;
            _serviceBusSender = serviceBusSender;
        }

        [HttpGet]
        [Route("Listagem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<GetProdutoRequest>> GetAllWithStock()
        {
            return Ok(_produtoAppService.GetAllWithStock());
        }

        [HttpPost]
        [Route("Venda/codProduto/{codProduto}")]
        [ProducesResponseType(typeof(UpdateProdutoRequest), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Venda(string codProduto, [FromBody][Required]VendaProdutoRequest vendaProdutoRequest)
        {
            await _produtoAppService.VenderProduto(codProduto, vendaProdutoRequest.Quantidade);
            await _serviceBusSender.SendProdutoVendidoMessage(codProduto, vendaProdutoRequest);
            return Ok();
        }
    }
}
