<%@ WebHandler Language="C#" Class="GridShowHander" %>

using System;
using System.Web;
using System.Text;
using System.Data;

public class GridShowHander : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string s = context.Request["json"];
        context.Response.Write(new SrnprWeb.WebProcess.GridShowWWP().GetResponseString(s));
        
    }



    
    
    
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
