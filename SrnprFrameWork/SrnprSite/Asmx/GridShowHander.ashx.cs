using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SrnprSite.Asmx
{
   
    public class GridShowHander : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string s = context.Request["json"];
            context.Response.Write(new SrnprWeb.WebProcess.GridShowWWP().GetResponseString(s));

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
