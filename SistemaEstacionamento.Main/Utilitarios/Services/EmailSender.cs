using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using SistemaEstacionamento.Main.Models.Configuration;
using SistemaEstacionamento.Main.Utilitarios.Services.Interface;
using System.Net;
using System.Net.Mail;

using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace SistemaEstacionamento.Main.Utilitarios.Services
{
    public class EmailSender : IEmailSenders
    {
        public readonly EmailConfiguration _emailConfiguration;
        
        public EmailSender(IOptions<EmailConfiguration> emailConfiguration)
        {
            _emailConfiguration = emailConfiguration.Value;
                
        }

        public async Task<bool> SendEmailAsync(string email, string assunto, string mensagemTexto, string mensagemHTML)
        {
            // 1. Configuração da Mensagem (MailMessage)
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(_emailConfiguration.EmailRemetente);
            mail.To.Add(email);
            mail.Subject = assunto;
            mail.Body = "<h1>Usuario e Senha</h1>";
            mail.Body += "<br/>";
            mail.Body +=$"<b style ='font-size:16px'>{mensagemTexto}</b><br/></br>";
            mail.Body += mensagemHTML;
            mail.IsBodyHtml = true; // Permite HTML [5]

            // 2. Configuração do Servidor SMTP (SmtpClient)
            SmtpClient smtp = new SmtpClient(_emailConfiguration.EnderecoServidorEmail);
            smtp.Port = _emailConfiguration.PortaServidorEmail; // Porta comum para SMTP com STARTTLS [1]
            smtp.Credentials = new NetworkCredential(_emailConfiguration.EmailRemetente, _emailConfiguration.Senha);
            smtp.EnableSsl = _emailConfiguration.UsarSsl; // Conexão segura [3]

            // 3. Envio
            try
            {
               await smtp.SendMailAsync(mail);
               return true;
            }
            catch (Exception ex)
            {               
                throw new Exception("Erro: " + ex.Message);
               
            }
            finally
            {
                mail.Dispose();
                smtp.Dispose(); // Libera recursos [3]
            }
           

            //var smtp = new SmtpClient(_emailConfiguration.EnderecoServidorEmail, _emailConfiguration.PortaServidorEmail)
            //{
            //    Credentials = new NetworkCredential(_emailConfiguration.EmailRemetente, _emailConfiguration.Senha),
            //    EnableSsl = _emailConfiguration.UsarSsl
            //};

            //var mail = new MailMessage();
            //mail.From = new MailAddress(_emailConfiguration.EmailRemetente);
            //mail.To.Add(email);
            //mail.Subject = "Teste";
            //mail.Body = "Email enviado com sucesso";

            //try
            //{
            //    smtp.Send(mail);
            //}
            //catch ( Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}             
            //finally
            //{
            //    mail.Dispose();
            //    smtp.Dispose();

            //}

        }
        
    }
}
