using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.ReplaceFile
{




    /// <summary>
    /// Description: 文件替换实体
    /// Author:Liudpc
    /// Create Date: 2010-6-10 11:28:33
    /// </summary>
    public class ReplaceFileEntityCRF
    {



        /// <summary>
        /// Description: 模板实体
        /// Author:Liudpc
        /// Create Date: 2010-6-10 11:27:55
        /// </summary>
        public TempleteXmlEntityCRF TempleteXml { get; set; }




        /// <summary>
        /// Description: 输入参数及值
        /// Author:Liudpc
        /// Create Date: 2010-6-10 11:27:46
        /// </summary>
        public string ReplaceParms { get; set; }



        /// <summary>
        /// Description: 替换唯一编号
        /// Author:Liudpc
        /// Create Date: 2010-6-10 11:27:30
        /// </summary>
        public string ReplaceFileId { get; set; }


        public ServerDatabaseEntityCRF DataServer { get; set; }

    }
}
