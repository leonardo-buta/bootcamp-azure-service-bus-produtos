using AutoMapper;
using Evendas.Application.RequestsModel;
using Evendas.Domain.Models;

namespace Evendas.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateProdutoRequest, Produto>();
            CreateMap<UpdateProdutoRequest, Produto>();
            CreateMap<Produto, GetProdutoRequest>().ReverseMap();
        }
    }
}
