using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprWeb.WebConfig
{
    public class ConfigWWC
    {
      


        public static string WebConfigPath = "";

        private static string GetWebConfigPath()
        {

            string sDir = "";




            if (!string.IsNullOrEmpty(System.Web.Configuration.WebConfigurationManager.AppSettings[ConstStatic.WebConfigWCS.WebConfig_AppKeyName]))
                sDir= System.Web.Configuration.WebConfigurationManager.AppSettings[ConstStatic.WebConfigWCS.WebConfig_AppKeyName].Trim();
            else
                sDir= ConstStatic.WebConfigWCS.WebConfig_DefaultBaseFullPath+ConstStatic.WebConfigWCS.WebConfig_DefaultPath+"\\";


            



            return sDir;
        }






    }
}
