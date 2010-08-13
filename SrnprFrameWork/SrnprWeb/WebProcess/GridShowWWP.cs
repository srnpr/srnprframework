﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SrnprWeb.WebEntity;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;



namespace SrnprWeb.WebProcess
{
    public class GridShowWWP
    {

        private static string sFilePath = "D:\\SrnprFrameWork\\WebWidget\\";
        private static string sFileExt = "GridShow\\{0}.www.gs.xml";

        private static string GridShowList = "GridShowList\\GridShowList.xml";


        public static void SaveFileByEntity(WebEntity.GridShowWWE gsw)
        {
            SrnprCommon.CommonFunction.EntitySerializerCCF<WebEntity.GridShowWWE>.EntityToXml(gsw, sFilePath +string.Format(sFileExt,gsw.Id) );

        }

        public static WebEntity.GridShowWWE GetEntityById(string sId)
        {
            return SrnprCommon.CommonFunction.EntitySerializerCCF<WebEntity.GridShowWWE>.XmlToEntity(sFilePath + string.Format(sFileExt, sId));
        }


        public static WebEntity.GridShowListWWE GetList()
        {

            if (File.Exists(sFilePath + GridShowList))
            {
                return SrnprCommon.CommonFunction.EntitySerializerCCF<WebEntity.GridShowListWWE>.XmlToEntity(sFilePath + GridShowList);
            }
            else
            {
                WebEntity.GridShowListWWE gsl=new GridShowListWWE();
                gsl.ItemList=new List<ItemBaseWWE>();
                return gsl;
            }

        }

        public static void SaveList(WebEntity.GridShowListWWE gsl)
        {
            SrnprCommon.CommonFunction.EntitySerializerCCF<WebEntity.GridShowListWWE>.EntityToXml(gsl, sFilePath + GridShowList);
        }



        /// <summary>
        /// 
        /// Description: 返回显示内容
        /// Author:Liudpc
        /// Create Date: 2010-8-9 11:46:59
        /// </summary>
        /// <param name="sXmlId"></param>
        /// <param name="sClientId"></param>
        /// <returns></returns>
        public static string GetShowHtml(string sXmlId, string sClientId)
        {

            return GetShowHtml(sXmlId, sClientId,"");
        }

        public static string GetShowHtml(string sXmlId, string sClientId, string sRequest)
        {
            StringBuilder sb = new StringBuilder();

            //定义参数名称
            string sObjId = "SWJGSF_Obj_" + sClientId;
            //开始输出执行逻辑
            sb.Append("<div id=\"SWJGSF_Div_" + sClientId + "\"></div><input type=\"hidden\" name=\"SWJGSF_Hidden_" + sClientId + "\" id=\"SWJGSF_Hidden_" + sClientId + "\" value=\"" + sRequest + "\"><script>var " + sObjId + "=" + SrnprWeb.WebProcess.GridShowWWP.WidgetRequestString(sXmlId, sClientId) + ";SWJGSF.Init(" + sObjId + ");</script>");

            return sb.ToString();
        }









        public  string GetResponseString(string sJson)
        {


            var t =  CommonFunction.JsonHelper.Deserialize<SrnprWeb.WebEntity.GridShowRequestWWE>(sJson);

        
           // SrnprWeb.WebProcess.GridShowWWP gsw = new SrnprWeb.WebProcess.GridShowWWP();

            return CommonFunction.JsonHelper.Serialize<SrnprWeb.WebEntity.GridShowResponseWWE>(GetHtmlByEntity(GetEntityById(t.Id), t));
        }



