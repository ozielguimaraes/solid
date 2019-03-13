namespace Daycoval.Solid.Domain.Patterns.Strategy.TipoProduto
{
    public abstract class TipoProdutoStrategy
    {
        public abstract decimal CalcularValorImposto(decimal valorProduto);
    }
}