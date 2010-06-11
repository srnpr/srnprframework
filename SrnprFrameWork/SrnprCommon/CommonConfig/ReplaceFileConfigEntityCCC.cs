using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.CommonConfig
{



    /// <summary>
    /// Description: 替换文件配置
    /// Author:Liudpc
    /// Create Date: 2010-6-11 13:30:17
    /// </summary>
    public class ReplaceFileConfigEntityCCC
    {



        /// <summary>
        /// Description: 数据库实体组
        /// Author:Liudpc
        /// Create Date: 2010-6-11 13:22:28
        /// </summary>
        public List<ReplaceFile.ServerDatabaseEntityCRF> DataServerList { get; set; }


        /// <summary>
        /// Description: 邮件服务器实体组
        /// Author:Liudpc
        /// Create Date: 2010-6-11 13:22:37
        /// </summary>
        public List<ReplaceFile.ServerEmailEntityCRF> EmailServerList { get; set; }



        /// <summary>
        /// Description: 分割字符串
        /// Author:Liudpc
        /// Create Date: 2010-6-11 13:29:30
        /// </summary>
        public string SplitString { get; set; }


        /// <summary>
        /// Description: 内容替换参数开始符
        /// Author:Liudpc
        /// Create Date: 2010-6-11 13:29:43
        /// </summary>
        public string ReplaceFrom { get; set; }



        /// <summary>
        /// Description: 主替换参数模板标记
        /// Author:Liudpc
        /// Create Date: 2010-6-11 13:30:00
        /// </summary>
        public string MainParmReplace { get; set; }



        /// <summary>
        /// Description: XML文件目录
        /// Author:Liudpc
        /// Create Date: 2010-6-11 13:22:06
        /// </summary>
        public string XmlFileDirectory { get; set; }




        /// <summary>
        /// Description: 代码文件名称后缀
        /// Author:Liudpc
        /// Create Date: 2010-6-11 14:44:55
        /// </summary>
        public string CodeFileApp { get; set; }


        /// <summary>
        /// Description: 设计文件名称后缀
        /// Author:Liudpc
        /// Create Date: 2010-6-11 14:44:37
        /// </summary>
        public string DesignFileApp { get; set; }

    }
}
