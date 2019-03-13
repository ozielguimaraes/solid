namespace Daycoval.Solid.Domain.Patterns.Strategy.TipoProduto
{
    public class TipoProdutoSuperfulos : TipoProdutoStrategy
    {
        public override decimal CalcularValorImposto(decimal valorProduto)
        {
            return valorProduto * 0.20M;
        }
    }
}