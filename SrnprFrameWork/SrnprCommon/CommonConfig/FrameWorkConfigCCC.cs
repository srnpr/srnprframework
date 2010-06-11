﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;


namespace SrnprCommon.CommonConfig
{
     class FrameWorkConfigCCC
    {
        private static FrameWorkConfigEntityCCC frameConfig;
        public static FrameWorkConfigEntityCCC GetFrameWorkConfigRoot()
        {
            if (frameConfig == null)
            {
                frameConfig = new FrameWorkConfigEntityCCC();


                XmlDocument xd = new XmlDocument();
                xd.Load(@"S:\AAAProject\SrnprFrameWork\SrnprFile\FraomeWork\SrnprFrameWorkConfigSFW.xml");






               frameConfig.CommonConfigPath= xd.DocumentElement.SelectSingleNode("CommonConfig/ConfigFilePath").InnerText;




            }

            return frameConfig;

        }

    }
}