using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.CommonFunction
{



    /// <summary>
    /// Description: 执行函数类
    /// Author:Liudpc
    /// Create Date: 2010-6-11 9:28:47
    /// </summary>
    public class EvalFunctionCCF
    {


        /// <summary>
        /// 
        /// Description: 执行表达式
        /// Author:Liudpc
        /// Create Date: 2010-6-11 9:28:56
        /// </summary>
        /// <param name="sExpress">表达式</param>
        /// <returns>执行结果</returns>
        public static string Eval(string sExpress)
        {
           
            object ret;
            try
            {
                ret = Microsoft.JScript.Eval.JScriptEvaluate(sExpress, Microsoft.JScript.Vsa.VsaEngine.CreateEngine());
            }
            catch (Exception e)
            {
                ret = e.Message;
            }
            return ret.ToString().Trim();
        }
    }
}
