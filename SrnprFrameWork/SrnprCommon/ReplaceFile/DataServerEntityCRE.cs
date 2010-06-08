using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SrnprCommon.EnumCommon;

namespace SrnprCommon.ReplaceFile
{


    /// <summary>
    /// Description: 数据库服务器实体类
    /// Author:Liudpc
    /// Create Date: 2010-6-8 10:10:09
    /// </summary>
    public class DataServerEntityCRE
    {

        /// <summary>
        /// Description: 编号
        /// Author:Liudpc
        /// Create Date: 2010-6-8 10:17:18
        /// </summary>
        public string Id { get; set; }


        /// <summary>
        /// Description: 数据库服务器类型
        /// Author:Liudpc
        /// Create Date: 2010-6-8 10:37:47
        /// </summary>
        public DataServerType ServerType { get; set; }



        /// <summary>
        /// Description: 服务器范畴
        /// Author:Liudpc
        /// Create Date: 2010-6-8 10:39:18
        /// </summary>
        public ServerCategory Category { get; set; }

    }
}
