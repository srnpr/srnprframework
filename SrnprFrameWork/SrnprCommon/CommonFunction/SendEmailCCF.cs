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




        public bool Send(ReplaceFile.DoSendEmailEntityCRF emailInfo)
        {

            try
            {



                MailAddress from = new MailAddress(emailInfo.EmailServer.SendMailName, emailInfo.EmailServer.SendMailDisplayName);
                MailAddress to = new MailAddress(emailInfo.ToEmail);
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(from, to);

               

                mail.Subject = emailInfo.Title;
                mail.Body = emailInfo.Content;
                mail.BodyEncoding = Encoding.UTF8; ;
                mail.SubjectEncoding = Encoding.UTF8;

                mail.IsBodyHtml = emailInfo.EmailServer.IsBodyHtml;

                SmtpClient client = new SmtpClient(emailInfo.EmailServer.SmtpHost);

                NetworkCredential smtpuserinfo = new System.Net.NetworkCredential(emailInfo.EmailServer.UserName, emailInfo.EmailServer.Password);
                client.Credentials = smtpuserinfo;

                client.EnableSsl = emailInfo.EmailServer.EnableSsl;



                if (!string.IsNullOrEmpty(emailInfo.EmailServer.Port))
                {
                    client.Port = int.Parse(emailInfo.EmailServer.Port);
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
