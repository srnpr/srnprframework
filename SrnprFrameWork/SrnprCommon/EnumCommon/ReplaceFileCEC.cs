﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.EnumCommon
{
    /// <summary>
    /// Description: 数据库服务器类型枚举
    /// Author:Liudpc
    /// Create Date: 2010-6-8 10:35:44
    /// </summary>
    public enum DataServerType
    {

        /// <summary>
        /// Description: 微软SqlServer
        /// Author:Liudpc
        /// Create Date: 2010-6-8 10:36:15
        /// </summary>
        MsSql,

    }


    /// <summary>
    /// Description: 服务器范畴枚举
    /// Author:Liudpc
    /// Create Date: 2010-6-8 10:35:57
    /// </summary>
    public enum ServerCategory
    {

        /// <summary>
        /// Description: 测试机
        /// Author:Liudpc
        /// Create Date: 2010-6-8 10:36:23
        /// </summary>
        TestServer,

        /// <summary>
        /// Description: 开发机
        /// Author:Liudpc
        /// Create Date: 2010-6-8 10:36:32
        /// </summary>
        DevServer,

        /// <summary>
        /// Description: 正式机
        /// Author:Liudpc
        /// Create Date: 2010-6-8 10:36:44
        /// </summary>
        FormalServer
    }

}
