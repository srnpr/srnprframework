using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using SrnprCommon.ReplaceFile;

namespace SrnprCommon.CommonConfig
{
    public class ReplaceFileConfigCCC
    {

     




        private static ReplaceFileConfigEntityCCC replaceEntity;


        public  static ReplaceFileConfigEntityCCC Config()
        {

            if (replaceEntity == null)
            {

                replaceEntity = new ReplaceFileConfigEntityCCC();

                replaceEntity.DataServerList = new List<ServerDatabaseEntityCRF>();
                replaceEntity.EmailServerList = new List<ServerEmailEntityCRF>();


                XmlDocument xdConfig = new XmlDocument();
                xdConfig.Load(CommonConfig.FrameWorkConfigCCC.GetFrameWorkConfigRoot().CommonConfigPath);
                XmlDocument xd = new XmlDocument();
                xd.Load(xdConfig.DocumentElement.SelectSingleNode("ReplaceFile/ReplaceFilePath").InnerText.Trim());
                XmlNode xnRoot = xd.DocumentElement;



                replaceEntity.SplitString = xnRoot.SelectSingleNode("Config/SplitString").InnerText.Trim();
                replaceEntity.ReplaceFrom = xnRoot.SelectSingleNode("Config/ReplaceFrom").InnerText.Trim();
                replaceEntity.MainParmReplace = xnRoot.SelectSingleNode("Config/MainParmReplace").InnerText.Trim();
                replaceEntity.XmlFileDirectory = xnRoot.SelectSingleNode("Config/XmlFileDirectory").InnerText.Trim();


                foreach (XmlNode xn in xnRoot.SelectNodes("DataServerInfo/DataServer"))
                {
                    ServerDatabaseEntityCRF db = new ServerDatabaseEntityCRF();
                    db.Id = xn.Attributes["id"].Value.Trim();

                    switch (xn.Attributes["serverType"].Value.Trim())
                    {
                        case "mssql":
                            db.ServerType = EnumCommon.DataServerType.MsSql;
                            break;

                        default:
                            db.ServerType = EnumCommon.DataServerType.MsSql;
                            break;
                    }

                    db.ConnString = xn.Attributes["connString"].Value.Trim();


                    replaceEntity.DataServerList.Add(db);
                }


                foreach (XmlNode xn in xnRoot.SelectNodes("EmailServerInfo/EmailServer"))
                {
                    ServerEmailEntityCRF em = new ServerEmailEntityCRF();
                    em.Id = xn.Attributes["id"].Value.Trim();

                    replaceEntity.EmailServerList.Add(em);
                }








            }






            return replaceEntity;


        }
        




    }
}
