using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;


namespace SrnprCommon.CommonConfig
{
     class FrameWorkConfigCCC
    {
        private static FrameWorkConfigEntityCCC frameConfig;

        private static string BaseConfigPath = "D:\\SrnprFrameWork\\";

        public static FrameWorkConfigEntityCCC GetFrameWorkConfigRoot()
        {
            if (frameConfig == null)
            {
                frameConfig = new FrameWorkConfigEntityCCC();


                XmlDocument xd = new XmlDocument();
                xd.Load(@BaseConfigPath+"SrnprFrameWorkConfigSFW.xml");
               frameConfig.CommonConfigPath= xd.DocumentElement.SelectSingleNode("CommonConfig/ConfigFilePath").InnerText;



            }

            return frameConfig;

        }



         /// <summary>
         /// 
         /// Description: 重新检测配置文件
         /// Author:Liudpc
         /// Create Date: 2010-6-30 12:00:27
         /// </summary>
         /// <param name="sPathUrl"></param>
         /// <returns></returns>
        public static string GetConfigPath(string sPathUrl)
        {
            if (sPathUrl.IndexOf("~/") == 0)
            {
                sPathUrl = sPathUrl.Replace("~/", BaseConfigPath);
            }
            return sPathUrl;
        }


    }
}
