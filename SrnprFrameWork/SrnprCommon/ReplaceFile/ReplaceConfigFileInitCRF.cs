using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SrnprCommon.ReplaceFile
{
    class ReplaceConfigFileInitCRF
    {



        private static List<ServerDatabaseEntityCRF> dataBaseList;

        private static List<ServerEmailEntityCRF> emailList;



        public static void ProcessServerInfo(ReplaceFileEntityCRF rfe)
        {


            if (dataBaseList == null || emailList == null)
            {


                XmlDocument xdConfig = new XmlDocument();
                xdConfig.Load(CommonConfig.FrameWorkConfigCCC.GetFrameWorkConfigRoot().CommonConfigPath);


                XmlDocument xd = new XmlDocument();
                xd.Load(xdConfig.DocumentElement.SelectSingleNode("ReplaceFile/ReplaceFilePath").InnerText.Trim());

                dataBaseList = new List<ServerDatabaseEntityCRF>();
                emailList = new List<ServerEmailEntityCRF>();


                







            }


            



        }





    }
}
