using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprWeb.WebProcess
{
    public class GridShowWWP
    {

        public string GetHtmlByEntity(WebEntity.GridShowWWE gsw)
        {


            StringBuilder sb=new StringBuilder();


            sb.Append("<table>");
            sb.Append("<tr>");
            for (int i = 0, j = gsw.ColumnList.Count; i < j; i++)
            {
                sb.Append("<th>" + gsw.ColumnList[i].HeaderText + "</th>");
            }
            sb.Append("</tr>");











            sb.Append("</table>");







            return sb.ToString();

        }







    }
}
