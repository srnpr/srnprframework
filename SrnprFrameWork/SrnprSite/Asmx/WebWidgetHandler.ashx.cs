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




            

          




            string sRes = SrnprWeb.WebProcess.WidgetProcessWWP.Response(req);

            context.Response.Write(sRes);
        }




        public Dictionary<int, SrnprWeb.WebEntity.WidgetProcessWWE> DicProcess(SrnprWeb.WebEntity.WidgetRequestWWE req)
        {

            Dictionary<int, SrnprWeb.WebEntity.WidgetProcessWWE> dic = new Dictionary<int, SrnprWeb.WebEntity.WidgetProcessWWE>();



            for (int i = 0, j = req.RQ.Count; i < j; i++)
            {

                switch (req.RQ[i].WidgetType)
                {

                    case "LS":

                        switch (req.RQ[i].Id)
                        {
                            case "":


                                break;
                        }
                        break;
                }



            }








            return dic;
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
