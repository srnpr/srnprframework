using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;

namespace SrnprSite.Asmx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    
    public class WebWidgetHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string s = context.Request["json"];




            SrnprWeb.WebEntity.WidgetRequestWWE req = SrnprWeb.WebProcess.WidgetProcessWWP.DeserializeRequest(s);











            string sRes = SrnprWeb.WebProcess.WidgetProcessWWP.Response(req, DicProcess(req));

            context.Response.Write(sRes);
        }




        /// <summary>
        /// 
        /// Description: 返回附加的数据
        /// Author:Liudpc
        /// Create Date: 2010-8-27 10:38:44
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public Dictionary<int, SrnprWeb.WebEntity.WidgetProcessWWE> DicProcess(SrnprWeb.WebEntity.WidgetRequestWWE req)
        {

            Dictionary<int, SrnprWeb.WebEntity.WidgetProcessWWE> dic = new Dictionary<int, SrnprWeb.WebEntity.WidgetProcessWWE>();



            for (int i = 0, j = req.RQ.Count; i < j; i++)
            {

                switch (req.RQ[i].WidgetType)
                {

                    case "LS":

                        DataTable dt = GetTableById(req.RQ[i].Id);

                        if (dt != null)
                        {
                            dic[i] = new SrnprWeb.WebEntity.WidgetProcessWWE();
                            dic[i].DataInfo = dt;
                        }

                        break;
                }



            }








            return dic;
        }




        public DataTable GetTableById(string sId)
        {

            DataTable dt = new DataTable();

            switch (sId)
            {
                case "abc":


                    List<SrnprWeb.WebEntity.ItemKvdWWE> kvd = new List<SrnprWeb.WebEntity.ItemKvdWWE>();
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { K = "kkk", V = "vvv", D = "ddd" });

                    dt = SrnprCommon.CommonFunction.ListStaticCCF.ListToDataTable<SrnprWeb.WebEntity.ItemKvdWWE>(kvd);



                    break;
                case "":

                    break;
            }

            return dt;

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
