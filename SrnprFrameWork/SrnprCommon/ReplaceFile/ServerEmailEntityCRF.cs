using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.ReplaceFile
{
    public class ServerEmailEntityCRF
    {

        public string Id { get; set; }



        public string SendMailName { get; set; }


        public string SendMailDisplayName { get; set; }



        public bool IsBodyHtml { get; set; }


        public string SmtpHost { get; set; }


        public string UserName { get; set; }

        public string Password { get; set; }



        public bool EnableSsl { get; set; }

        public string Port { get; set; }

    }
}
