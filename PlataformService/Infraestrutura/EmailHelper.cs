using Helpes.Infraestrutura;
using Modelos.Infra;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PlataformSrvice.Infraestrutura
{
    internal class EmailHelper
    {
        private Email email = new Email();
        private string mailFrom = ConfigurationManager.AppSettings["EmailFrom"].ToString();
        private string host = ConfigurationManager.AppSettings["Host"].ToString();
        private string SenhaEmail = ConfigurationManager.AppSettings["SenhaEmail"].ToString();
        private int hostPort = Convert.ToInt32(ConfigurationManager.AppSettings["HostPort"].ToString());
        private string cCEmail = ConfigurationManager.AppSettings["CCEmail"].ToString();


        public EmailHelper(Email _email)
        {
            email = _email;
        }

        public void Send()
        {



            MailMessage message = new MailMessage();
            message.From = new MailAddress(mailFrom);
            message.To.Add(new MailAddress(email.To));
            message.Subject = email.Subject;
            message.Body = email.Body;
            message.IsBodyHtml = true;


            if (email.Anexos != null)
            {
                for (int i = 0; i < email.Anexos.Count; i++)
                {
                    message.Attachments.Add(email.Anexos[i]);
                }
            }

            if (!string.IsNullOrEmpty(cCEmail))
            {
                message.CC.Add(cCEmail);
            }


            SmtpClient smtp = new SmtpClient();
            smtp.Host = host;
            smtp.Port = hostPort;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(mailFrom, SenhaEmail);           
            
            try
            {
                smtp.Send(message);
            }
            catch (SmtpException ex)
            {
                EventoLog.RegisterAsync("WorkflowService", ex.Message);
            }

        }
    }


}

