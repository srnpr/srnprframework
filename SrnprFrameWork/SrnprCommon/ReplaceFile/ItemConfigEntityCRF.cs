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
        /// Description: 快速设定状态标识符
        /// Author:Liudpc
        /// Create Date: 2010-6-13 11:18:50
        /// </summary>
        public string StateSql { get; set; }


        /// <summary>
        /// Description: 版本标识符
        /// Author:Liudpc
        /// Create Date: 2010-6-9 10:01:03
        /// </summary>
        public string Version { get; set; }



        /// <summary>
        /// Description: XML文件的唯一编号
        /// Author:Liudpc
        /// Create Date: 2010-7-1 9:48:56
        /// </summary>
        public string XmlGuid { get; set; }


        /// <summary>
        /// Description: Xml文件名称
        /// Author:Liudpc
        /// Create Date: 2010-7-1 9:51:19
        /// </summary>
        public string XmlFileId { get; set; }


        /// <summary>
        /// Description: 创建时间
        /// Author:Liudpc
        /// Create Date: 2010-7-1 9:51:48
        /// </summary>
        public string CreateDate { get; set; }


        /// <summary>
        /// Description: 最后一次修改时间
        /// Author:Liudpc
        /// Create Date: 2010-7-1 9:52:05
        /// </summary>
        public string UpdateDate { get; set; }



    }
}
