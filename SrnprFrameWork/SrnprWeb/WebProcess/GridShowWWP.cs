using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SrnprWeb.WebEntity;
using System.Data;

namespace SrnprWeb.WebProcess
{
    public class GridShowWWP
    {







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



        public DataTable GetDataByEntity(WebEntity.GridShowWWE gsw, long iPageIndex, long iPageSize)
        {

            string sSql = "select * from (select " + string.Join(",", gsw.ColumnList.Select(t => t.ColumnData).ToArray()) + " ,ROW_NUMBER() over(order by " + gsw.TableInfo.KeyColumn  + ") as srspdatapageno from " + gsw.TableInfo.TableName + gsw.TableInfo.WhereString + " )srspdatapagetable where srspdatapagetable.srspdatapageno between " + ((iPageIndex - 1) * iPageSize + 1).ToString() + " and " + (iPageIndex * iPageSize).ToString();


            



            return SrnprCommon.DataHelper.SqlHelperCDH.ExecuteDataTable(System.Configuration.ConfigurationSettings.AppSettings[gsw.TableInfo.DataBaseId].Trim(), sSql);


        }







        public GridShowResponseWWE GetHtmlByEntity(WebEntity.GridShowWWE gsw,GridShowRequestWWE request)
        {

            GridShowResponseWWE response = new GridShowResponseWWE();
            response.Request = request;



            DataTable dt = GetDataByEntity(gsw,request.PageIndex,request.PageSize);



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
