using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SrnprCommon.EnumCommon;

namespace SrnprCommon.CommonLog
{



    /// <summary>
    /// Description: 系统级异常完全出错抛出
    /// Author:Liudpc
    /// Create Date: 2010/12/15 16:23:57
    /// </summary>
    public class ExceptionLogCCL
    {



        /// <summary>
        /// 
        /// Description: 抛出异常错误信息
        /// Author:Liudpc
        /// Create Date: 2010/12/15 16:23:44
        /// </summary>
        /// <param name="exc"></param>
        /// <param name="sMessage"></param>
        public static void ThrowException(ExceptionEnum exc, string sMessage)
        {

            Exception e = new Exception("ExceptionEnum:" + exc.ToString() + ";ExceptionMessage:"+sMessage);

            throw e;
        }



    }
}
