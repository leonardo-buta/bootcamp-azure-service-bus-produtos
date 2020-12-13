using Evendas.Application.Interfaces;
using Evendas.Application.RequestsModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Evendas.API.ModuloVendas.Controllers
{
    [Route("api/vendas/[controller]")]
    public class ProdutoController : Controller
    {
        private readonly IProdutoAppService _produtoAppService;

        public ProdutoController(IProdutoAppService produtoAppService)
        {
            _produtoAppService = produtoAppService;
        }

        [HttpGet]
        [Route("Listagem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<GetProdutoRequest>> GetAllWithStock()
        {
            return Ok(_produtoAppService.GetAllWithStock());
        }

        [HttpPost]
        [Route("Venda/codProduto/{codProduto}/quantidade/{quantidade}")]
        [ProducesResponseType(typeof(UpdateProdutoRequest), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Venda(string codProduto, int quantidade)
        {
            await _produtoAppService.VenderProduto(codProduto, quantidade);
            return Ok();
        }
    }
}
