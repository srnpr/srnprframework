using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.ReplaceFile
{

    /// <summary>
    /// Description: 实际发送邮件操作实体
    /// Author:Liudpc
    /// Create Date: 2010-6-11 13:51:08
    /// </summary>
    public class DoSendEmailEntityCRF
    {


        /// <summary>
        /// Description: 发送成功标志
        /// Author:Liudpc
        /// Create Date: 2010-6-11 14:23:56
        /// </summary>
        public bool SendSuccessFlag { get; set; }


        /// <summary>
        /// Description: 模板编号
        /// Author:Liudpc
        /// Create Date: 2010-6-11 13:50:54
        /// </summary>
        public string TempleteId { get; set; }



        /// <summary>
        /// Description: 收件人
        /// Author:Liudpc
        /// Create Date: 2010-6-11 13:50:41
        /// </summary>
        public string ToEmail { get; set; }





        /// <summary>
        /// Description: 邮件标题
        /// Author:Liudpc
        /// Create Date: 2010-6-11 13:54:18
        /// </summary>
        public string Title { get; set; }




        /// <summary>
        /// Description: 邮件内容
        /// Author:Liudpc
        /// Create Date: 2010-6-11 13:54:34
        /// </summary>
        public string Content { get; set; }



        public ServerEmailEntityCRF EmailServer { get; set; }



    }
}
