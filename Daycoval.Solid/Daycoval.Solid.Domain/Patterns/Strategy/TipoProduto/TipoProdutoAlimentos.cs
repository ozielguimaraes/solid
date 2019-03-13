namespace Daycoval.Solid.Domain.Patterns.Strategy.TipoProduto
{
    public class TipoProdutoAlimentos : TipoProdutoStrategy
    {
        public override decimal CalcularValorImposto(decimal valorProduto)
        {
            return valorProduto * 0.05M;
        }
    }
}