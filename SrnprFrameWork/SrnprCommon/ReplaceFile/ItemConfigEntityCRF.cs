using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.ReplaceFile
{

    /// <summary>
    /// Description: xml文件配置信息
    /// Author:Liudpc
    /// Create Date: 2010-6-9 9:38:53
    /// </summary>
    public class ItemConfigEntityCRF
    {

        /// <summary>
        /// Description: 是否可用
        /// Author:Liudpc
        /// Create Date: 2010-6-9 10:02:04
        /// </summary>
        public bool Used { get; set; }


        /// <summary>
        /// Description: 标题
        /// Author:Liudpc
        /// Create Date: 2010-6-9 10:02:00
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        /// Description: 描述信息
        /// Author:Liudpc
        /// Create Date: 2010-6-9 10:01:55
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// Description: 数据库服务器编号
        /// Author:Liudpc
        /// Create Date: 2010-6-9 10:01:33
        /// </summary>
        public string DataServerId { get; set; }


        /// <summary>
        /// Description: 邮件服务器编号
        /// Author:Liudpc
        /// Create Date: 2010-6-9 10:01:26
        /// </summary>
        public string EmailServerId { get; set; }


        /// <summary>
        /// Description: 版本标识符
        /// Author:Liudpc
        /// Create Date: 2010-6-9 10:01:03
        /// </summary>
        public string Version { get; set; }

    }
}
