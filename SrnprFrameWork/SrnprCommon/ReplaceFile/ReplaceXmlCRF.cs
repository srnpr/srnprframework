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
      
       
       



        /// <summary>
        /// 
        /// Description: 根据sql解析出可以匹配使用的参数
        /// Author:Liudpc
        /// Create Date: 2010-6-12 16:02:51
        /// </summary>
        /// <param name="sSql"></param>
        /// <returns></returns>
        public string[] RegexSqlStringParm(string sSql)
        {
            List<string> strList = new List<string>();
           string sReplace= Regex.Replace(sSql, @"\(.*\)", "").ToLower();
          sReplace= Regex.Match(sReplace,"(?<=select).*?(?=from)").Value;

          foreach (string s in sReplace.Split(','))
          {
              if (s.LastIndexOf(" as ") > -1)
              {
                  strList.Add(  s.Substring(s.LastIndexOf(" as ")+3).Trim());
              }
          }

            return strList.ToArray();

        
        }



        /// <summary>
        /// 
        /// Description: 根据文件路径分析出列表元素
        /// Author:Liudpc
        /// Create Date: 2010-6-11 16:23:48
        /// </summary>
        /// <param name="sListFilePath"></param>
        /// <returns></returns>
        public List<ReplaceFileListEntityCRF> GetListFileInfoByFilePath(string sListFilePath)
        {

            List<ReplaceFileListEntityCRF> list = new List<ReplaceFileListEntityCRF>();
            if (File.Exists(sListFilePath))
            {
                XmlDocument xd = new XmlDocument();
                xd.Load(sListFilePath);
                foreach (XmlNode xn in xd.DocumentElement.SelectNodes("EmailList/EmailInfo"))
                {
                    ReplaceFileListEntityCRF r = new ReplaceFileListEntityCRF();
                    r.Id = xn.ChildNodes[0].InnerText.Trim();
                    r.Title = xn.ChildNodes[1].InnerText.Trim();
                    r.Description = xn.ChildNodes[2].InnerText.Trim();
                    r.FilePath = xn.ChildNodes[3].InnerText.Trim();
                    list.Add(r);
                }
            }

            return list;
        }




       

        /// <summary>
        /// 
        /// Description: 根据文件夹名称分析该文件夹下内容
        /// Author:Liudpc
        /// Create Date: 2010-6-11 16:19:01
        /// </summary>
        /// <param name="sDirectory"></param>
        /// <param name="sListFileSavePath"></param>
        /// <returns></returns>
        public bool RecheckXmlFromDirectory(string sDirectory,string sListFileSavePath)
        {


            if (Directory.Exists(sDirectory))
            {
                string sCode=CommonConfig.ReplaceFileConfigCCC.Config.CodeFileApp;
                string sDesign=CommonConfig.ReplaceFileConfigCCC.Config.DesignFileApp;
                XmlDocument xd = new XmlDocument();
                XmlNode xnRoot = xd.CreateElement("EmailListRoot");
                XmlNode xnList = xnRoot.AppendChild(xd.CreateElement("EmailList"));
                foreach (string sFileName in Directory.GetFiles(sDirectory))
                {

                    if(sFileName.IndexOf(sCode)>0&&sFileName.IndexOf(sDesign)==-1)
                    {

                        TempleteCodeEntityCRF code = GetTempleteCode(sFileName);
                        XmlNode xn = xd.CreateElement("EmailInfo");
                      
                        XmlAppendNode(xd, "Id", sFileName.Substring(sFileName.LastIndexOf("\\") + 1, sFileName.LastIndexOf(sCode) - sFileName.LastIndexOf("\\") - 1), xn);
                        XmlAppendNode(xd, "Title", code.Config.Title, xn);
                        XmlAppendNode(xd, "Description", code.Config.Description, xn);
                        XmlAppendNode(xd, "FilePath", sFileName, xn);
                        xnList.AppendChild(xn);
                    }

                }

                xd.AppendChild(xnRoot);
                xd.Save(sListFileSavePath);





            }
            else
            {
                AddLog(20010025,sDirectory);
            }






            return true;
        }





        /// <summary>
        /// 
        /// Description: 根据状态sql取出所有的状态
        /// Author:Liudpc
        /// Create Date: 2010-6-13 11:46:22
        /// </summary>
        /// <param name="sSqlstring"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public string[] GetStateValue(string sSqlstring, ServerDatabaseEntityCRF db)
        {
            DataTable dt = new DataHelper.DataTableAutoHelperCDH().GetDataTable(db, sSqlstring, null);
            string[] str = new string[dt.Rows.Count];
            for (int i = 0, j = str.Length; i < j; i++)
            {
                str[i] = dt.Rows[i][0].ToString().Trim();
            }
            return str;

        }




        /// <summary>
        /// 
        /// Description: 从数据库中匹配出内容
        /// Author:Liudpc
        /// Create Date: 2010-6-11 16:38:03
        /// </summary>
        /// <param name="rfe"></param>
        /// <returns></returns>
        public DataReplaceEntityCRF GetDataReplace(ReplaceFileEntityCRF rfe)
        {
            DataReplaceEntityCRF dre = new DataReplaceEntityCRF();
            dre.ResultFlag = true;


            dre.MainParms = new Dictionary<string, string>();

            string sMainReplace = ReplaceFileConfigCCC.Config.MainParmReplace;

            List<SqlParameter> sqlParmList = new List<SqlParameter>();

            DataHelper.DataTableAutoHelperCDH dtah = new DataTableAutoHelperCDH();
       


            #region 开始进行输入参数的处理
            if (!string.IsNullOrEmpty(rfe.ReplaceParms))
            {

                string[] strParms = Regex.Split(rfe.ReplaceParms, ReplaceFileConfigCCC.Config.SplitString);

                List<ItemPramEntityCRF> parmList = rfe.TempleteXml.Code.Parm;
                for (int i = 0, j = strParms.Length; i < j; i++)
                {
                    int iIndex=strParms[i].IndexOf("=");
                    if(iIndex>0)
                    {
                        ItemPramEntityCRF  ipe= parmList.Single(t => t.ParmName == strParms[i].Substring(0, iIndex));
                        sqlParmList.Add(new SqlParameter("@"+ipe.ParmName,strParms[i].Substring(iIndex+1)));

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
                    if (dre.ResultFlag)
                    {

                        DataTable dt = dtah.GetDataTable(rfe.DataServer, ReplaceParmsByDict(imse.SqlString, dre.MainParms), sp);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0, j = dt.Columns.Count; i < j; i++)
                            {
                                dre.MainParms.Add(string.Format(sMainReplace, dt.Columns[i].ColumnName), dt.Rows[0][i].ToString().Trim());
                            }
                        }
                        else
                        {
                            dre.ResultFlag = false;
                        }
                    }
                    
                }
            }

            #endregion


            #region 开始进行循环表格取出
            if (rfe.TempleteXml.Code.ListSql.Count > 0)
            {
                dre.ListParms = new List<DataTable>();
                SqlParameter[] sp=sqlParmList.ToArray();
                foreach (ItemListSqlEntityCRF ilse in rfe.TempleteXml.Code.ListSql)
                {
                    dre.ListParms.Add(dtah.GetDataTable(rfe.DataServer, ReplaceParmsByDict(ilse.SqlString,dre.MainParms), sp));
                }
            }
            #endregion 

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
        public string ReplaceParmsByDict(string sInput, Dictionary<string, string> dValue)
        {
            if (sInput.IndexOf(ReplaceFileConfigCCC.Config.ReplaceFrom) > -1)
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
        public TempleteCodeEntityCRF GetTempleteCode(string sFilePath)
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
                   

                    tce.Config.StateSql =XmlGetChildValueByName(xnConfig,"StateSql");

                    tce.Config.Version =XmlGetChildValueByName(xnConfig,"Version");

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
                        tce.Parm.Add(new ItemPramEntityCRF() { ParmName = XmlGetAttValueByName(xn,"parmName"), ParmText =XmlGetAttValueByName(xn,"parmText"),Guid=XmlGetAttValueByName(xn,"guid")  });
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
                        tce.MainSql.Add(new ItemMainSqlEntityCRF() { SqlString = xn.InnerText.Trim(), Guid = XmlGetAttValueByName(xn, "guid") });
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
                        tce.ListSql.Add(new ItemListSqlEntityCRF() { SqlString = xn.InnerText.Trim(), Guid = XmlGetAttValueByName(xn, "guid") });
                    }
                }

                #endregion



            }
            else
            {
                AddLog(20010015,sFilePath);
            }



            return tce;


        }




        /// <summary>
        /// 
        /// Description: 保存设计到xml文件
        /// Author:Liudpc
        /// Create Date: 2010-6-12 17:17:49
        /// </summary>
        /// <param name="desigon"></param>
        /// <param name="sSavePath"></param>
        /// <returns></returns>
        public bool SaveTempleteDesign(TempleteDesignEntityCRF desigon,string sSavePath)
        {

            XmlDocument xd = new XmlDocument();

            XmlNode xnReplaceFileRoot = xd.CreateElement("ReplaceFileRoot");

            XmlNode xnReplaceFile = xd.CreateElement("ReplaceFile");


            XmlNode xnReplaceFileDesign = xd.CreateElement("ReplaceFileDesign");

            XmlNode xnRuleItem = xd.CreateElement("RuleItem");
            XmlNode xnRule = xd.CreateElement("Rule");
            XmlNode xnRuleExpression = xd.CreateElement("RuleExpression");
            foreach (ItemRuleEntityAtCRF ruleEntity in desigon.ItemRule)
            {
                switch (ruleEntity.RuleType)
                {
                    case ItemRuleType.RuleExpression:

                        ItemRuleExpressionEntityCRF iree=ruleEntity as ItemRuleExpressionEntityCRF;
                        XmlNode xnExpressionInfo = xd.CreateElement("ExpressionInfo");


                        XmlAppendNode(xd, "TempleteGuid", iree.TempleteGuid, xnExpressionInfo);
                        XmlAppendNode(xd, "Expression", iree.Expression, xnExpressionInfo);
                        XmlAppendNode(xd, "ExpressionParm", iree.ExpressionParm, xnExpressionInfo);

                        xnRuleExpression.AppendChild(xnExpressionInfo);

                        break;
                }

            }
            xnRule.AppendChild(xnRuleExpression);
            xnRuleItem.AppendChild(xnRule);
            xnReplaceFileDesign.AppendChild(xnRuleItem);





            XmlNode xnTempleteItem = xd.CreateElement("TempleteItem");
            foreach (ItemTempleteEntityIfCRF tempEntity in desigon.ItemTemplete)
            {
                switch (tempEntity.TempleteType)
                {
                    case ItemTempleteType.EmailInfo:

                        ItemTempleteEmailInfoEntityCRF itee = tempEntity as ItemTempleteEmailInfoEntityCRF;
                        XmlNode xnTemplete = xd.CreateElement("Templete");

                        XmlAttribute xa = xd.CreateAttribute("guid");
                        xa.Value = itee.Guid;
                        xnTemplete.Attributes.Append(xa);


                        XmlNode xnEmailInfo = xd.CreateElement("EmailInfo");

                        XmlAppendNode(xd, "Title", itee.Title, xnEmailInfo);
                        XmlAppendNode(xd, "Content", itee.Content, xnEmailInfo);

                        xnTemplete.AppendChild(xnEmailInfo);

                        xnTempleteItem.AppendChild(xnTemplete);

                        break;
                }
            }




            xnReplaceFileDesign.AppendChild(xnTempleteItem);

            xnReplaceFile.AppendChild(xnReplaceFileDesign);


            xnReplaceFileRoot.AppendChild(xnReplaceFile);
            xd.AppendChild(xnReplaceFileRoot);




            xd.Save(sSavePath);



            return true;

        }


        /// <summary>
        /// 
        /// Description: 保存代码xml
        /// Author:Liudpc
        /// Create Date: 2010-6-30 11:11:40
        /// </summary>
        /// <param name="code"></param>
        /// <param name="sSavePath"></param>
        /// <returns></returns>
        public BaseEntity.ResultReturnEntityCBE SaveTempleteCode(TempleteCodeEntityCRF code, string sSavePath)
        {
            BaseEntity.ResultReturnEntityCBE rre = new SrnprCommon.BaseEntity.ResultReturnEntityCBE();
            rre.ResultFlag = true;
            XmlDocument xd = new XmlDocument();




            XmlNode xnReplaceFileRoot = xd.CreateElement("ReplaceFileRoot");
            XmlNode xnReplaceFile = xd.CreateElement("ReplaceFile");
            XmlNode xnReplaceFileCode = xd.CreateElement("ReplaceFileCode");


            #region 开始保存配置文件
            if (rre.ResultFlag)
            {
                XmlNode xnConfigItem = xd.CreateElement("ConfigItem");
                XmlAppendNode(xd, "Used", code.Config.Used ? "true" : "false", xnConfigItem);
                XmlAppendNode(xd, "Title", code.Config.Title, xnConfigItem);
                XmlAppendNode(xd, "Description", code.Config.Description, xnConfigItem);
                XmlAppendNode(xd, "DataServerId", code.Config.DataServerId, xnConfigItem);
                XmlAppendNode(xd, "EmailServerId", code.Config.EmailServerId, xnConfigItem);
                XmlAppendNode(xd, "StateSql", code.Config.StateSql, xnConfigItem);
                XmlAppendNode(xd, "Version", code.Config.Version, xnConfigItem);

                xnReplaceFileCode.AppendChild(xnConfigItem);
            }
            #endregion


            #region 开始保存输入参数
            if (rre.ResultFlag)
            {
                XmlNode xnParmItem = xd.CreateElement("ParmItem");

                foreach (var v in code.Parm)
                {
                    XmlNode xnParm = xd.CreateElement("Parm");
                    xnParm.Attributes["parmText"].Value = v.ParmText;
                    xnParm.Attributes["parmName"].Value = v.ParmName;
                    xnParm.Attributes["guid"].Value = v.Guid;
                    xnParmItem.AppendChild(xnParm);
                }

                xnReplaceFileCode.AppendChild(xnParmItem);
            }
            #endregion

            #region 开始保存主Sql
            if (rre.ResultFlag)
            {
                XmlNode xnMainItem = xd.CreateElement("MainItem");

                foreach (var v in code.MainSql)
                {
                    XmlNode xnMainSql = xd.CreateElement("MainSql");
                    xnMainSql.Attributes["guid"].Value = v.Guid;
                    xnMainSql.InnerText = v.SqlString;
                    xnMainItem.AppendChild(xnMainSql);
                }

                xnReplaceFileCode.AppendChild(xnMainItem);
            }
            #endregion

            #region 开始保存循环Sql
            if (rre.ResultFlag)
            {
                XmlNode xnListItem = xd.CreateElement("ListItem");

                foreach (var v in code.ListSql)
                {
                    XmlNode xnListSql = xd.CreateElement("ListSql");
                    xnListSql.Attributes["guid"].Value = v.Guid;
                    xnListSql.InnerText = v.SqlString;
                    xnListItem.AppendChild(xnListSql);
                }

                xnReplaceFileCode.AppendChild(xnListItem);
            }
            #endregion


            xnReplaceFile.AppendChild(xnReplaceFileCode);
            xnReplaceFileRoot.AppendChild(xnReplaceFile);

            xd.AppendChild(xnReplaceFileRoot);

            xd.Save(sSavePath);

            return rre;
        }




        /// <summary>
        /// 
        /// Description: Xml文件添加元素
        /// Author:Liudpc
        /// Create Date: 2010-6-12 17:18:16
        /// </summary>
        /// <param name="xd"></param>
        /// <param name="sNodeName"></param>
        /// <param name="sInnerText"></param>
        /// <param name="xnFather"></param>
        private void XmlAppendNode(XmlDocument xd,string sNodeName, string sInnerText, XmlNode xnFather)
        {
            XmlNode xn = xd.CreateElement(sNodeName);
            xn.InnerText = sInnerText;
            xnFather.AppendChild(xn);

        }




        /// <summary>
        /// 
        /// Description: 根据名称取出子元素并返回字符串  适用于可能存在
        /// Author:Liudpc
        /// Create Date: 2010-6-13 11:25:00
        /// </summary>
        /// <param name="xnFather"></param>
        /// <param name="sNodeName"></param>
        /// <returns></returns>
        private string XmlGetChildValueByName(XmlNode xnFather, string sNodeName)
        {
            XmlNode xn = xnFather.SelectSingleNode(sNodeName);
            if (xn != null)
            {
                return xn.InnerText.Trim();

            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 
        /// Description: 根据名称取出属性
        /// Author:Liudpc
        /// Create Date: 2010-6-29 15:11:36
        /// </summary>
        /// <param name="xnNode"></param>
        /// <param name="sAttName"></param>
        /// <returns></returns>
        private string XmlGetAttValueByName(XmlNode xnNode, string sAttName)
        {
            XmlAttribute xa = xnNode.Attributes[sAttName];
            if (xa != null)
            {
                return xa.Value;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 
        /// Description: 得到设计实体
        /// Author:Liudpc
        /// Create Date: 2010-6-9 17:01:54
        /// </summary>
        /// <param name="sFilePath"></param>
        /// <returns></returns>
        public TempleteDesignEntityCRF GetTempleteDesign(string sFilePath)
        {
            TempleteDesignEntityCRF returnDesignEntity = new TempleteDesignEntityCRF();

            if (File.Exists(sFilePath))
            {
                XmlDocument xd = new XmlDocument();
                xd.Load(sFilePath);
                XmlNode xnDesign = xd.SelectSingleNode("ReplaceFileRoot/ReplaceFile/ReplaceFileDesign");


                returnDesignEntity.ItemRule = new List<ItemRuleEntityAtCRF>();
                XmlNode xnRule = xnDesign.SelectSingleNode("RuleItem/Rule");
                if (xnRule != null)
                {
                    foreach (XmlNode xnRuleInfo in xnRule.ChildNodes)
                    {
                        if (xnRuleInfo.Name == "RuleExpression")
                        {
                            foreach (XmlNode xn in xnRuleInfo.SelectNodes("ExpressionInfo"))
                            {
                                ItemRuleExpressionEntityCRF iree = new ItemRuleExpressionEntityCRF();
                                iree.Expression = xn.SelectSingleNode("Expression").InnerText.Trim();
                                iree.TempleteGuid = xn.SelectSingleNode("TempleteGuid").InnerText.Trim();
                                iree.ExpressionParm = xn.SelectSingleNode("ExpressionParm").InnerText.Trim();
                                returnDesignEntity.ItemRule.Add(iree);
                            }
                        }

                    }
                }


                returnDesignEntity.ItemTemplete = new List<ItemTempleteEntityIfCRF>();
                XmlNode xnTempleteItem = xnDesign.SelectSingleNode("TempleteItem");
                if (xnTempleteItem != null)
                {
                    foreach (XmlNode xnTemplete in xnTempleteItem)
                    {

                        foreach (XmlNode xnTempleteInfo in xnTemplete.ChildNodes)
                        {
                            if (xnTempleteInfo.Name == "EmailInfo")
                            {

                                ItemTempleteEmailInfoEntityCRF itee = new ItemTempleteEmailInfoEntityCRF();
                                itee.Guid = xnTemplete.Attributes["guid"].Value.Trim();
                                itee.Title = xnTempleteInfo.SelectSingleNode("Title").InnerText;
                                itee.Content = xnTempleteInfo.SelectSingleNode("Content").InnerText;

                                returnDesignEntity.ItemTemplete.Add(itee);
                            }

                        }
                    }
                }






            }
            else
            {
                AddLog(20010015, sFilePath);
            }





            return returnDesignEntity;
        }




        public string GetReplaceContentFromDatabase(string sContent,DataReplaceEntityCRF dataEntity)
        {

            sContent = ReplaceParmsByDict(sContent, dataEntity.MainParms);

            if(sContent.IndexOf("[#循环开始]")>-1)
            {
                MatchCollection mc = Regex.Matches(sContent, "\\[#循环开始\\].*?\\[#循环结束\\]", RegexOptions.Singleline);
                
                
                for (int i = 0, j = mc.Count; i < j; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int n = 0, m = dataEntity.ListParms[i].Rows.Count; n < m; n++)
                    {

                        string sList = mc[i].Value.Replace("[#循环开始]", "").Replace("[#循环结束]","");

                        for (int q = 0, w = dataEntity.ListParms[i].Columns.Count; q < w; q++)
                        {
                            sList=sList.Replace("[#"+dataEntity.ListParms[i].Columns[q].ColumnName.Trim()+"]",dataEntity.ListParms[i].Rows[n][q].ToString());
                        }
                        sb.Append(sList);

                    }

                    sContent = sContent.Replace(mc[i].Value, sb.ToString());

                }



               


            }



            return sContent;

        }





        /// <summary>
        /// 
        /// Description: 添加到日志文件
        /// Author:Liudpc
        /// Create Date: 2010-6-10 17:50:18
        /// </summary>
        /// <param name="sLogId"></param>
        /// <param name="strParams"></param>
        private void AddLog(long sLogId, params string[] strParams)
        {



        }

    }
}
