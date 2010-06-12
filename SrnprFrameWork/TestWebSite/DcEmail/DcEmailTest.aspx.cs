using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SrnprCommon;
using System.Text;

public partial class DcEmail_DcEmailTest : System.Web.UI.Page
{
    string sXmlId = "";

    string sInputNameLeft = "dev_dcemailtest_input_parms_";

    SendEmail.SendEmail se = new SendEmail.SendEmail();
    protected void Page_Load(object sender, EventArgs e)
    {
        sXmlId = Request["id"].ToString().Trim();

        if (!IsPostBack)
        {

             //se.GetTempleteDesign(sXmlId);
            StringBuilder sb = new StringBuilder();

            foreach (SrnprCommon.ReplaceFile.ItemPramEntityCRF ipe in se.GetTempleteCode(sXmlId).Parm)
            {
                sb.Append("<li>"+ipe.ParmText+"：<input type=\"text\" name=\""+sInputNameLeft+ipe.ParmName+"\"></li>");
            }


            lbInput.Text = sb.ToString().Trim();
        }

        

    }

    protected void btnTest_Click(object sender, EventArgs e)
    {

    }
}
