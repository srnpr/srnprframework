using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SrnprCommon.ReplaceFile;
using System.Text;
using SendEmail;

public partial class DcEmail_DcEmailDesign : System.Web.UI.Page
{
    string sId;
    protected void Page_Load(object sender, EventArgs e)
    {

        sId = Request["Id"].ToString().Trim();


        if (!IsPostBack)
        {
            BindRP();
            pShow.Visible = false;
        }




        if (!string.IsNullOrEmpty(Request["dev_dcemail_submit_type"]))
        {
            string sGuid=Request["dev_dcemail_submit_tempguid"].ToString().Trim();
            switch (int.Parse(Request["dev_dcemail_submit_type"]))
            {

                case 1:
                    pShow.Visible = true;
                    hfTempId.Value = "";
                    tbContent.Text = "";
                    tbRuleExpress.Text = "";
                    tbTitle.Text = "";
                    tbToEmail.Text = "";

                    break;
                case 2:
                    pShow.Visible = true;
                    hfTempId.Value = sGuid;


                    EmailDesignInfo eInfo= new SendEmail.SendEmail().GetDesign(sId);

                    EmailDesignItem eItem = eInfo.ListItem.SingleOrDefault(t => t.TempleteGuid == sGuid);


                    tbContent.Text = eItem.Content;
                    tbRuleExpress.Text = eItem.RuleExpress;
                    tbTitle.Text = eItem.Title;
                    tbToEmail.Text = eItem.ToEmail;

                  

                    break;
                case 3:
                    pShow.Visible = false;
                    new SendEmail.SendEmail().DelItemToXml(sId, sGuid);

                    BindRP();

                    break;

            }


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


    protected void btnSave_Click(object sender, EventArgs e)
    {


        EmailDesignItem edi = new EmailDesignItem();

        edi.Content = tbContent.Text.Trim();
        edi.RuleExpress=tbRuleExpress.Text.Trim();
        edi.TempleteGuid = hfTempId.Value.Trim();
        edi.Title = tbTitle.Text.Trim();
        edi.ToEmail = tbToEmail.Text.Trim();
        edi.XmlId = sId;

        if(string.IsNullOrEmpty(edi.TempleteGuid))
        {
            new SendEmail.SendEmail().AddItemToXml(edi);
        }
        else
        {
            new SendEmail.SendEmail().UpdateItemToXml(edi);
        }

         pShow.Visible = false;


         BindRP();

    }
}
