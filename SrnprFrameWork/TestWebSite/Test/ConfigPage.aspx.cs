using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SrnprWeb;

public partial class Test_ConfigPage : System.Web.UI.Page
{

    private string sId = "tttt";
    protected void Page_Load(object sender, EventArgs e)
    {



        if (!IsPostBack)
        {
            tbEditor.Text = SrnprWeb.WebProcess.PageShowWWP.GetEntityById(sId).HtmlContent;
        }



    }
    protected void btnOk_Click(object sender, EventArgs e)
    {

        string sText = tbEditor.Text.Trim();




        SrnprWeb.WebEntity.PageShowWWE psw = new SrnprWeb.WebEntity.PageShowWWE();

        psw.Id = sId;

        psw.Guid = Guid.NewGuid().ToString();
        psw.HtmlContent = sText;


        SrnprWeb.WebProcess.PageShowWWP.SaveFileByEntity(psw);




    }
}
