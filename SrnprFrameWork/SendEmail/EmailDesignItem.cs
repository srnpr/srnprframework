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


        /// <summary>
        /// Description: XML文件编号
        /// Author:Liudpc
        /// Create Date: 2010-6-21 15:21:30
        /// </summary>
        public string XmlId { get; set; }


        /// <summary>
        /// Description: 邮件标题
        /// Author:Liudpc
        /// Create Date: 2010-6-21 15:21:40
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        /// Description: 邮件内容
        /// Author:Liudpc
        /// Create Date: 2010-6-21 15:21:46
        /// </summary>
        public string Content { get; set; }


        /// <summary>
        /// Description: 模板编号
        /// Author:Liudpc
        /// Create Date: 2010-6-21 15:21:59
        /// </summary>
        public string TempleteGuid { get; set; }


        /// <summary>
        /// Description: 收件人
        /// Author:Liudpc
        /// Create Date: 2010-6-21 15:22:06
        /// </summary>
        public string ToEmail { get; set; }


        /// <summary>
        /// Description: 规则表达式
        /// Author:Liudpc
        /// Create Date: 2010-6-21 15:22:16
        /// </summary>
        public string RuleExpress { get; set; }
    }
}
