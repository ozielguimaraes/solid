using Daycoval.Solid.Domain.Services.Interfaces;
using System.Net.Mail;

namespace Daycoval.Solid.Domain.Services
{
    public class EmailService : IEmailService
    {
        public void EnviarEmailNovaCompra(string nomeCliente, string emailCliente)
        {
            //Colocar em um arquivo de configuração
            using (var msg = new MailMessage("tiago.dantas@bancodaycoval.com.br", emailCliente))
            using (var smtp = new SmtpClient("servidor.smtp"))
            {
                msg.Subject = "Dados da sua compra";
                msg.Body = $"Olá {nomeCliente}, Obrigado por efetuar sua compra conosco.";

                smtp.Send(msg);
            }
        }
    }
}