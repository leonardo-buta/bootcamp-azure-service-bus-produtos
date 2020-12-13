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

        public ProdutoController(IProdutoAppService produtoAppService)
        {
            _produtoAppService = produtoAppService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<GetProdutoRequest>> Get()
        {
            return Ok(_produtoAppService.GetAll());
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

            return Ok(produtoRequest);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(UpdateProdutoRequest), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody][Required] UpdateProdutoRequest updateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _produtoAppService.Update(id, updateRequest);

            return Ok(updateRequest);
        }
    }
}
