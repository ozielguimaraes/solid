using Daycoval.Solid.Domain.Entidades;
using Daycoval.Solid.Domain.Patterns.Strategy.TipoProduto;
using Daycoval.Solid.Domain.Services.Interfaces;
using System.Runtime.InteropServices;

namespace Daycoval.Solid.Domain.Services
{
    public class PedidoService : IPedidoService
    {
        public void EfetuarPedido(Carrinho carrinho, DetalhePagamento detalhePagamento, bool notificarClienteEmail,
            bool notificarClienteSms)
        {
            TipoProdutoStrategy tipoProdutoStrategy;
            foreach (var produto in carrinho.Produtos)
            {
                if (produto.TipoProduto == TipoProduto.Alimentos)
                    tipoProdutoStrategy = new TipoProdutoAlimentos();

                else if (produto.TipoProduto == TipoProduto.Eletronico)
                    tipoProdutoStrategy = new TipoProdutoEletronico();

                else tipoProdutoStrategy = new TipoProdutoSuperfulos();

                produto.ValorImposto = tipoProdutoStrategy.CalcularValorImposto(produto.Valor);
                carrinho.RecalcularTotalPedido(produto);
            }

            if (detalhePagamento.FormaPagamento.Equals(FormaPagamento.CartaoCredito) ||
                detalhePagamento.FormaPagamento.Equals(FormaPagamento.CartaoDebito))
            {
                using (var gatewayPagamento = new GatewayPagamentoService())
                {
                    gatewayPagamento.Login = "login"; //Colocar em um arquivo de configuração
                    gatewayPagamento.Senha = "senha"; //Colocar em um arquivo de configuração
                    gatewayPagamento.FormaPagamentoCartao = (FormaPagamentoCartao)detalhePagamento.FormaPagamento;
                    gatewayPagamento.NomeImpresso = detalhePagamento.NomeImpressoCartao;
                    gatewayPagamento.AnoExpiracao = detalhePagamento.AnoExpiracao;
                    gatewayPagamento.MesExpiracao = detalhePagamento.MesExpiracao;
                    gatewayPagamento.Valor = carrinho.ValorTotalPedido;

                    gatewayPagamento.EfetuarPagamento();
                }

                InformarPagamento(carrinho);
            }

            if (detalhePagamento.FormaPagamento.Equals(FormaPagamento.Dinheiro))
            {
                InformarPagamento(carrinho);
            }

            var estoque = new EstoqueService();

            if (carrinho.FoiPago)
            {
                foreach (var produto in carrinho.Produtos)
                {
                    estoque.SolicitarProduto(produto);
                }

                EntregarProdutos(carrinho);
            }
            else
            {
                throw new ExternalException("O pagamento não foi efetuado.");
            }

            if (carrinho.FoiEntregue)
            {
                foreach (var produto in carrinho.Produtos)
                {
                    estoque.BaixarEstoque(produto);
                }
            }
            else
            {
                throw new ExternalException("Os produtos não foram entregues.");
            }

            if (notificarClienteEmail)
            {
                if (!string.IsNullOrWhiteSpace(carrinho.Cliente.Email))
                {
                    var emailService = new EmailService();
                    emailService.EnviarEmailNovaCompra(carrinho.Cliente.Nome, carrinho.Cliente.Email);
                }
            }

            if (notificarClienteSms)
            {
                if (!string.IsNullOrWhiteSpace(carrinho.Cliente.Celular))
                {
                    var smsService = new SmsService();
                    smsService.EnviarSms(carrinho.Cliente.Celular, "Obrigado por sua compra");
                }
            }
        }

        private void EntregarProdutos(Carrinho carrinho)
        {
            carrinho.FoiEntregue = true;
        }

        private void InformarPagamento(Carrinho carrinho)
        {
            carrinho.FoiPago = true;
        }
    }
}