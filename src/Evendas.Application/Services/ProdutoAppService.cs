using AutoMapper;
using Evendas.Application.Interfaces;
using Evendas.Application.RequestsModel;
using Evendas.Domain.Interfaces;
using Evendas.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evendas.Application.Services
{
    public class ProdutoAppService : IProdutoAppService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoAppService(IMapper mapper, IUnitOfWork uow, IProdutoRepository produtoRepository)
        {
            _mapper = mapper;
            _uow = uow;
            _produtoRepository = produtoRepository;
        }

        public async Task<GetProdutoRequest> GetByIdAsync(long id)
        {
            return _mapper.Map<GetProdutoRequest>(await _produtoRepository.GetByIdAsync(id));
        }

        public async Task<GetProdutoRequest> GetByCodProdutoAsync(string codProduto)
        {
            return _mapper.Map<GetProdutoRequest>(_produtoRepository.GetAll().Where(x => x.CodProduto.ToUpper() == codProduto.ToUpper()).FirstOrDefault());
        }

        public async Task Create(CreateProdutoRequest createProductRequest)
        {
            if (createProductRequest.Preco > 0 && createProductRequest.QtdEstoque > 0)
            {
                var exists = await _produtoRepository.ExistsAsync(x => x.CodProduto.ToUpper() == createProductRequest.CodProduto.ToUpper() ||
                                                                       x.Nome.ToUpper() == createProductRequest.Nome.ToUpper());
                if (!exists)
                {
                    await _produtoRepository.AddAsync(_mapper.Map<Produto>(createProductRequest));
                    await _uow.CommitAsync();
                }
            }
        }

        public async Task Update(long id, UpdateProdutoRequest updateProductRequest)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);
            _mapper.Map(updateProductRequest, produto);
            _produtoRepository.Update(produto);
            await _uow.CommitAsync();
        }

        public async Task VenderProduto(string codProduto, int quantidade)
        {
            var produto = _produtoRepository.GetAll().Where(x => x.CodProduto.ToUpper() == codProduto.ToUpper()).FirstOrDefault();

            if (produto.QtdEstoque - quantidade >= 0)
            {
                produto.QtdEstoque -= quantidade;
                _produtoRepository.Update(produto);
                await _uow.CommitAsync();
            }
        }

        public IEnumerable<GetProdutoRequest> GetAll()
        {
            return _mapper.Map<List<GetProdutoRequest>>(_produtoRepository.GetAll());
        }

        public IEnumerable<GetProdutoRequest> GetAllWithStock()
        {
            return _mapper.Map<List<GetProdutoRequest>>(_produtoRepository.GetAll().Where(x => x.QtdEstoque > 0));
        }
    }
}
