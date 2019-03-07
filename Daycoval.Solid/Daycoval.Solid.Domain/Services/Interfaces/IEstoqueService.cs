using Daycoval.Solid.Domain.Entidades;

namespace Daycoval.Solid.Domain.Services.Interfaces
{
    public interface IEstoqueService
    {
        void SolicitarProduto(Produto produto);
        void BaixarEstoque(Produto produto);
    }
}