using Daycoval.Solid.Domain.Entidades;
using System;

namespace Daycoval.Solid.Domain.Services
{
    public class GatewayPagamentoService : IDisposable
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public string NomeImpresso { get; set; }
        public decimal Valor { get; set; }
        public int MesExpiracao { get; set; }
        public int AnoExpiracao { get; set; }
        public FormaPagamentoCartao FormaPagamentoCartao { get; set; }

        public void EfetuarPagamento()
        {
            //Não é necessário implementar este método.
        }

        public void Dispose()
        {
            //https://docs.microsoft.com/en-us/visualstudio/code-quality/ca1816-call-gc-suppressfinalize-correctly?view=vs-2017

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Login = string.Empty;
                Senha = string.Empty;
                NomeImpresso = string.Empty;
                Valor = 0M;
                MesExpiracao = 0;
                AnoExpiracao = 0;
            }
        }
    }
}