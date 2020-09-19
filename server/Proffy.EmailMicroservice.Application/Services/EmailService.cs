using System;
using System.Net;
using System.Net.Mail;

namespace Proffy.EmailMicroservice.Application.Services
{
    public static class EmailService
    {
        public static void SendEmail(string recipient)
        {
            MailMessage _mailMessage = new MailMessage();

            _mailMessage.From = new MailAddress(recipient);

            _mailMessage.CC.Add(recipient);
            _mailMessage.Subject = "Bem vindo ao Proffy!";
            _mailMessage.IsBodyHtml = true;
            _mailMessage.Body = "<b>Conta criada com sucesso!</b><p>Você já pode se conectar ao Proffy. <br/><br/> Caso não tenha feito nenhum cadastro, favor desconsiderar este e-mail.</p>";

            SmtpClient _smtpClient = new SmtpClient("smtp.live.com", Convert.ToInt32("587"));

            // Config without port
            // SmtpClient _smtpClient = new SmtpClient(UtilRsource.ConfigSmtp);

            _smtpClient.UseDefaultCredentials = false;
            _smtpClient.Credentials = new NetworkCredential(Settings.Email, Settings.Password);
            _smtpClient.EnableSsl = true;
            _smtpClient.Send(_mailMessage);
        }
    }
}
