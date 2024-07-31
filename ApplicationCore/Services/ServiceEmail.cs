using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApplicationCore.Utils;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;

namespace ApplicationCore.Services
{
    public class ServiceEmail : IServiceEmail
    {
        private readonly EmailSetting Configuration;

        public ServiceEmail(EmailSetting configuration)
        {
            Configuration = configuration;
        }

        public void SendEmail(string subject, string body, string to)
        {
            try
            {
                var fromEmail = "asomameco12@gmail.com";
                var password= "ijonexbpzqdewpjj";

                var message = new MailMessage();

                message.From = new MailAddress( fromEmail);
                message.Subject = subject;
                message.To.Add(new MailAddress(to));
                message.Body = body;
                message.IsBodyHtml = true;

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {

                    Port = 587,
                    Credentials = new NetworkCredential(fromEmail, password),
                    EnableSsl=true
                    
                };


                smtpClient.Send(message);
            }

            catch (Exception ex)
            {
                throw new Exception("Error al enviar al correo: "+"\n"+ex.Message);
            }
        }
    }
}
