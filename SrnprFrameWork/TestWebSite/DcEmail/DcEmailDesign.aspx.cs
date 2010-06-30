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


                    EmailDesignInfo eInfo= new SendEmail.SendEmailCommon().GetDesign(sId);

                    EmailDesignItem eItem = eInfo.ListItem.SingleOrDefault(t => t.TempleteGuid == sGuid);


                    if (cblState.Visible)
                    {
                        string[] str = eItem.RuleExpress.Replace("||", "|").Replace("==", "=").Split('|');

                        foreach(string s in str)
                        {
                            string sValue = s.Split('=')[1].Replace("\"","");

                            if(cblState.Items.FindByValue(sValue)!=null)
                            cblState.Items.FindByValue(sValue).Selected = true;
                        }

                        


                    }
                    
                    

                    

                    tbContent.Text = eItem.Content;
                    tbRuleExpress.Text = eItem.RuleExpress;
                    tbTitle.Text = eItem.Title;
                    tbToEmail.Text = eItem.ToEmail;

                  

                    break;
                case 3:
                    pShow.Visible = false;
                    new SendEmail.SendEmailCommon().DelItemToXml(sId, sGuid);

                    BindRP();

                    break;

            }


        }



    }



    protected void BindRP()
    {
        SendEmail.SendEmailCommon se = new SendEmail.SendEmailCommon();

        SendEmail.EmailDesignInfo edi = se.GetDesign(sId);


        if (!string.IsNullOrEmpty(edi.StateTitle)&&edi.StateValue.Length>0)
        {
            if (cblState.Items.Count == 0)
            {
                tbRuleExpress.Visible = false;
                lbRule.Text = edi.StateTitle.Trim();

                foreach (string s in edi.StateValue)
                {
                    cblState.Items.Add(new ListItem(s.Trim(), s.Trim()));
                }
            }

        }
        else
        {

            cblState.Visible = false;
        }



        lbParmsShow.Text = edi.Parms;

        rpList.DataSource = edi.ListItem;
        rpList.DataBind();
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        SendEmail.SendEmailCommon se= new SendEmail.SendEmailCommon();

        EmailDesignItem edi = new EmailDesignItem();

        edi.Content = tbContent.Text.Trim();



        edi.RuleExpress=tbRuleExpress.Text.Trim();

        if (cblState.Visible)
        {
            string sStateName =se.GetMainReplace( lbRule.Text.Trim());

            List<string> lStr = new List<string>();

            foreach (ListItem li in cblState.Items)
            {
                if (li.Selected)
                {

                    lStr.Add("\""+sStateName+"\"==\"" + li.Value.Trim() + "\"");

                }
            }

            if (lStr.Count > 0)
            {

                edi.RuleExpress = string.Join("||", lStr.ToArray());
            }
            else
            {
                edi.RuleExpress = "\"" + sStateName + "\"==\"" + sStateName+"\"";
            }




        }



        edi.TempleteGuid = hfTempId.Value.Trim();
        edi.Title = tbTitle.Text.Trim();
        edi.ToEmail = tbToEmail.Text.Trim();
        edi.XmlId = sId;

        if(string.IsNullOrEmpty(edi.TempleteGuid))
        {
           se.AddItemToXml(edi);
        }
        else
        {
            se.UpdateItemToXml(edi);
        }

         pShow.Visible = false;


         BindRP();

    }
}
