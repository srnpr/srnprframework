using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace SrnprSite.Ashx
{
    public partial class Excel : System.Web.UI.Page
    {

        protected string sId = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            sId = Request["id"].ToString().Trim();
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            string s = hfJson.Value.Trim();


            DataTable dt = new SrnprWeb.WebProcess.GridShowWWP().GetDataTable(s,0);

            DataTableToExcel(dt, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".xls");
        }


        protected void DataTableToExcel(DataTable dt, string sFileName)
        {

            HttpResponse resp;
            resp = System.Web.HttpContext.Current.Response;
            resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            resp.AppendHeader("Content-Disposition", "attachment;filename=" + sFileName);

            StringBuilder sb = new StringBuilder();

            sb.Append("<table>");

            int iColumnLength = dt.Columns.Count;


            List<string> strList = new List<string>();

            for (int i = 0; i < iColumnLength; i++)
            {
                strList.Add("<th>"+dt.Columns[i].ColumnName+"</th>");

            }
            sb.Append("<tr>"+string.Join("", strList.ToArray()) + "</tr>");

            for (int i = 0, j = dt.Rows.Count; i < j; i++)
            {

                strList = new List<string>();
                for (int n = 0; n < iColumnLength; n++)
                {
                    strList.Add("<td style=\"vnd.ms-excel.numberformat: @;\">"+dt.Rows[i][n].ToString()+"</td>");

                }

                sb.Append("<tr>"+string.Join("", strList.ToArray()) + "</tr>");
            }


            sb.Append("</table>");



            resp.Write(sb.ToString());

            //写缓冲区中的数据到HTTP头文件中 
            resp.End();

        }

        protected void btnExcelAll_Click(object sender, EventArgs e)
        {
            string s = hfJson.Value.Trim();
            DataTable dt = new SrnprWeb.WebProcess.GridShowWWP().GetDataTable(s, 1);

            DataTableToExcel(dt, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")+ ".xls");
        }

    }
}
