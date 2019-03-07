using Daycoval.Solid.Domain.Services.Interfaces;

namespace Daycoval.Solid.Domain.Services
{
    public class SmsService: ISmsService
    {
        public string Celular { get; private set; }
        public string Mensagem { get; private set; }

        public void EnviarSms(string celular, string mensagem)
        {
            //Este método não precisa ser implementado.
        }
    }
}