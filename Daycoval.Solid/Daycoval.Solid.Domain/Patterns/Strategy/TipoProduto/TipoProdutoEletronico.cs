namespace Daycoval.Solid.Domain.Patterns.Strategy.TipoProduto
{
    public class TipoProdutoEletronico : TipoProdutoStrategy
    {
        public override decimal CalcularValorImposto(decimal valorProduto)
        {
            return valorProduto * 0.15M;
        }
    }
}