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
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Repositorys.Models;

namespace ApplicationCore.Services
{
    public class ServiceEmail : IServiceEmail
    {
        private readonly EmailSetting Configuration;

        public ServiceEmail(EmailSetting configuration)
        {
            Configuration = configuration;
        }

        public void SendEmailCreateEvent(EventeInfoEmail evento, string to)
        {
            try
            {
               var templatePath = Path.Combine("~/Contend/HtmlTemplates/EmailConfirmTemplate.html");


                string bodyHtml = "<!DOCTYPE html>\r\n<html lang=\"es\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <style>\r\n        body {\r\n            font-family: Arial, sans-serif;\r\n            background-color: #f4f4f4;\r\n            margin: 0;\r\n            padding: 0;\r\n        }\r\n        .container {\r\n            width: 100%;\r\n            max-width: 600px;\r\n            margin: 0 auto;\r\n            background-color: #f0f0f0;\r\n            padding: 20px;\r\n            border-radius: 10px;\r\n            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);\r\n        }\r\n        .header {\r\n            display: flex;\r\n            justify-content: space-between;\r\n            align-items: center;\r\n        }\r\n        .logo {\r\n            width: 200px;\r\n            height: 100px;\r\n            max-width: 100%;\r\n            max-height: 100%;\r\n        }\r\n        h1 {\r\n            color: #333333;\r\n            text-align: center;\r\n        }\r\n        p {\r\n            color: #666666;\r\n            line-height: 1.6;\r\n        }\r\n        .highlight {\r\n            font-weight: bold;\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"container\">\r\n        <div class=\"header\">\r\n            <img class=\"logo\" id='base64image' src=\"~/Contend/imgs/Logo.jpg\" />\r\n        </div>\r\n        <p>Usted a confirmado la asistencia al evento :<span class=\"highlight\">[NombreEvento]</span> </p>\r\n        <h2>Resumen del evento</h2>\r\n        <p>Se realizara en la fecha: <span class=\"highlight\">$[FechaEvento]</span></p>\r\n        <p>[Descripcion]</p>\r\n    </div>\r\n</body>\r\n</html>\r\n+";

                bodyHtml = bodyHtml.Replace("[NombreEvento]", evento.Nombre)
                                .Replace("[FechaEvento]", evento.Fecha)
                                .Replace("[Descripcion]", evento.Descripcion);

                SendEmail("Confimacion de asistencia a evento", bodyHtml, to);
            }

            catch (Exception ex)
            {
                throw new Exception("Error al enviar al correo: " + "\n" + ex.Message);
            }
        }

        public void SendEmailRegisterEvent(EventeInfoEmail evento, string to)
        {
            try
            {
                // var templatePath = HttpContext.Current.Server.MapPath("~/Contend/HtmlTemplates/EmailConfirmTemplate.html");
                string bodyHtml = "<!DOCTYPE html>\r\n<html lang=\"es\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <style>\r\n        body {\r\n            font-family: Arial, sans-serif;\r\n            background-color: #f4f4f4;\r\n            margin: 0;\r\n            padding: 0;\r\n        }\r\n        .container {\r\n            width: 100%;\r\n            max-width: 600px;\r\n            margin: 0 auto;\r\n            background-color: #f0f0f0;\r\n            padding: 20px;\r\n            border-radius: 10px;\r\n            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);\r\n        }\r\n        .header {\r\n            display: flex;\r\n            justify-content: space-between;\r\n            align-items: center;\r\n        }\r\n        .logo {\r\n            width: 200px;\r\n            height: 100px;\r\n            max-width: 100%;\r\n            max-height: 100%;\r\n        }\r\n        h1 {\r\n            color: #333333;\r\n            text-align: center;\r\n        }\r\n        p {\r\n            color: #666666;\r\n            line-height: 1.6;\r\n        }\r\n        .highlight {\r\n            font-weight: bold;\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"container\">\r\n       \r\n        <p>Usted a confirmado la asistencia al evento :<span class=\"highlight\">[NombreEvento]</span> </p>\r\n        <h2>Resumen del evento</h2>\r\n        <p>Se realizara en la fecha: <span class=\"highlight\">[FechaEvento]</span></p>\r\n        <p>[Descripcion]</p>\r\n    </div>\r\n</body>\r\n</html>\r\n+";

                bodyHtml = bodyHtml.Replace("[NombreEvento]", evento.Nombre)
                                .Replace("[FechaEvento]", evento.Fecha)
                                .Replace("[Descripcion]", evento.Descripcion);

                SendEmail("Invitacion a nuestro nuevo evento", bodyHtml, to);
            }

            catch (Exception ex)
            {
                throw new Exception("Error al enviar al correo: " + "\n" + ex.Message);
            }
        }

        private void SendEmail(string subject, string body, string to)
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
