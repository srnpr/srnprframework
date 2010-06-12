using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SrnprCommon.ReplaceFile;
using System.Text;

public partial class DcEmail_DcEmailDesign : System.Web.UI.Page
{
    string sId;
    protected void Page_Load(object sender, EventArgs e)
    {

        sId = Request["Id"].ToString().Trim();


        if (!IsPostBack)
        {
            BindRP();
        }

    }



    protected void BindRP()
    {
        SendEmail.SendEmail se = new SendEmail.SendEmail();

        SendEmail.EmailDesignInfo edi = se.GetDesign(sId);

        lbParmsShow.Text = edi.Parms;

        rpList.DataSource = edi.ListItem;
        rpList.DataBind();
    }


}
