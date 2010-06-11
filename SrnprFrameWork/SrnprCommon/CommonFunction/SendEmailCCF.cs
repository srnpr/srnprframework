using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace SrnprCommon.CommonFunction
{
    public class SendEmailCCF
    {




        public bool Send(string sToEmail, string sTitle, string sContent, ReplaceFile.ServerEmailEntityCRF server)
        {

            try
            {



                MailAddress from = new MailAddress(server.SendMailName, server.SendMailDisplayName);
                MailAddress to = new MailAddress(sToEmail);
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(from, to);

               

                mail.Subject = sTitle;
                mail.Body = sContent;
                mail.BodyEncoding = Encoding.UTF8; ;
                mail.SubjectEncoding = Encoding.UTF8;
               
                mail.IsBodyHtml = server.IsBodyHtml;

                SmtpClient client = new SmtpClient(server.SmtpHost);

                NetworkCredential smtpuserinfo = new System.Net.NetworkCredential(server.UserName, server.Password);
                client.Credentials = smtpuserinfo;

                client.EnableSsl = server.EnableSsl;
               


                if (!string.IsNullOrEmpty(server.Port))
                {
                    client.Port = int.Parse(server.Port);
                }


                client.Send(mail);

                return true;
            }
            catch
            {
                return false;
            }







        }

    }
}
