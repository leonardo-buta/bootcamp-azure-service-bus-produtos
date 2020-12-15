using Evendas.Application.Interfaces;
using Evendas.Application.RequestsModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace EVendas.API.ModuloEstoque.Controllers
{
    [Route("api/estoque/[controller]")]
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
        public ActionResult<List<GetProdutoRequest>> GetAll()
        {
            return Ok(_produtoAppService.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            return Ok(await _produtoAppService.GetByIdAsync(id));
        }

        [HttpGet]
        [Route("codProduto/{codProduto}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByCodProdutoAsync(string codProduto)
        {
            return Ok(await _produtoAppService.GetByCodProdutoAsync(codProduto));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateProdutoRequest), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CreateProdutoRequest), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromBody][Required] CreateProdutoRequest produtoRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _produtoAppService.Create(produtoRequest);
            await _serviceBusSender.SendCreateProdutoMessage(produtoRequest);

            return Ok(produtoRequest);
        }

        [HttpPut]
        [Route("{codProduto}")]
        [ProducesResponseType(typeof(UpdateProdutoRequest), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(string codProduto, [FromBody][Required] UpdateProdutoRequest updateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _produtoAppService.Update(codProduto, updateRequest);
            await _serviceBusSender.SendUpdateProdutoMessage(codProduto, updateRequest);

            return Ok(updateRequest);
        }
    }
}
