using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SendEmail
{



    /// <summary>
    /// Description: 邮件设计实体
    /// Author:Liudpc
    /// Create Date: 2010-6-13 11:29:14
    /// </summary>
    public class EmailDesignItem
    {
        public string XmlId { get; set; }


        public string Title { get; set; }


        public string Content { get; set; }

        public string TempleteGuid { get; set; }

        public string ToEmail { get; set; }

        public string RuleExpress { get; set; }
    }
}
