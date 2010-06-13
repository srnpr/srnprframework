using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SrnprCommon;
using System.Text;
using SrnprCommon.ReplaceFile;

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
            btnTestSend.Visible = false;
        }


        
             //se.GetTempleteDesign(sXmlId);
            StringBuilder sb = new StringBuilder();

            foreach (SrnprCommon.ReplaceFile.ItemPramEntityCRF ipe in se.GetTempleteCode(sXmlId).Parm)
            {

                if (!string.IsNullOrEmpty(Request[sInputNameLeft + ipe.ParmName]))
                {
                    sb.Append("<li>" + ipe.ParmText + "：<input type=\"text\" name=\"" + sInputNameLeft + ipe.ParmName + "\" value=\"" + Request[sInputNameLeft + ipe.ParmName] + "\"></li>");
                }
                else
                {
                    sb.Append("<li>" + ipe.ParmText + "：<input type=\"text\" name=\"" + sInputNameLeft + ipe.ParmName + "\"></li>");
                }
            }


            lbInput.Text = sb.ToString().Trim();
       

        

    }


    private string GetParms()
    {
        List<string> lStr = new List<string>();
        foreach (SrnprCommon.ReplaceFile.ItemPramEntityCRF ipe in se.GetTempleteCode(sXmlId).Parm)
        {
            if (!string.IsNullOrEmpty(Request[sInputNameLeft + ipe.ParmName]))
            {
                lStr.Add(ipe.ParmName + "=" + Request[sInputNameLeft + ipe.ParmName]);
            }
            else
            {
                lStr.Add(ipe.ParmName + "=");
            }
        }

        return string.Join(SrnprCommon.CommonConfig.ReplaceFileConfigCCC.Config.SplitString, lStr.ToArray());
    }




    protected void btnTest_Click(object sender, EventArgs e)
    {


        



       List<DoSendEmailEntityCRF> doSend=  se.GetSendList(sXmlId, GetParms());

       lbCount.Text = doSend.Count.ToString();

       rpList.DataSource = doSend;
       rpList.DataBind();

       if (doSend.Count > 0)
       {
           btnTestSend.Visible = true;

       }


    }
    protected void btnTestSend_Click(object sender, EventArgs e)
    {
        

            se.Send(sXmlId, GetParms());

        
    }
}
