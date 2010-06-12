using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DcEmail_DcEmailList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

       

        if (!IsPostBack)
        {
            BindRP();
        }


    }

    private void BindRP()
    {

        rpList.DataSource = new SendEmail.SendEmail().GetListFileInfoByFilePath();
        rpList.DataBind();
       
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
         new SendEmail.SendEmail().RecheckAllEmailFile();

        BindRP();

    }
}