        /// <summary>
        /// 
        /// Description: 得到处理数据集合
        /// Author:Liudpc
        /// Create Date: 2010-8-13 16:40:43
        /// </summary>
        /// <param name="sJson"></param>
        /// <param name="iType">0为当前页  1为所有页</param>
        /// <returns></returns>
        public  DataTable GetDataTable(string sJson,int iType)
        {
            var tJson = CommonFunction.JsonHelper.Deserialize<SrnprWeb.WebEntity.GridShowRequestWWE>(sJson);

            if (iType== 1)
            {
                tJson.PageSize = tJson.RowsCount;
                tJson.PageIndex = 1;

            }
           


            var Ent=GetEntityById(tJson.Id);

            DataTable dtOld= GetDataByEntity(Ent, tJson);


            List<string> remove=new List<string>();
            for (int i = 0, j = dtOld.Columns.Count; i < j; i++)
            {
                string sName = dtOld.Columns[i].ColumnName;

                var tc = Ent.ColumnList.SingleOrDefault(x => RecheckColumnName(x.ColumnData) == sName);

                if (tc != null)
                {

                    var te = tJson.ShowColumn.SingleOrDefault(x => x.Guid == tc.Guid);

                    if (te != null && GetSelectValue(te.ShowDisplay) == "d")
                    {
                        dtOld.Columns[i].ColumnName = te.HeaderText;

                    }
                    else
                    {
                        remove.Add(sName);
                    }

                }
                else
                {
                    remove.Add(sName);
                }


            }


            for (int i = 0, j = remove.Count; i < j; i++)
            {
                dtOld.Columns.Remove(remove[i]);
            }


                /*

            DataTable dt = new DataTable();
            dt.TableName = "Excel";


           


            List<ItemKvdWWE> kvdList = new List<ItemKvdWWE>();

            for (int i = 0, j = t.ShowColumn.Count; i < j; i++)
            {

                if (t.ShowColumn[i].ShowDisplay == GetSelectValue(t.ShowColumn[i].ShowDisplay))
                {
                    kvdList.Add(new ItemKvdWWE() { K = t.ShowColumn[i].Guid, D = t.ShowColumn[i].ShowDisplay, V = RecheckColumnName(Ent.ColumnList.Single(x => x.Guid == t.ShowColumn[i].Guid).ColumnData) });
                }

            }




            foreach (var k in kvdList)
            {
                dt.Columns.Add(k.D);
            }


            for (int i = 0, j = dtOld.Rows.Count; i < j; i++)
            {

            }


                */
            





            return dtOld;

        }







       


        /// <summary>
        /// 
        /// Description: 初始化信息
        /// Author:Liudpc
        /// Create Date: 2010-8-10 12:53:12
        /// </summary>
        /// <param name="sId"></param>
        /// <param name="sClientId"></param>
        /// <returns></returns>
        public static string WidgetRequestString(string sId,string sClientId)
        {
            WebEntity.GridShowRequestWWE req = new GridShowRequestWWE();

            req.Id = sId;
            req.PageIndex = 1;
            req.PageSize = 10;
            req.ProcessType = "";
            req.RowsCount = -1;
            req.ClientId = sClientId;

            return CommonFunction.JsonHelper.Serialize<WebEntity.GridShowRequestWWE>(req);


        }





