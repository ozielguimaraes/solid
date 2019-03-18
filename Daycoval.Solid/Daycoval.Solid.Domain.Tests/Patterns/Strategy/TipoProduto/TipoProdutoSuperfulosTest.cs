using Daycoval.Solid.Domain.Entidades;
using Daycoval.Solid.Domain.Patterns.Strategy.TipoProduto;
using Xunit;

namespace Daycoval.Solid.Domain.Tests.Patterns.Strategy.TipoProduto
{
   public  class TipoProdutoSuperfulosTest
    {
        [Fact]
        public void CalculandoValorImpostoSuperfulosCorretamente()
        {
            var produto = new Produto
            {
                Descricao = "Headphone megapower",
                Quantidade = 1,
                TipoProduto = Entidades.TipoProduto.Superfulos,
                Valor = 999
            };

            var tipoProdutoStrategy = new TipoProdutoSuperfulos();
            produto.ValorImposto = tipoProdutoStrategy.CalcularValorImposto(produto.Valor);

            Assert.True(produto.ValorImposto == 199.8m);
        }
    }
}