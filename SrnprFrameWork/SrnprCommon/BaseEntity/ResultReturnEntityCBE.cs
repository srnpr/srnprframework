using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.BaseEntity
{
    public class ResultReturnEntityCBE:InterFace.ResultReturnBaseCIF
    {
        #region ResultReturnBaseCIF 成员

        public bool ResultFlag
        {
            get;
            set;
        }

        public string ResultMessage
        {
            get;
            set;
        }

        #endregion
    }
}
