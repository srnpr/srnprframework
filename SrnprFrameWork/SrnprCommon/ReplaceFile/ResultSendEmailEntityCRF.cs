﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.ReplaceFile
{
    public class ResultSendEmailEntityCRF:ResultReplaceEntityIfCRF
    {
        #region ResultReplaceEntityIfCRF 成员

        public bool ReplaceResultFlag
        {
            get;
            set;
        }

        #endregion




        /// <summary>

        /// Description: 发送状态
        /// Author:Liudpc
        /// Create Date: 2010-6-11 13:38:54
        /// </summary>
        public int SendState { get; set; }
    }
}
