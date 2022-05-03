using AutoMapper;
using NerdStore.Catalogo.API.Controllers.ViewModels;
using NerdStore.Catalogo.Domain.Models;

namespace NerdStore.Catalogo.API.Configurations.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProdutoViewModel, Produto>()
                .ConstructUsing(p =>
                    new Produto(p.Nome, p.Descricao, p.Ativo,
                                p.Valor, p.CategoriaId, p.DataCadastro,
                                p.Imagem, new Dimensoes(p.Altura, p.Largura, p.Profundidade)));

            CreateMap<CategoriaViewModel, Categoria>()
                .ConstructUsing(c => new Categoria(c.Nome, c.Codigo));
        }
    }
}
