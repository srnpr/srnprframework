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



        public  void ProcessServerInfo(ServerDatabaseEntityCRF dbEntity,ServerEmailEntityCRF emailEntity)
        {


            if (dataBaseList == null || emailList == null)
            {

                XmlDocument xdConfig = new XmlDocument();
                xdConfig.Load(CommonConfig.FrameWorkConfigCCC.GetFrameWorkConfigRoot().CommonConfigPath);
                XmlDocument xd = new XmlDocument();
                 xd.Load(xdConfig.DocumentElement.SelectSingleNode("ReplaceFile/ReplaceFilePath").InnerText.Trim());
                 XmlNode xnRoot = xd.DocumentElement;
                dataBaseList = new List<ServerDatabaseEntityCRF>();
                emailList = new List<ServerEmailEntityCRF>();
                foreach (XmlNode xn in xnRoot.SelectNodes("DataServerInfo/DataServer"))
                {
                    ServerDatabaseEntityCRF db = new ServerDatabaseEntityCRF();
                    db.Id = xn.Attributes["id"].Value.Trim();

                    switch(xn.Attributes["serverType"].Value.Trim())
                    {
                        case "mssql":
                            db.ServerType=EnumCommon.DataServerType.MsSql;
                            break;

                        default:
                            db.ServerType=EnumCommon.DataServerType.MsSql;
                            break;
                    }

                    db.ConnString = xn.Attributes["connString"].Value.Trim();


                    dataBaseList.Add(db);
                }


                foreach (XmlNode xn in xnRoot.SelectNodes("EmailServerInfo/EmailServer"))
                {
                    ServerEmailEntityCRF em = new ServerEmailEntityCRF();
                    em.Id = xn.Attributes["id"].Value.Trim();

                    emailList.Add(em);
                }



            }

            dbEntity = dataBaseList.SingleOrDefault(t => t.Id == dbEntity.Id);
            emailEntity = emailList.SingleOrDefault(t => t.Id == emailEntity.Id);
            



        }





    }
}
