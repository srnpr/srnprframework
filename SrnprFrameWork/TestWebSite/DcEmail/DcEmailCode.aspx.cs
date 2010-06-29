using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DcEmail_DcEmailCode : System.Web.UI.Page
{

    SendEmail.SendEmail se = new SendEmail.SendEmail();
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {

            if (Request["id"] != null)
            {
               

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





            BindFirst(TempCode);
            BindList(TempCode);


            pParmAdd.Visible = false;

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


    protected void BindFirst(SrnprCommon.ReplaceFile.TempleteCodeEntityCRF tc)
    {

        tbTitle.Text = tc.Config.Title;
        tbDescription.Text = tc.Config.Description;





        ddlDataBase.DataSource = se.GetServerDatabase();
        ddlDataBase.DataTextField = ddlDataBase.DataValueField = "Id";
        ddlDataBase.DataBind();

        if (!string.IsNullOrEmpty(tc.Config.DataServerId))
        {
            ddlDataBase.SelectedIndex = ddlDataBase.Items.IndexOf(ddlDataBase.Items.FindByValue(tc.Config.DataServerId));
        }





        ddlServerEmail.DataSource = se.GetServerEmail();
        ddlServerEmail.DataTextField = ddlServerEmail.DataValueField = "Id";
        ddlServerEmail.DataBind();

        if (!string.IsNullOrEmpty(tc.Config.EmailServerId))
        {
            ddlServerEmail.SelectedIndex = ddlServerEmail.Items.IndexOf(ddlServerEmail.Items.FindByValue(tc.Config.EmailServerId));
        }
    }




    protected void BindList(SrnprCommon.ReplaceFile.TempleteCodeEntityCRF tc)
    {

        

        rpParmItem.DataSource = tc.Parm;
        rpParmItem.DataBind();


       





    }


    protected void lbParmChange_Click(object sender, EventArgs e)
    {
        string sCommandName = ((LinkButton)sender).CommandName;

        string sParmGuid = ((LinkButton)sender).CommandArgument;

        if (sCommandName == "upd")
        {
            SrnprCommon.ReplaceFile.ItemPramEntityCRF ipe = TempCode.Parm.SingleOrDefault(t => t.Guid == sParmGuid);
            if (ipe != null)
            {
                hfParmId.Value = sParmGuid;
                pParmAdd.Visible = true;
                btnParm.Text = "确认修改";
                tbParmName.Text = ipe.ParmName;
                tbParmDescriptioon.Text = ipe.ParmText;




            }
        }
        else if (sCommandName == "del")
        {
             SrnprCommon.ReplaceFile.ItemPramEntityCRF ipe = TempCode.Parm.SingleOrDefault(t => t.Guid == sParmGuid);
             if (ipe != null)
             {
                 TempCode.Parm.Remove(ipe);
             }
             BindList(TempCode);
        }

        


    }



    protected void btnSave_Click(object sender, EventArgs e)
    {
        
    }
    protected void lbParmAdd_Click(object sender, EventArgs e)
    {
        pParmAdd.Visible = true;
        btnParm.Text = "确认添加";
        hfParmId.Value = "";
        tbParmName.Text = "";
        tbParmDescriptioon.Text = "";



    }
    protected void btnParmCancel_Click(object sender, EventArgs e)
    {
        pParmAdd.Visible = false;
    }
    protected void btnParm_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(tbParmName.Text) && !string.IsNullOrEmpty(tbParmDescriptioon.Text) && TempCode.Parm.Count(t => t.ParmName == tbParmName.Text.Trim()) == 0)
        {
            string sParmGuid = hfParmId.Value.Trim();

            if (!string.IsNullOrEmpty(sParmGuid))
            {
                SrnprCommon.ReplaceFile.ItemPramEntityCRF ipe = TempCode.Parm.SingleOrDefault(t => t.Guid == sParmGuid);
                if (ipe != null)
                {
                    ipe.ParmName = tbParmName.Text;
                    ipe.ParmText = tbParmDescriptioon.Text;
                }
            }
            else
            {
                SrnprCommon.ReplaceFile.ItemPramEntityCRF ipe = new SrnprCommon.ReplaceFile.ItemPramEntityCRF();
                ipe.ParmName = tbParmName.Text.Trim();
                ipe.ParmText = tbParmDescriptioon.Text.Trim();
                ipe.Guid = Guid.NewGuid().ToString();
                TempCode.Parm.Add(ipe);
            }
        }

        BindList(TempCode);
    }
}
