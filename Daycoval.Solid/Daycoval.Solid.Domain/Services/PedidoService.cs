using System;
using System.Net.Mail;
using System.Runtime.InteropServices;
using Daycoval.Solid.Domain.Entidades;
using Daycoval.Solid.Domain.Services.Interfaces;

namespace Daycoval.Solid.Domain.Services
{
    public class PedidoService : IPedidoService
    {
        public void EfetuarPedido(Carrinho carrinho, DetalhePagamento detalhePagamento, bool notificarClienteEmail,
            bool notificarClienteSms)
        {
            foreach (var produto in carrinho.Produtos)
            {
                if (produto.TipoProduto == TipoProduto.Alimentos)
                {
                    produto.ValorImposto = produto.Valor * 0.05M;
                    carrinho.ValorTotalPedido += (produto.Valor + produto.ValorImposto) * produto.Quantidade;
                }
                else
                {
                    if (produto.TipoProduto == TipoProduto.Eletronico)
                    {
                        produto.ValorImposto = produto.Valor * 0.15M;
                        carrinho.ValorTotalPedido += (produto.Valor + produto.ValorImposto) * produto.Quantidade;
                    }
                    else
                    {
                        if (produto.TipoProduto == TipoProduto.Superfulos)
                        {
                            produto.ValorImposto = produto.Valor * 0.20M;
                            carrinho.ValorTotalPedido += (produto.Valor + produto.ValorImposto) * produto.Quantidade;
                        }
                        else
                        {
                            throw new ArgumentException("O tipo de produto informado não está disponível.");
                        }
                    }
                }
            }

            if (detalhePagamento.FormaPagamento.Equals(FormaPagamento.CartaoCredito) ||
                detalhePagamento.FormaPagamento.Equals(FormaPagamento.CartaoDebito))
            {
                using (var gatewayPagamento = new GatewayPagamentoService())
                {
                    gatewayPagamento.Login = "login";
                    gatewayPagamento.Senha = "senha";
                    gatewayPagamento.FormaPagamentoCartao = (FormaPagamentoCartao) detalhePagamento.FormaPagamento;
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
                    using (var msg = new MailMessage("tiago.dantas@bancodaycoval.com.br", carrinho.Cliente.Email))
                    using (var smtp = new SmtpClient("servidor.smtp"))
                    {
                        msg.Subject = "Dados da sua compra";
                        msg.Body = $"Obrigado por efetuar sua compra conosco.";

                        smtp.Send(msg);
                    }
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