using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

namespace SrnprSite.Asmx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
   
    public class GridShowExcel : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {



            string s = context.Request["json"];
           

            DataTable dt = new SrnprWeb.WebProcess.GridShowWWP().GetDataTable(s);
            
            HttpResponse resp;
            resp = context.Response;
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




            
            //写缓冲区中的数据到HTTP头文件中 
            resp.End();




        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
