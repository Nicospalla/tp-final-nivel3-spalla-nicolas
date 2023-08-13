using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace accesoriosSeguridad
{
    public class EmailService
    {
        SmtpClient server;
        MailMessage mail;

        public EmailService()
        {
            server = new SmtpClient();
            server.Credentials = new NetworkCredential ("asp.netspalla@gmail.com", "uxhahjycubstobiy");
            server.EnableSsl = true;
            server.Port = 587;
            server.Host = "smtp.gmail.com";

        }
        public void cuerpoMail (string email, int passProvisional)
        {
            mail = new MailMessage ();
            mail.From = new MailAddress ("asp.netspalla@gmail.com");
            mail.Subject = "Cambio de password";
            mail.To.Add(email);
            mail.Body = "La contraseña provisional es " + passProvisional + "\n Ingresa con esta password y configura una nueva desde tu perfil"; 
            
        }
        public void enviarMail()
        {
            try
            {
                server.Send(mail);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
