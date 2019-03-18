using Daycoval.Solid.Domain.Entidades;
using Daycoval.Solid.Domain.Patterns.Strategy.TipoProduto;
using Xunit;

namespace Daycoval.Solid.Domain.Tests.Patterns.Strategy.TipoProduto
{
    public class TipoProdutoAlimentosTest
    {
        [Fact]
        public void CalculandoValorImpostoAlimentosCorretamente()
        {
            var produto = new Produto
            {
                Descricao = "Arroz tipo 1",
                Quantidade = 1,
                TipoProduto = Entidades.TipoProduto.Alimentos,
                Valor = 15
            };

            var tipoProdutoStrategy = new TipoProdutoAlimentos();
            produto.ValorImposto = tipoProdutoStrategy.CalcularValorImposto(produto.Valor);

            Assert.True(produto.ValorImposto == 0.75m);
        }
    }
}