<%@ WebHandler Language="C#" Class="GridShowHander" %>

using System;
using System.Web;
using System.Text;
using System.Data;

public class GridShowHander : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";





        string s = context.Request["json"];






        var t=JSONHelper.Deserialize<SrnprWeb.WebEntity.GridShowRequestWWE>(s);
        
        context.Response.Write(GetData());


        
        
        
        
    }



    public string GetData()
    {
        
        /*
        DataTable dt = new DataTable();
        dt.TableName = "gridshow";
        dt.Columns.Add("A");
        DataRow dr=dt.Rows.Add("a1");
        
        
        return JSONHelper.Serialize<DataTable>(dt);
         */

        TestListString tls = new TestListString();

        tls.ListString = new System.Collections.Generic.List<System.Collections.Generic.List<string>>();
        tls.ListString.Add(new System.Collections.Generic.List<string>() { "a", "f" });
        tls.ListString.Add(new System.Collections.Generic.List<string>() { "s", "dd\"afd" });

        return JSONHelper.Serialize<TestListString>(tls);
        
        
        
    }
    
    

    public string TTT()
    {
        return JSONHelper.Serialize<TestUser>(new TestUser { Name = "aa", Age = 11 });

    }


    public string GetResponse()
    {
        StringBuilder sb = new StringBuilder();


        
        

        return sb.ToString();
        
    }
    
    
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
