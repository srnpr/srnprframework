using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
            string sRes = SrnprWeb.WebProcess.WidgetProcessWWP.Response(s);

            context.Response.Write(sRes);
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
