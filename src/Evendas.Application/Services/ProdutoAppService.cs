using AutoMapper;
using Evendas.Application.Interfaces;
using Evendas.Application.RequestsModel;
using Evendas.Domain.Interfaces;
using Evendas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Evendas.Application.Services
{
    public class ProdutoAppService : IProdutoAppService
    {
        private readonly IMapper _mapper;
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoAppService(IMapper mapper, IProdutoRepository produtoRepository)
        {
            _mapper = mapper;
            _produtoRepository = produtoRepository;
        }

        public async Task Create(CreateProdutoRequest createProductRequest)
        {
            await _produtoRepository.AddAsync(_mapper.Map<Produto>(createProductRequest));
        }

        public async Task Update(UpdateProdutoRequest updateProductRequest)
        {
            var produto = await _produtoRepository.GetByIdAsync(updateProductRequest.Id);
            _mapper.Map(updateProductRequest, produto);
            _produtoRepository.Update(produto);
        }

        public IEnumerable<ProdutoResult> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
