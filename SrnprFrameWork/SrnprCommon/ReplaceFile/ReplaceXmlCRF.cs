using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;
using SrnprCommon.CommonConfig;
using SrnprCommon.EnumCommon;
using System.Data;
using System.Data.SqlClient;
using SrnprCommon.DataHelper;

namespace SrnprCommon.ReplaceFile
{
    public class ReplaceXmlCRF
    {



        public ReplaceResultEntity Replace(ReplaceFileEntityCRF replaceEntity)
        {

            ReplaceResultEntity returnResult = new ReplaceResultEntity();




            
            









            return returnResult;



        }
        
      



        public DataReplaceEntityCRF GetDataReplace(ReplaceFileEntityCRF rfe)
        {
            DataReplaceEntityCRF dre = new DataReplaceEntityCRF();
            dre.MainParms = new Dictionary<string, string>();

            string sMainReplace=ReplaceFileConfigCCC.MainParmReplace;

            List<SqlParameter> sqlParmList = new List<SqlParameter>();

            DataHelper.DataTableAutoHelperCDH dtah = new DataTableAutoHelperCDH();
       


            #region 开始进行输入参数的处理
            if (!string.IsNullOrEmpty(rfe.ReplaceParms))
            {

                string[] strParms = Regex.Split(rfe.ReplaceParms, ReplaceFileConfigCCC.SplitString);

                List<ItemPramEntityCRF> parmList = rfe.TempleteXml.Code.Parm;
                for (int i = 0, j = strParms.Length; i < j; i++)
                {
                    int iIndex=strParms[i].IndexOf("=");
                    if(iIndex>0)
                    {
                        ItemPramEntityCRF  ipe= parmList.Single(t => t.ParmName == strParms[i].Substring(0, iIndex));
                        sqlParmList.Add(new SqlParameter("@"+ipe,strParms[i].Substring(iIndex+1)));

                        if (ipe != null)
                        {
                            dre.MainParms.Add(string.Format(sMainReplace, ipe.ParmText), strParms[i].Substring(iIndex + 1));

                        }
                    }
                }

            }
            #endregion


            #region 开始进行主sql取出
            if (rfe.TempleteXml.Code.MainSql.Count > 0)
            {
                SqlParameter[] sp=sqlParmList.ToArray();
                foreach(ItemMainSqlEntityCRF imse in rfe.TempleteXml.Code.MainSql)
                {
                    DataTable dt = dtah.GetDataTable(rfe.DataServer, imse.SqlString, sp);
                    if(dt.Rows.Count>0)
                    {
                        for (int i = 0, j = dt.Columns.Count; i < j; i++)
                        {
                            dre.MainParms.Add(string.Format(sMainReplace, dt.Columns[i].ColumnName), dt.Rows[0][i].ToString().Trim());
                        }
                    }
                }
            }

            #endregion


            if (rfe.TempleteXml.Code.ListSql.Count > 0)
            {
                SqlParameter[] sp=sqlParmList.ToArray();
                foreach (ItemListSqlEntityCRF ilse in rfe.TempleteXml.Code.ListSql)
                {
                    dre.ListParms.Add(dtah.GetDataTable(rfe.DataServer, ReplaceParmsByDict(ilse.SqlString,dre.MainParms), sp));
                }
            }

            return dre;
        }




        /// <summary>
        /// 
        /// Description: 根据字典替换字符串
        /// Author:Liudpc
        /// Create Date: 2010-6-10 12:03:39
        /// </summary>
        /// <param name="sInput"></param>
        /// <param name="dValue"></param>
        /// <returns></returns>
        private string ReplaceParmsByDict(string sInput, Dictionary<string, string> dValue)
        {
            if (sInput.IndexOf(ReplaceFileConfigCCC.ReplaceFrom) > -1)
            {
                foreach (KeyValuePair<string, string> kvp in dValue)
                {
                    sInput = sInput.Replace(kvp.Key, kvp.Value);
                }
            }
            return sInput;


        }




        /// <summary>
        /// 
        /// Description: 根据文件路径得到实体
        /// Author:Liudpc
        /// Create Date: 2010-6-10 14:13:27
        /// </summary>
        /// <param name="sFilePath"></param>
        /// <returns></returns>
        public TempleteXmlEntityCRF GetTempleteXml(string sFilePath)
        {
            TempleteXmlEntityCRF txe = new TempleteXmlEntityCRF();

            txe.Code = GetTempleteCode(sFilePath);

            txe.Design = GetTempleteDesign(sFilePath.Replace(".xml",".ds.xml"));


            return txe;
        }



        /// <summary>
        /// 
        /// Description: 得到编码实体
        /// Author:Liudpc
        /// Create Date: 2010-6-9 17:01:41
        /// </summary>
        /// <param name="sFilePath"></param>
        /// <returns></returns>
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



        /// <summary>
        /// 
        /// Description: 得到设计实体
        /// Author:Liudpc
        /// Create Date: 2010-6-9 17:01:54
        /// </summary>
        /// <param name="sFilePath"></param>
        /// <returns></returns>
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
