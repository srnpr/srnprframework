using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.InterFace
{

    /// <summary>
    /// Description: 结果返回基接口
    /// Author:Liudpc
    /// Create Date: 2010-6-30 10:55:51
    /// </summary>
    interface ResultReturnBaseCIF
    {


        /// <summary>
        /// Description: 执行结果标识符
        /// Author:Liudpc
        /// Create Date: 2010-6-30 10:56:20
        /// </summary>
        bool ResultFlag { get; set; }



        /// <summary>
        /// Description: 执行结果返回消息
        /// Author:Liudpc
        /// Create Date: 2010-6-30 10:56:36
        /// </summary>
        string ResultMessage { get; set; }

    }
}
