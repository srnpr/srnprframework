using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace SrnprCommon.CommonFunction
{

    /// <summary>
    /// Description: 发送邮件函数
    /// Author:Liudpc
    /// Create Date: 2010-6-30 11:07:02
    /// </summary>
    public class SendEmailCCF
    {




        /// <summary>
        /// 
        /// Description: 发送邮件
        /// Author:Liudpc
        /// Create Date: 2010-6-30 11:06:50
        /// </summary>
        /// <param name="emailInfo"></param>
        /// <param name="emailServer"></param>
        /// <returns></returns>
        public static bool Send(ReplaceFile.DoSendEmailEntityCRF emailInfo,ReplaceFile.ServerEmailEntityCRF emailServer)
        {

            try
            {

               

                MailAddress from = new MailAddress(emailServer.SendMailName, emailServer.SendMailDisplayName);
                MailAddress to = new MailAddress(emailInfo.ToEmail);
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(from, to);

               

                mail.Subject = emailInfo.Title;
                mail.Body = emailInfo.Content;
                mail.BodyEncoding = Encoding.UTF8; ;
                mail.SubjectEncoding = Encoding.UTF8;

                mail.IsBodyHtml = emailServer.IsBodyHtml;

                SmtpClient client = new SmtpClient(emailServer.SmtpHost);

                NetworkCredential smtpuserinfo = new System.Net.NetworkCredential(emailServer.UserName, emailServer.Password);
                client.Credentials = smtpuserinfo;

                client.EnableSsl = emailServer.EnableSsl;



                if (!string.IsNullOrEmpty(emailServer.Port))
                {
                    client.Port = int.Parse(emailServer.Port);
                }


                client.Send(mail);

                emailInfo.SendSuccessFlag = true;
            }
            catch
            {
                emailInfo.SendSuccessFlag = false;
            }

            return emailInfo.SendSuccessFlag;







        }

    }
}
