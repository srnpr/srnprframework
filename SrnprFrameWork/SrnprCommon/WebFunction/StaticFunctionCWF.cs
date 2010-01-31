using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.WebFunction
{
    class StaticFunctionCWF
    {

        /// <summary>
        /// 
        /// Author:Liudpc
        /// Create Date: 2009-11-26 11:08:36
        /// Description: 判断链接是否http链接
        /// </summary>
        /// <param name="sUrl"></param>
        /// <returns></returns>
        public static bool IsHttpUrl(string sUrl)
        {
            bool bReturn = false;


            if (sUrl.Length>7&& sUrl.ToLower().Substring(0, 7) == "http://")
            {
                bReturn = true;
            }

            return bReturn;

        }
    }
}
