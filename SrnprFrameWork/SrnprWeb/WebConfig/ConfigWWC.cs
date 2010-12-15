using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprWeb.WebConfig
{
    public class ConfigWWC
    {
      

        public static string WebConfigPath = "";


        /// <summary>
        /// 
        /// Description: 得到配置文件夹
        /// Author:Liudpc
        /// Create Date: 2010/12/15 16:32:05
        /// </summary>
        /// <returns></returns>
        private static string GetWebConfigPath()
        {

            string sDir = "";

            if (!string.IsNullOrEmpty(System.Web.Configuration.WebConfigurationManager.AppSettings[ConstStatic.WebConfigWCS.WebConfig_AppKeyName]))
                sDir= System.Web.Configuration.WebConfigurationManager.AppSettings[ConstStatic.WebConfigWCS.WebConfig_AppKeyName].Trim();
            else
                sDir= ConstStatic.WebConfigWCS.WebConfig_DefaultBaseFullPath+ConstStatic.WebConfigWCS.WebConfig_DefaultPath+"\\";


            //判断是否存在配置文件夹
            if (!System.IO.Directory.Exists(sDir))
            {
                SrnprCommon.CommonLog.ExceptionLogCCL.ThrowException(SrnprCommon.EnumCommon.ExceptionEnum.NotConfigPath, sDir);
            }








            return sDir;
        }






    }
}