        /// <summary>
        /// 
        /// Description: 根据实体取出数据
        /// Author:Liudpc
        /// Create Date: 2010-8-10 12:52:35
        /// </summary>
        /// <param name="gsw"></param>
        /// <param name="req"></param>
        /// <param name="sOrdeString"></param>
        /// <returns></returns>
        public DataTable GetDataByEntity(WebEntity.GridShowWWE gsw, GridShowRequestWWE req)
        {


            string sOrdeString = "";

            //开始智能分析排序字段
            if (req.ShowColumn.Count > 0)
            {

                var vSort = req.ShowColumn.FirstOrDefault(t => (t.OrderType == "a" || t.OrderType == "e"));
                if (vSort == null)
                {
                    vSort = gsw.ColumnList.FirstOrDefault(x => ReckeckOrderColumn(x.ColumnData) != "");
                }
                sOrdeString = ReckeckOrderColumn(gsw.ColumnList.SingleOrDefault(t => t.Guid == vSort.Guid).ColumnData) + (vSort.OrderType == "a" ? " asc " : " desc ");
            }



            string sWhere = gsw.TableInfo.WhereString;

            Dictionary<string, string> dQuery = new Dictionary<string, string>();

            if (req.QueryDict!=null&& req.QueryDict.Count > 0)
            {
                List<string> listStr = new List<string>();
                foreach (KeyValuePair<string, string> kvp in req.QueryDict)
                {

                    var o = gsw.ParamList.FirstOrDefault(t => t.ParamName == kvp.Key);
                    if (o != null && !string.IsNullOrEmpty(o.ParamName) && !string.IsNullOrEmpty(o.ColumnField))
                    {
                        string sParam = "";
                        switch (o.ParamQueryType)
                        {
                            case "a":
                                sParam += " and ";
                                break;
                            case "o":
                                sParam += " or ";
                                break;
                            case "d":
                            default:
                                sParam += " ";
                                break;
                        }


                        switch (o.ParamOperator)
                        {

                            case "e":
                                sParam += o.ColumnField+"=@"+kvp.Key;
                                dQuery.Add(kvp.Key, kvp.Value);
                                
                                break;
                            case "b":
                                sParam += o.ColumnField + ">@" + kvp.Key;
                                dQuery.Add(kvp.Key, kvp.Value);
                                break;
                            case "s":
                                sParam += o.ColumnField + "<@" + kvp.Key;
                                dQuery.Add(kvp.Key, kvp.Value);
                                break;
                            case "l":
                                sParam += o.ColumnField + " like @" + kvp.Key;

                                dQuery.Add(kvp.Key, "%" + kvp.Value + "%");
                               
                                break;
                            case "d":
                            default:
                                sParam += o.ParamName;
                                break;
                        }

                        listStr.Add(sParam);

                    }


                }


                




                if (listStr.Count > 0)
                {

                    sWhere += (string.IsNullOrEmpty(sWhere)?" 1=1" :"")+ string.Join(" ", listStr.ToArray());

                }


                
            }




            


            if (!string.IsNullOrEmpty(sWhere))
            {
                sWhere = " where " + sWhere;
            }







            string sGroupWhere = sWhere;


            if (!string.IsNullOrEmpty(gsw.TableInfo.GroupColumn) && !string.IsNullOrEmpty(req.GroupValue))
            {
                string sGroup = gsw.TableInfo.GroupColumn.Split(',')[0];

                dQuery[sGroup] = req.GroupValue;
                sWhere += (string.IsNullOrEmpty(sWhere) ? " where " : " and ") + sGroup + "=@" + sGroup;
            }



            if (req.RowsCount == -1)
            {
                if (!string.IsNullOrEmpty(gsw.TableInfo.GroupColumn))
                {
                    string[] strSplit = gsw.TableInfo.GroupColumn.Split(',');

                    if (strSplit.Length > 0)
                    {
                        string sField, sGroup, sSum;
                        if (strSplit.Length == 1)
                        {
                            sGroup = strSplit[0];
                            sField = sGroup = strSplit[0];
                            sSum = "1";
                        }
                        else if (strSplit.Length == 2)
                        {
                            sGroup = strSplit[0];
                            sField = strSplit[1];
                            sSum = "1";
                        }
                        else
                        {
                            sGroup = strSplit[0];
                            sField = strSplit[1];
                            sSum = strSplit[2];
                        }



                        string sGroupSql = "select (" + sGroup + ") as k,sum(" + sSum + ") as v, (" + sField + ") as d from " + gsw.TableInfo.TableName + sGroupWhere + " group by " + sGroup;

                        DataTable dt = SrnprCommon.DataHelper.SqlHelperCDH.ExecuteDataTable(GetConnString(gsw.TableInfo.DataBaseId), sGroupSql);
                       

                        List<WebEntity.ItemKvdWWE> kvdList = new List<ItemKvdWWE>();

                        for (int i = 0, j = dt.Rows.Count; i < j; i++)
                        {
                            kvdList.Add(new ItemKvdWWE() { K = dt.Rows[i][0].ToString().Trim(), V = dt.Rows[i][1].ToString().Trim(), D = dt.Rows[i][2].ToString().Trim() });
                        }
                        req.GroupKvd = kvdList;



                    }
                }

            }



            if (req.RowsCount < 0)
            {
                string sSqlCount = "select count(1) from " + gsw.TableInfo.TableName + sWhere;
                req.RowsCount = long.Parse(SrnprCommon.DataHelper.SqlHelperCDH.ExecuteScalar(GetConnString(gsw.TableInfo.DataBaseId), sSqlCount, dQuery).ToString());
            }
                




            string sSql = "select * from (select " + string.Join(",", gsw.ColumnList.Select(t => t.ColumnData).ToArray()) + " ,ROW_NUMBER() over(order by " + sOrdeString + ") as srspdatapageno from " + gsw.TableInfo.TableName + sWhere + " )srspdatapagetable where srspdatapagetable.srspdatapageno between " + ((req.PageIndex - 1) * req.PageSize + 1).ToString() + " and " + (req.PageIndex * req.PageSize).ToString();
            return SrnprCommon.DataHelper.SqlHelperCDH.ExecuteDataTable(GetConnString(gsw.TableInfo.DataBaseId), sSql, dQuery);


        }


