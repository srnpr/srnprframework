using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.BaseEntity
{

    /// <summary>
    /// Description: 通用执行返回结果标识
    /// Author:Liudpc
    /// Create Date: 2010-6-30 11:04:22
    /// </summary>
    public class ResultReturnEntityCBE:InterFace.ResultReturnBaseCIF
    {
        #region ResultReturnBaseCIF 成员

        /// <summary>
        /// Description: 执行结果 是否成功
        /// Author:Liudpc
        /// Create Date: 2010-6-30 11:04:41
        /// </summary>
        public bool ResultFlag
        {
            get;
            set;
        }


        /// <summary>
        /// Description: 返回的消息
        /// Author:Liudpc
        /// Create Date: 2010-6-30 11:04:53
        /// </summary>
        public string ResultMessage
        {
            get;
            set;
        }

        #endregion
    }
}
