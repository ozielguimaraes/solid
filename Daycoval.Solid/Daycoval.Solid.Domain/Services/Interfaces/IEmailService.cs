namespace Daycoval.Solid.Domain.Services.Interfaces
{
    public interface IEmailService
    {
        void EnviarEmailNovaCompra(string nomeCliente, string emailCliente);
    }
}