        /// <summary>
        /// 
        /// Description: 得到连接字符串
        /// Author:Liudpc
        /// Create Date: 2010-8-3 10:35:44
        /// </summary>
        /// <param name="sDataBaseId"></param>
        /// <returns></returns>
        public string GetConnString(string sDataBaseId)
        {
            return System.Configuration.ConfigurationSettings.AppSettings[sDataBaseId].Trim();
        }


        /// <summary>
        /// 
        /// Description: 得到 数据集的总数
        /// Author:Liudpc
        /// Create Date: 2010-8-2 13:10:24
        /// </summary>
        /// <param name="gsw"></param>
        /// <returns></returns>
        public long GetDataCount(WebEntity.GridShowWWE gsw)
        {
            string sSql = "select count(1) from "+gsw.TableInfo.TableName+gsw.TableInfo.WhereString;
            return long.Parse(SrnprCommon.DataHelper.SqlHelperCDH.ExecuteScalar(GetConnString(gsw.TableInfo.DataBaseId), sSql).ToString());

        }






        private string GetSelectValue(string s)
        {
            return string.IsNullOrEmpty(s) ? "d" : s;

        }


        public GridShowResponseWWE GetHtmlByEntity(WebEntity.GridShowWWE gsw,GridShowRequestWWE request)
        {

            GridShowResponseWWE response = new GridShowResponseWWE();
            response.Request = request;


            if (gsw != null)
            {
                if (request.ShowColumn == null)
                {
                    request.ShowColumn = new List<GridShowColumnBaseWWE>();

                    foreach (var t in gsw.ColumnList)
                    {
                        if (t.ShowDisplay != "h")
                        {
                            request.ShowColumn.Add(new GridShowColumnBaseWWE() { Guid = t.Guid, HeaderText = t.HeaderText, OrderType = t.OrderType, ShowDisplay = t.ShowDisplay });
                        }
                    }
                }

               



                DataTable dt = new DataTable();


                if (request.ProcessType == "" || request.ProcessType == "server")
                {

                    //开始分析排序依据
                    dt = GetDataByEntity(gsw, request);


                    StringBuilder sb = new StringBuilder();



                    sb.Append("<div class=\"SWCGSF_DIV_MAIN\"><table id=\"GS_table_" + request.ClientId + "\" cellspacing=\"1\" cellpadding=\"0\">");




                    //定义显示
                    Dictionary<string, string> dShow = new Dictionary<string, string>();

                    Dictionary<string, string> dOrder = new Dictionary<string, string>();


                    if (request.ShowColumn.Count > 0)
                    {

                        for (int i = 0, j = request.ShowColumn.Count; i < j; i++)
                        {
                            dShow.Add(request.ShowColumn[i].Guid, request.ShowColumn[i].ShowDisplay);
                            dOrder.Add(request.ShowColumn[i].Guid, request.ShowColumn[i].OrderType);
                        }

                    }



                    int iColumnCount = gsw.ColumnList.Count;



                    if (iColumnCount > 0)
                    {



                        sb.Append("<tr>");
                        for (int i = 0; i < iColumnCount; i++)
                        {

                            if (dShow[gsw.ColumnList[i].Guid] != "h" && dShow[gsw.ColumnList[i].Guid] != "n")
                            {
                                bool bIsOrder = string.IsNullOrEmpty(ReckeckOrderColumn(gsw.ColumnList[i].ColumnData));

                                var vSort = request.ShowColumn.SingleOrDefault(t => t.Guid == gsw.ColumnList[i].Guid);



                                string sOrderType = string.IsNullOrEmpty(vSort.OrderType) ? "d" : vSort.OrderType; ;

                                if (string.IsNullOrEmpty(ReckeckOrderColumn(gsw.ColumnList[i].ColumnData)))
                                {
                                    sOrderType = "n";
                                }






                                string sSortVisgn = "";





                                switch (sOrderType)
                                {
                                    case "d":
                                        sSortVisgn = " <a href=\"javascript:SWJGSF.Sort('" + gsw.Id + "','" + vSort.Guid + "')\"> " + vSort.HeaderText + "</a>";
                                        break;
                                    case "a":
                                        sSortVisgn = " <a href=\"javascript:SWJGSF.Sort('" + gsw.Id + "','" + vSort.Guid + "')\"> " + vSort.HeaderText + "</a>↑";
                                        break;
                                    case "e":
                                        sSortVisgn = " <a href=\"javascript:SWJGSF.Sort('" + gsw.Id + "','" + vSort.Guid + "')\"> " + vSort.HeaderText + "</a>↓";
                                        break;
                                    case "n":
                                    default:
                                        sSortVisgn = vSort.HeaderText;
                                        break;
                                }






                                sb.Append("<th class=\"SWCGSF_TABLE_" + sOrderType + "\" " + (string.IsNullOrEmpty(gsw.ColumnList[i].Width) ? "" : gsw.ColumnList[i].Width) + " >" + sSortVisgn + "</th>");
                            }

                        }
                        sb.Append("</tr>");






                        for (int i = 0, j = dt.Rows.Count; i < j; i++)
                        {

                            sb.Append("<tr class=\"SWCGSF_TR_"+(i%2)+"\">");
                            for (int n = 0; n < iColumnCount; n++)
                            {

                                if (dShow[gsw.ColumnList[n].Guid] != "h" && dShow[gsw.ColumnList[n].Guid] != "n")
                                {
                                    sb.Append("<td >");

                                    if (!string.IsNullOrEmpty(gsw.ColumnList[n].ColumnData))
                                    {

                                        string sData = RecheckColumnName(gsw.ColumnList[n].ColumnData);



                                        switch (gsw.ColumnList[n].ColumnType)
                                        {
                                            case "r":
                                                sb.Append("<input type=\"radio\" name=\"" + request.ClientId + "_column_" + gsw.ColumnList[n].Guid + "\" value=\"" + dt.Rows[i][sData].ToString().Trim() + "\" />" + GetDataRowReplace(gsw.ColumnList[n].ColumnShow, dt.Rows[i]));
                                                break;
                                            case "c":
                                                sb.Append("<input type=\"checkbox\" name=\"" + request.ClientId + "_column_" + gsw.ColumnList[n].Guid + "\" value=\"" + dt.Rows[i][sData].ToString().Trim() + "\" />" + GetDataRowReplace(gsw.ColumnList[n].ColumnShow, dt.Rows[i]));
                                                break;
                                            case "l":
                                                sb.Append("<a target=\"_blank\" href=\"" + GetDataRowReplace(gsw.ColumnList[n].ColumnShow, dt.Rows[i]).Replace("[this]", dt.Rows[i][sData].ToString().Trim()) + "\">" + dt.Rows[i][sData].ToString().Trim() + "</a>");
                                                break;
                                            case "d":
                                            default:
                                                sb.Append(dt.Rows[i][sData].ToString().Trim());
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        sb.Append(GetDataRowReplace(gsw.ColumnList[n].ColumnShow, dt.Rows[i]));
                                    }



                                    sb.Append("</td>");
                                }




                            }
                            sb.Append("</tr>");
                        }





                    }

                    sb.Append("</table></div>");


                    response.HtmlString = sb.ToString();

                }
                else if (request.ProcessType == "client")
                {
                    response.DataItem = new List<List<string>>();

                    for (int i = 0, j = dt.Rows.Count; i < j; i++)
                    {
                        List<string> strList = new List<string>();
                        for (int n = 0, m = dt.Columns.Count; n < m; n++)
                        {
                            strList.Add(dt.Rows[i][n].ToString().Trim());

                        }
                        response.DataItem.Add(strList);
                    }
                }
                else if (request.ProcessType == "demo")
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<div class=\"SWCGSF_DIV_MAIN\"><table id=\"GS_table_" + request.ClientId + "\" cellspacing=\"1\" cellpadding=\"0\">");

                    List<string> lTd = new List<string>();

                    sb.Append("<tr>");
                    for (int i = 0, j = request.ShowColumn.Count; i < j; i++)
                    {
                        if (GetSelectValue(request.ShowColumn[i].ShowDisplay) == "d")
                        {
                            sb.Append("<th>" + request.ShowColumn[i].HeaderText + "</th>");

                            lTd.Add("<td></td>");
                        }
                    }
                    sb.Append("</tr>");


                    string sTd = "<tr {0}>" + string.Join("", lTd.ToArray()) + "</tr>";

                    for (int i = 0; i < request.PageSize; i++)
                    {
                        sb.Append(string.Format( sTd," class=\"SWCGSF_TR_"+(i%2)+"\""));
                    }


                    sb.Append("</table></div>");


                    response.HtmlString = sb.ToString();
                }
            }
           

