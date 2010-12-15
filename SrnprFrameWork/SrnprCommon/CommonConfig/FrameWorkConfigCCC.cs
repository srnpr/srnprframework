using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace SrnprCommon.CommonConfig
{

    /// <summary>
    /// Description: 整体配置文件
    /// Author:Liudpc
    /// Create Date: 2010-6-30 12:01:00
    /// </summary>
     class FrameWorkConfigCCC
    {
        private static FrameWorkConfigEntityCCC frameConfig;

        private static string BaseConfigPath = "D:\\SrnprFrameWork\\";



               
         /// 
         /// Description: 得到最基本配置
         /// Author:Liudpc
         /// Create Date: 2010-6-30 12:01:13
         /// </summary>
         /// <returns></returns>
        public static FrameWorkConfigEntityCCC GetFrameWorkConfigRoot()
        {
            if (frameConfig == null)
            {
                LoadConfig();
            }

            return frameConfig;

        }





         /// <summary>
         /// 
         /// Description: 重新加载配置文件
         /// Author:Liudpc
         /// Create Date: 2010-7-1 10:51:16
         /// </summary>
        private static void LoadConfig()
        {
            FrameWorkConfigEntityCCC fwce = new FrameWorkConfigEntityCCC();
            XmlDocument xd = new XmlDocument();
            string sPath=BaseConfigPath + "\\Config\\SrnprFrameWorkConfigSFW.xml";

            xd.Load(sPath);

            FileSystemWatcher fsw = new FileSystemWatcher(BaseConfigPath + "\\Config\\");
            fsw.Changed += new FileSystemEventHandler(fsw_Changed);
            fsw.EnableRaisingEvents = true;


            fwce.CommonConfigPath = GetConfigPath(xd.DocumentElement.SelectSingleNode("CommonConfig/ConfigFilePath").InnerText);

            frameConfig = fwce;
        }

         /// <summary>
         /// 
         /// Description: 如果配置文件有更改则调用
         /// Author:Liudpc
         /// Create Date: 2010-7-1 10:48:45
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        static void fsw_Changed(object sender, FileSystemEventArgs e)
        {
            FrameWorkConfigCCC.LoadConfig();
            ReplaceFileConfigCCC.LoadConfig();
        }



         /// <summary>
         /// 
         /// Description: 重新检测配置文件的路径并替换掉
         /// Author:Liudpc
         /// Create Date: 2010-6-30 12:00:27
         /// </summary>
         /// <param name="sPathUrl"></param>
         /// <returns></returns>
        public static string GetConfigPath(string sPathUrl)
        {
            if (sPathUrl.IndexOf("~\\") == 0)
            {
                sPathUrl = sPathUrl.Replace("~\\", BaseConfigPath);
            }
            return sPathUrl;
        }


    }
}
