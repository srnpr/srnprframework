using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
      
       
       //Panel
        for (int i = 0, j = 100; i < 100; i++)
        {
            Response.Write("<iframe src=\"http://10.1.120."+i.ToString()+"\"></iframe>");
        }
    }
}