            return response;

        }


        private string RecheckColumnName(string sString)
        {
            return sString.Substring(sString.LastIndexOf(" as ") == -1 ? 0 : (sString.LastIndexOf(" as ") + 4)).Trim();
        }


        private string ReckeckOrderColumn(string sString)
        {
            sString = sString.ToLower();

            if (!string.IsNullOrEmpty(sString)&& sString.IndexOf(" from ") == -1)
            {
                return sString.Substring(0, sString.IndexOf(" as ") == -1 ? (sString.Length) : sString.LastIndexOf(" as ")).Trim();
            }
            else
            {
                return "";
            }
        }


        /// <summary>
        /// 
        /// Description: 根据列绑定
        /// Author:Liudpc
        /// Create Date: 2010-8-3 16:12:34
        /// </summary>
        /// <param name="sString"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
        private string GetDataRowReplace(string sString, DataRow dr)
        {

            if (sString.IndexOf("[#") > -1)
            {

               MatchCollection mc =  Regex.Matches(sString, "[#.*?]");


               for (int i = 0, j = mc.Count; i < j; i++)
               {

                   string sObj=mc[i].Value.Substring(2, mc[i].Length - 3);

                   if (dr[sObj] != null)
                   {
                       sString = sString.Replace(mc[i].Value, dr[sObj].ToString().Trim());
                   }

               }

            }

            return sString;

        }






        private Dictionary<string, string> RecheckJsonDic(string sJson)
        {
            Dictionary<string, string> d=new Dictionary<string,string>();

           



            //var t=Regex.Matches("(?!\")(?<=\").*?(?=\\\")(?!\\\")";









            return d;
        }
       






    }
}
