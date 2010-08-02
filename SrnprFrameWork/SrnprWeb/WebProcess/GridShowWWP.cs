using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SrnprWeb.WebEntity;
using System.Data;
using System.IO;


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
                gsl.ItemList=new List<GridShowListItemWWE>();
                return gsl;
            }

        }

        public static void SaveList(WebEntity.GridShowListWWE gsl)
        {
            SrnprCommon.CommonFunction.EntitySerializerCCF<WebEntity.GridShowListWWE>.EntityToXml(gsl, sFilePath + GridShowList);
        }








        public  string GetResponseString(string sJson)
        {


            var t =  CommonFunction.JsonHelper.Deserialize<SrnprWeb.WebEntity.GridShowRequestWWE>(sJson);


            SrnprWeb.WebProcess.GridShowWWP gsw = new SrnprWeb.WebProcess.GridShowWWP();

            return CommonFunction.JsonHelper.Serialize<SrnprWeb.WebEntity.GridShowResponseWWE>(gsw.GetHtmlByEntity(GetEntityById(t.Id), t));
        }








        public WebEntity.GridShowWWE InitTemp()
        {
            GridShowWWE gsw = new GridShowWWE();
            gsw.TableInfo = new GridShowDataWWE();

            gsw.TableInfo.TableName = "Ope_SalesOrderList";
            gsw.TableInfo.DataBaseId = "SO";
            gsw.TableInfo.KeyColumn = "SalesOrderListId";



            gsw.ParamList = new List<GridShowParamWWE>();

           


            gsw.ColumnList = new List<GridShowColumnWWE>();

            gsw.ColumnList.Add(new GridShowColumnWWE() { ColumnData = "ProductName", HeaderText = "商品名称" });
            gsw.ColumnList.Add(new GridShowColumnWWE { ColumnData = "ProductId", HeaderText = "商品编号" });


            return gsw;


        }


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





        public DataTable GetDataByEntity(WebEntity.GridShowWWE gsw, long iPageIndex, long iPageSize)
        {

            string sSql = "select * from (select " + string.Join(",", gsw.ColumnList.Select(t => t.ColumnData).ToArray()) + " ,ROW_NUMBER() over(order by " + gsw.TableInfo.KeyColumn  + ") as srspdatapageno from " + gsw.TableInfo.TableName + gsw.TableInfo.WhereString + " )srspdatapagetable where srspdatapagetable.srspdatapageno between " + ((iPageIndex - 1) * iPageSize + 1).ToString() + " and " + (iPageIndex * iPageSize).ToString();






            return SrnprCommon.DataHelper.SqlHelperCDH.ExecuteDataTable(GetConnString(gsw.TableInfo.DataBaseId), sSql);


        }


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








        public GridShowResponseWWE GetHtmlByEntity(WebEntity.GridShowWWE gsw,GridShowRequestWWE request)
        {

            GridShowResponseWWE response = new GridShowResponseWWE();
            response.Request = request;



            DataTable dt = GetDataByEntity(gsw,request.PageIndex,request.PageSize);


            if (request.RowsCount == -1)
            {
                request.RowsCount = GetDataCount(gsw);
            }



            if (request.ProcessType == ""||request.ProcessType=="server")
            {
                StringBuilder sb = new StringBuilder();


             
                sb.Append("<div><table>");
                sb.Append("<tr>");
                for (int i = 0, j = gsw.ColumnList.Count; i < j; i++)
                {
                    sb.Append("<th>" + gsw.ColumnList[i].HeaderText + "</th>");
                }
                sb.Append("</tr>");

                for (int i = 0, j = dt.Rows.Count; i < j; i++)
                {
                    sb.Append("<tr>");


                    for (int n = 0, m = dt.Columns.Count-1; n < m; n++)
                    {
                        sb.Append("<td>" + dt.Rows[i][n].ToString() + "</td>");

                    }


                    sb.Append("</tr>");

                }

                sb.Append("</div></table>");

                sb.Append("<div>");





                sb.Append("</div>");



                sb.Append("");

                response.HtmlString = sb.ToString();

            }
            else if(request.ProcessType=="client")
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

            return response;

        }







    }
}
