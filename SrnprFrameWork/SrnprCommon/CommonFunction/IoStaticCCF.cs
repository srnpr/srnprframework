using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SrnprCommon.CommonFunction
{

    /// <summary>
    /// Description: IO相关操作
    /// Author:Liudpc
    /// Create Date: 2010-6-30 13:12:36
    /// </summary>
    public static class IoStaticCCF
    {

        /// <summary>
        /// 
        /// Description: 判断目录是否存在 如果不存在则创建
        /// Author:Liudpc
        /// Create Date: 2010-6-30 13:12:32
        /// </summary>
        /// <param name="sDir"></param>
        /// <returns></returns>
        public static bool CheckDirectory(string sDir)
        {

            

            if (!Directory.Exists(sDir))
            {
                Directory.CreateDirectory(sDir);
            }
            return true;
        }


        public static string ReadFileContent(string sFilePath)
        {
            string sReturn = "";
            using (StreamReader sr = new StreamReader(sFilePath, UnicodeEncoding.Unicode))
            {
               sReturn= sr.ReadToEnd();
               sr.Close();
            }
            return sReturn;
        }


       

    }
}
