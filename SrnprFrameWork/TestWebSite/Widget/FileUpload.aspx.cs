using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widget_FileUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        int i=WidgetFileUploadWWW1.FileInfo.Count;


        int i2 = WidgetFileUploadWWW2.FileInfo.Count;
        if (!IsPostBack)
        {
            
            List<SrnprWeb.WebEntity.FileUploadInfoWWE> t = new List<SrnprWeb.WebEntity.FileUploadInfoWWE>();
            t.Add(new SrnprWeb.WebEntity.FileUploadInfoWWE() { FileName = "aa", FileUrl = "bb" });
            WidgetFileUploadWWW2.FileInfo = t;

            

            //WidgetFileUploadWWW2.FileInfo.Add(new SrnprWeb.WebEntity.FileUploadInfoWWE() { FileName = "aa", FileUrl = "bb" });
           
        }


        int i3=WidgetFileUploadWWW3.FileInfo.Count;


        
    }
}
