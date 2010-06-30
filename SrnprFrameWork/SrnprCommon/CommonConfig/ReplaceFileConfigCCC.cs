using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using SrnprCommon.ReplaceFile;
using System.IO;
using SrnprCommon.CommonFunction;

namespace SrnprCommon.CommonConfig
{

    /// <summary>
    /// Description: 文件替换配置
    /// Author:Liudpc
    /// Create Date: 2010-6-30 13:23:02
    /// </summary>
    public class ReplaceFileConfigCCC
    {

     




        private static ReplaceFileConfigEntityCCC replaceEntity;


        /// <summary>
        /// Description: 文件替换配置参数
        /// Author:Liudpc
        /// Create Date: 2010-6-30 13:22:50
        /// </summary>
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
                    xd.Load(FrameWorkConfigCCC.GetConfigPath(XmlStaticCCF.GetChildValueByName(xdConfig.DocumentElement,"ReplaceFile/ReplaceFilePath")));
                    XmlNode xnRoot = xd.DocumentElement;



                    replaceEntity.SplitString = XmlStaticCCF.GetChildValueByName(xnRoot,"Config/SplitString");
                    replaceEntity.ReplaceFrom = XmlStaticCCF.GetChildValueByName(xnRoot,"Config/ReplaceFrom");
                    replaceEntity.MainParmReplace = XmlStaticCCF.GetChildValueByName(xnRoot,"Config/MainParmReplace");

                    replaceEntity.XmlFileDirectory = FrameWorkConfigCCC.GetConfigPath(XmlStaticCCF.GetChildValueByName(xnRoot,"Config/XmlFileDirectory"));
                    IoStaticCCF.CheckDirectory(replaceEntity.XmlFileDirectory);

                    replaceEntity.XmlFileHistoryDir = XmlStaticCCF.GetChildValueByName(xnRoot, "Config/ListFilePath");
                    IoStaticCCF.CheckDirectory(replaceEntity.XmlFileHistoryDir);


                    replaceEntity.CodeFileApp = XmlStaticCCF.GetChildValueByName(xnRoot,"Config/CodeFileApp");
                    replaceEntity.DesignFileApp = XmlStaticCCF.GetChildValueByName(xnRoot,"Config/DesignFileApp");



                    replaceEntity.ListFileDir = XmlStaticCCF.GetChildValueByName(xnRoot, "Config/ListFileDir");

                    IoStaticCCF.CheckDirectory(replaceEntity.ListFileDir);

                    replaceEntity.ListFilePath = XmlStaticCCF.GetChildValueByName(xnRoot,"Config/ListFilePath");


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

                        db.ConnString = XmlStaticCCF.GetAttValueByName(xn,"connString");


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
