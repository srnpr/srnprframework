using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SendEmail
{


    /// <summary>
    /// Description: 邮件设计信息
    /// Author:Liudpc
    /// Create Date: 2010-6-13 11:29:25
    /// </summary>
    public class EmailDesignInfo
    {

        public string Title { get; set; }



        public string Parms { get; set; }



        public string StateTitle { get; set; }

        public string[] StateValue { get; set; }



        public List<EmailDesignItem> ListItem { get; set; }

    }
}
