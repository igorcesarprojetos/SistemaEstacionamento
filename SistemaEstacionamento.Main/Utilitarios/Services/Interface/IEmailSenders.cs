namespace SistemaEstacionamento.Main.Utilitarios.Services.Interface
{
    public interface IEmailSenders
    {
        Task<bool> SendEmailAsync(string email, string assunto, string mensagemTexto, string mensagemHTML);
    }
}
