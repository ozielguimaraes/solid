using Daycoval.Solid.Domain.Entidades;

namespace Daycoval.Solid.Domain.Services.Interfaces
{
    public interface IPedidoService
    {
        void EfetuarPedido(Carrinho carrinho, DetalhePagamento detalhePagamento, bool notificarClienteEmail,
               bool notificarClienteSms);
    }
}