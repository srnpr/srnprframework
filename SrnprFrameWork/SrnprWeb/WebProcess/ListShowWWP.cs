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




        /// <summary>
        /// 
        /// Author:Liudpc
        /// Create Date: 2010-9-2 13:42:28
        /// Description: 得到输出的信息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="pro"></param>
        /// <returns></returns>
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



        /// <summary>
        /// 
        /// Author:Liudpc
        /// Create Date: 2010-9-2 13:41:57
        /// Description: 返回生成的HTML
        /// </summary>
        /// <returns></returns>
        public static string GetShowHtml(Dictionary<string,string> dObj)
        {




            return "<span id=\""+dObj["SId"]+"\"></span>"+CommonFunction.JSHelper.CreateScriptDefer( WebProcess.WidgetProcessWWP.SwwJsBaseName("I", CommonFunction.JsonHelper.SerializeDic(dObj)));
        }


        #region WidgetProcessWWI 成员

       
        public WebInterface.WidgetResponseWWI GetResponse(WebInterface.WidgetRequestWWI req)
        {
            return GetResponse(req, null);
        }


        #endregion


       

        
    }
}
