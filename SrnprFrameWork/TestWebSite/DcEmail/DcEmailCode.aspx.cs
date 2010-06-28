using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DcEmail_DcEmailCode : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {

            if (Request["id"] != null)
            {
                SendEmail.SendEmail se = new SendEmail.SendEmail();

                TempCode = se.GetTempleteCode(Request["id"]);
            }
            else
            {
                SrnprCommon.ReplaceFile.TempleteCodeEntityCRF tc = new SrnprCommon.ReplaceFile.TempleteCodeEntityCRF();
                tc.Config = new SrnprCommon.ReplaceFile.ItemConfigEntityCRF();
                tc.Parm = new List<SrnprCommon.ReplaceFile.ItemPramEntityCRF>();
                tc.MainSql=new List<SrnprCommon.ReplaceFile.ItemMainSqlEntityCRF>();
                tc.ListSql=new List<SrnprCommon.ReplaceFile.ItemListSqlEntityCRF>();
                TempCode=tc;
            }

            Bind(TempCode);

        }


    }

    private string SessionId = "Session_SendEmail_DcEmailCode_CodeTemp";

    protected SrnprCommon.ReplaceFile.TempleteCodeEntityCRF TempCode
    {

        get
        {
            return (SrnprCommon.ReplaceFile.TempleteCodeEntityCRF)Session[SessionId];
        }
        set
        {
            Session[SessionId] = value;
        }
    }


    protected void Bind(SrnprCommon.ReplaceFile.TempleteCodeEntityCRF tc)
    {

        tbTitle.Text = tc.Config.Title;
        tbDescription.Text = tc.Config.Description;




    }
}
