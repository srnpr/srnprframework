using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SendEmail
{
    public class ResultSendEmail:SrnprCommon.ReplaceFile.ResultReplaceEntityIfCRF
    {
        #region ResultReplaceEntityIfCRF 成员

        public bool ReplaceResultFlag
        {
            get;
            set;
        }

        #endregion




        /// <summary>
        /// Description: 发送状态（1：发送成功，2：部分发送成功，3：发送失败）
        /// Author:Liudpc
        /// Create Date: 2010-6-11 13:38:54
        /// </summary>
        public int SendState { get; set; }
    }
}
