using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace SrnprCommon.ReplaceFile
{
    public class ReplaceXmlCRF
    {


        /// <summary>
        /// 
        /// Description: 得到替换的内容
        /// Author:Liudpc
        /// Create Date: 2010-6-8 16:45:27
        /// </summary>
        /// <returns></returns>
        public string GetXmlReplaceContent()
        {




            return "";
        }





        public bool SendEmail()
        {

            return true;

        }



        public TempleteXmlEntityCRF GetTempleteXml(string sFilePath)
        {
            TempleteXmlEntityCRF txe = new TempleteXmlEntityCRF();

            txe.Code = GetTempleteCode(sFilePath);

            txe.Design = GetTempleteDesign(sFilePath.Replace(".xml",".ds.xml"));


            return txe;
        }



        private TempleteCodeEntityCRF GetTempleteCode(string sFilePath)
        {
            TempleteCodeEntityCRF tce = new TempleteCodeEntityCRF();

            //判断文件是否存在
            if (File.Exists(sFilePath))
            {
                XmlDocument xd = new XmlDocument();
                xd.Load(sFilePath);
                XmlNode xnCode= xd.SelectSingleNode("ReplaceFileRoot/ReplaceFile/ReplaceFileCode");


                #region 取出配置列表

                XmlNode xnConfig = xnCode.SelectSingleNode("ConfigItem");
                if (xnConfig != null)
                {
                    tce.Config = new ItemConfigEntityCRF();
                    tce.Config.Title = xnConfig.SelectSingleNode("Title").InnerText.Trim();
                    tce.Config.Description = xnConfig.SelectSingleNode("Description").InnerText.Trim();
                    tce.Config.DataServerId = xnConfig.SelectSingleNode("DataServerId").InnerText.Trim();
                    tce.Config.EmailServerId = xnConfig.SelectSingleNode("EmailServerId").InnerText.Trim();
                    tce.Config.Version = xnConfig.SelectSingleNode("Version").InnerText.Trim();

                    //判断是否可用
                    string sUsed = xnConfig.SelectSingleNode("Used").InnerText.Trim().ToLower();
                    if (sUsed == "true" || sUsed == "1")
                    {
                        tce.Config.Used = true;
                    }
                    else
                    {
                        tce.Config.Used = false;
                    }
                }

                #endregion


                #region 取出输入参数

                XmlNodeList xnlParms = xnCode.SelectNodes("ParmItem/Parm");
                if (xnlParms != null)
                {
                    tce.Parm = new List<ItemPramEntityCRF>();
                    foreach (XmlNode xn in xnlParms)
                    {
                        tce.Parm.Add(new ItemPramEntityCRF() { ParmName = xn.Attributes["parmName"].Value.Trim(), ParmText = xn.Attributes["parmText"].Value.Trim() });
                    }
                }

                #endregion


                #region 取出主sql

                XmlNodeList xnlMainSql = xnCode.SelectNodes("MainItem/MainSql");
                if (xnlMainSql != null)
                {
                    tce.MainSql = new List<ItemMainSqlEntityCRF>();
                    foreach (XmlNode xn in xnlMainSql)
                    {
                        tce.MainSql.Add(new ItemMainSqlEntityCRF() { SqlString = xn.InnerText.Trim() });
                    }
                }

                #endregion


                #region 取出循环sql

                XmlNodeList xnlListItem = xnCode.SelectNodes("ListItem/ListSql");
                if (xnlListItem != null)
                {
                    tce.ListSql = new List<ItemListSqlEntityCRF>();
                    foreach (XmlNode xn in xnlListItem)
                    {
                        tce.ListSql.Add(new ItemListSqlEntityCRF() { SqlString = xn.InnerText.Trim() });
                    }
                }

                #endregion



            }
            else
            {
                AddLog("log20010015",sFilePath);
            }



            return tce;


        }

        private TempleteDesignEntityCRF GetTempleteDesign(string sFilePath)
        {
            TempleteDesignEntityCRF tde = new TempleteDesignEntityCRF();

            if (File.Exists(sFilePath))
            {
                XmlDocument xd = new XmlDocument();
                xd.Load(sFilePath);
                XmlNode xnDesign = xd.SelectSingleNode("ReplaceFileRoot/ReplaceFile/ReplaceFileDesign");






            }
            else
            {
                AddLog("log20010015", sFilePath);
            }





            return tde;
        }











        private void AddLog(string sLogId, params string[] strParams)
        {



        }

    }
}
