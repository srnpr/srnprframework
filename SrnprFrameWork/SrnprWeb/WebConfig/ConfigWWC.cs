﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprWeb.WebConfig
{
    public class ConfigWWC
    {
        private  const  string WEB_CONFIG_NAME="SrnprWebConfig";

         static string WebConfigPath()
        {

             if(!string.IsNullOrEmpty(System.Web.Configuration.WebConfigurationManager.AppSettings[WEB_CONFIG_NAME]))
            return System.Web.Configuration.WebConfigurationManager.AppSettings[WEB_CONFIG_NAME].Trim();
             else
                 return "D:\\SrnprFrameWork\\SrnprWebConfig.config.xml";
        }




    }
}
