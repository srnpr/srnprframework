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
        //string s= DateTime.Parse("2010-3-31 12:00:00").AddMonths(-1).ToString().Trim();



        int i = WidgetFileUploadWWW1.FileInfo.Count;


        int i2 = WidgetFileUploadWWW2.FileInfo.Count;
        if (!IsPostBack)
        {

            // List<SrnprWeb.WebEntity.FileUploadInfoWWE> t = new List<SrnprWeb.WebEntity.FileUploadInfoWWE>();
            // t.Add(new SrnprWeb.WebEntity.FileUploadInfoWWE() { FileName = "aa", FileUrl = "bb" });
            // WidgetFileUploadWWW2.FileInfo = t;



            //WidgetFileUploadWWW2.FileInfo.Add(new SrnprWeb.WebEntity.FileUploadInfoWWE() { FileName = "aa", FileUrl = "bb" });

        }


        Response.Write(HttpUtility.HtmlDecode(WidgetFileUploadWWW2.GetHtml("")));

        int i3 = WidgetFileUploadWWW3.FileInfo.Count;



    }
}
