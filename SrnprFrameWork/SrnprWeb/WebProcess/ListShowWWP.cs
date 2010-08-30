using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SrnprWeb.WebEntity;
using System.Data;

namespace SrnprWeb.WebProcess
{
    public class ListShowWWP:WebInterface.WidgetProcessWWI
    {




        public WebInterface.WidgetResponseWWI GetResponse(WebInterface.WidgetRequestWWI request, WidgetProcessWWE pro)
        {
            ListShowResponseWWE res = new ListShowResponseWWE();
            ListShowRequestWWE req = (ListShowRequestWWE)request;
            DataTable dt = pro.DataInfo;
            StringBuilder sb = new StringBuilder();

            /*
            switch (req.ShowType)
            {
                case "select":
                    for (int i = 0, j = dt.Rows.Count; i < j; i++)
                    {
                     sb.Append(string.Format("<option value='{1}'>{0}<option>",dt.Rows[i].ItemArray));
                    }
                    break;

            }
             */



            List<ItemKvdWWE> kvdList = new List<ItemKvdWWE>();
            int iColumnCount = dt.Columns.Count;

            int iValue=iColumnCount>1?1:0;
          

            for (int i = 0, j = dt.Rows.Count; i < j; i++)
            {
                kvdList.Add(new ItemKvdWWE() { K = dt.Rows[i][0].ToString().Trim(), V = dt.Rows[i][iValue].ToString().Trim(), D = (iColumnCount > 2 ? dt.Rows[i][2].ToString().Trim() : "") });

            }

            res.Kvd = kvdList;


            res.HtmlString = sb.ToString();




            return res;
        }

        #region WidgetProcessWWI 成员

       
        public WebInterface.WidgetResponseWWI GetResponse(WebInterface.WidgetRequestWWI req)
        {
            return GetResponse(req, null);
        }


        #endregion


        private string ReplaceRow(DataRow dr, string sInput)
        {
            return string.Format(sInput, dr.ItemArray);
        }

        
    }
}
