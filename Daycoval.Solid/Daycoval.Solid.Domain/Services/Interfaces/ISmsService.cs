namespace Daycoval.Solid.Domain.Services.Interfaces
{
    public interface ISmsService
    {
        string Celular { get; }
        void EnviarSms(string celular, string mensagem);
    }
}