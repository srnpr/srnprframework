using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprWeb.CommonFunction
{

    /// <summary>
    /// Author:Liudpc
    /// Create Date: 2010-9-2 14:04:48
    /// Description: JS生成帮助
    /// </summary>
    public class JSHelperWCF
    {



        /// <summary>
        /// 
        /// Author:Liudpc
        /// Create Date: 2010-9-2 14:14:46
        /// Description: 添加页面加载完成时执行的脚本
        /// </summary>
        /// <param name="sScript"></param>
        /// <returns></returns>
        public static string CreateScriptDefer(string sScript)
        {
            return "<script  type=\"text/javascript\" defer=\"defer\">" + sScript + "</script>";

        }


    }
}
