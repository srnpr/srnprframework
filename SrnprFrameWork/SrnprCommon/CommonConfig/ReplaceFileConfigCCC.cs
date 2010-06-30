﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using SrnprCommon.ReplaceFile;
using System.IO;
using SrnprCommon.CommonFunction;

namespace SrnprCommon.CommonConfig
{
    public class ReplaceFileConfigCCC
    {

     




        private static ReplaceFileConfigEntityCCC replaceEntity;


        public  static ReplaceFileConfigEntityCCC Config
        {

            get
            {
                if (replaceEntity == null)
                {

                    replaceEntity = new ReplaceFileConfigEntityCCC();

                    replaceEntity.DataServerList = new List<ServerDatabaseEntityCRF>();
                    replaceEntity.EmailServerList = new List<ServerEmailEntityCRF>();


                    XmlDocument xdConfig = new XmlDocument();
                    xdConfig.Load(CommonConfig.FrameWorkConfigCCC.GetFrameWorkConfigRoot().CommonConfigPath);
                    XmlDocument xd = new XmlDocument();
                    xd.Load(FrameWorkConfigCCC.GetConfigPath(xdConfig.DocumentElement.SelectSingleNode("ReplaceFile/ReplaceFilePath").InnerText.Trim()));
                    XmlNode xnRoot = xd.DocumentElement;



                    replaceEntity.SplitString = xnRoot.SelectSingleNode("Config/SplitString").InnerText.Trim();
                    replaceEntity.ReplaceFrom = xnRoot.SelectSingleNode("Config/ReplaceFrom").InnerText.Trim();
                    replaceEntity.MainParmReplace = xnRoot.SelectSingleNode("Config/MainParmReplace").InnerText.Trim();
                    replaceEntity.XmlFileDirectory =FrameWorkConfigCCC.GetConfigPath( xnRoot.SelectSingleNode("Config/XmlFileDirectory").InnerText.Trim());
                    if (!Directory.Exists(replaceEntity.XmlFileDirectory))
                    {
                        Directory.CreateDirectory(replaceEntity.XmlFileDirectory);
                    }

                    replaceEntity.XmlFilehistoryDir = XmlStaticCCF.GetChildValueByName(xnRoot, "Config/ListFilePath");

                    replaceEntity.CodeFileApp = xnRoot.SelectSingleNode("Config/CodeFileApp").InnerText.Trim();
                    replaceEntity.DesignFileApp = xnRoot.SelectSingleNode("Config/DesignFileApp").InnerText.Trim();
                    replaceEntity.ListFilePath = xnRoot.SelectSingleNode("Config/ListFilePath").InnerText.Trim();


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
}
