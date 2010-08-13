using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace SrnprSite.Web.GridShow
{
    public partial class Excel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            string s =HttpContext.Current.Request["json"];


            DataTable dt = new SrnprWeb.WebProcess.GridShowWWP().GetDataTable(s);

            HttpResponse resp;
            resp = HttpContext.Current.Response;
            resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            resp.AppendHeader("Content-Disposition", "attachment;filename=" + "11.xsls");



            int iColumnCount = dt.Columns.Count;



            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {

                List<string> lStr = new List<string>();
                for (int i = 0; i < iColumnCount; i++)
                {
                    lStr.Add(dr[i].ToString().Trim());
                }

                sb.Append(string.Join("\t", lStr.ToArray()) + "\n");

            }

            resp.Write(sb.ToString());
        }
    }
}
