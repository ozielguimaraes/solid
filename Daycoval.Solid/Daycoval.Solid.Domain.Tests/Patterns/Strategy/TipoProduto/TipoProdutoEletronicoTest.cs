using Daycoval.Solid.Domain.Entidades;
using Daycoval.Solid.Domain.Patterns.Strategy.TipoProduto;
using Xunit;

namespace Daycoval.Solid.Domain.Tests.Patterns.Strategy.TipoProduto
{
  public class TipoProdutoEletronicoTest
    {
        [Fact]
        public void CalculandoValorImpostoEletronicoCorretamente()
        {
            var produto = new Produto
            {
                Descricao = "Iphone X",
                Quantidade = 1,
                TipoProduto = Entidades.TipoProduto.Eletronico,
                Valor = 3650
            };

            var tipoProdutoStrategy = new TipoProdutoEletronico();
            produto.ValorImposto = tipoProdutoStrategy.CalcularValorImposto(produto.Valor);

            Assert.True(produto.ValorImposto == 547.5m);
        }
    }
}