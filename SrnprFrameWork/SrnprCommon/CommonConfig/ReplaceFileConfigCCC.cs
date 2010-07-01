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

     




        private static ReplaceFileConfigEntityCCC rcEntity;



        public static bool LoadConfig()
        {

            ReplaceFileConfigEntityCCC rfce = new ReplaceFileConfigEntityCCC();
            rfce.DataServerList = new List<ServerDatabaseEntityCRF>();
            rfce.EmailServerList = new List<ServerEmailEntityCRF>();
            XmlDocument xdConfig = new XmlDocument();
            xdConfig.Load(CommonConfig.FrameWorkConfigCCC.GetFrameWorkConfigRoot().CommonConfigPath);


            XmlDocument xd = new XmlDocument();
            xd.Load(FrameWorkConfigCCC.GetConfigPath(XmlStaticCCF.GetChildValueByName(xdConfig.DocumentElement, "ReplaceFile/ReplaceFilePath")));
            XmlNode xnRoot = xd.DocumentElement;



            rfce.SplitString = XmlStaticCCF.GetChildValueByName(xnRoot, "Config/SplitString");
            rfce.ReplaceFrom = XmlStaticCCF.GetChildValueByName(xnRoot, "Config/ReplaceFrom");
            rfce.MainParmReplace = XmlStaticCCF.GetChildValueByName(xnRoot, "Config/MainParmReplace");

            rfce.XmlFileDirectory = FrameWorkConfigCCC.GetConfigPath(XmlStaticCCF.GetChildValueByName(xnRoot, "Config/XmlFileDirectory"));
            IoStaticCCF.CheckDirectory(rfce.XmlFileDirectory);

            rfce.XmlFileHistoryDir = FrameWorkConfigCCC.GetConfigPath(XmlStaticCCF.GetChildValueByName(xnRoot, "Config/XmlFileHistoryDir"));
            IoStaticCCF.CheckDirectory(rfce.XmlFileHistoryDir);


            rfce.CodeFileApp = XmlStaticCCF.GetChildValueByName(xnRoot, "Config/CodeFileApp");
            rfce.DesignFileApp = XmlStaticCCF.GetChildValueByName(xnRoot, "Config/DesignFileApp");



            rfce.ListFileDir = FrameWorkConfigCCC.GetConfigPath(XmlStaticCCF.GetChildValueByName(xnRoot, "Config/ListFileDir"));

            IoStaticCCF.CheckDirectory(rfce.ListFileDir);

            rfce.ListFilePath = rfce.ListFileDir + XmlStaticCCF.GetChildValueByName(xnRoot, "Config/ListFilePath");


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

                db.ConnString = XmlStaticCCF.GetAttValueByName(xn, "connString");


                rfce.DataServerList.Add(db);
            }


            foreach (XmlNode xn in xnRoot.SelectNodes("EmailServerInfo/EmailServer"))
            {
                ServerEmailEntityCRF em = new ServerEmailEntityCRF();
                em.Id = xn.Attributes["id"].Value.Trim();

                rfce.EmailServerList.Add(em);
            }

            rcEntity = rfce;
            return true;
        }




        /// <summary>
        /// Description: 文件替换配置参数
        /// Author:Liudpc
        /// Create Date: 2010-6-30 13:22:50
        /// </summary>
        public  static ReplaceFileConfigEntityCCC Config
        {

            get
            {
                if (rcEntity == null)
                {
                    LoadConfig();

                }

                return rcEntity;
            }


        }
        




    }
}
