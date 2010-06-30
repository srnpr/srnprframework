using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DcEmail_DcEmailCode : System.Web.UI.Page
{

    SendEmail.SendEmailCommon se = new SendEmail.SendEmailCommon();
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {

            if (Request["id"] != null)
            {

                tbXmlId.Text = Request["id"].Trim();
                tbXmlId.Enabled = false;
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
            pMainsql.Visible = false;
            pListSql.Visible = false;
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



        rpMainItem.DataSource = tc.MainSql;
        rpMainItem.DataBind();

        rpListItem.DataSource = tc.ListSql;
        rpListItem.DataBind();




    }


    protected void lbChange_Click(object sender, EventArgs e)
    {
        string sCommandName = ((LinkButton)sender).CommandName;

        string sParmGuid = ((LinkButton)sender).CommandArgument;

        if (sCommandName == "upd_parm")
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
        else if (sCommandName == "del_parm")
        {
             SrnprCommon.ReplaceFile.ItemPramEntityCRF ipe = TempCode.Parm.SingleOrDefault(t => t.Guid == sParmGuid);
             if (ipe != null)
             {
                 TempCode.Parm.Remove(ipe);
             }
             BindList(TempCode);
        }
        else if (sCommandName == "upd_mainsql")
        {
            SrnprCommon.ReplaceFile.ItemMainSqlEntityCRF imse = TempCode.MainSql.SingleOrDefault(t => t.Guid == sParmGuid);
            if (imse != null)
            {

                hfMainsqlId.Value = sParmGuid;
                pMainsql.Visible = true;
                btnMainsql.Text = "确认修改";
                tbMainsql.Text = imse.SqlString;
            }


        }
        else if (sCommandName == "del_mainsql")
        {
            SrnprCommon.ReplaceFile.ItemMainSqlEntityCRF imse = TempCode.MainSql.SingleOrDefault(t => t.Guid == sParmGuid);
            if (imse != null)
            {
                TempCode.MainSql.Remove(imse);
            }

            BindList(TempCode);

        }

        else if (sCommandName == "upd_listsql")
        {
            SrnprCommon.ReplaceFile.ItemListSqlEntityCRF ilse = TempCode.ListSql.SingleOrDefault(t => t.Guid == sParmGuid);
            if (ilse != null)
            {

                hfListsqlId.Value = sParmGuid;
                pListSql.Visible = true;
                btnListsql.Text = "确认修改";
                tbListsql.Text = ilse.SqlString;
            }


        }
        else if (sCommandName == "del_listsql")
        {
            SrnprCommon.ReplaceFile.ItemListSqlEntityCRF ilse = TempCode.ListSql.SingleOrDefault(t => t.Guid == sParmGuid);
            if (ilse != null)
            {
                TempCode.ListSql.Remove(ilse);
            }

            BindList(TempCode);

        }

        


    }



    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(tbXmlId.Text.Trim()))
        {


            se.SaveTempleteCode(TempCode, tbXmlId.Text.Trim());
            se.RecheckAllEmailFile();

        }
    }
 
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        switch (((Button)sender).CommandName)
        {

            case "cancel_parm":
                pParmAdd.Visible = false;
                break;
            case "cancel_mainsql":
                pMainsql.Visible = false;
                break;
            case "cancel_listsql":

                pListSql.Visible = false;
                break;
        }

       
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
    protected void btnMainsql_Click(object sender, EventArgs e)
    {
        if(!string.IsNullOrEmpty(tbMainsql.Text.Trim()))
        {
            string sMainsqlGuid = hfMainsqlId.Value.Trim();
            if (!string.IsNullOrEmpty(sMainsqlGuid))
            {
                SrnprCommon.ReplaceFile.ItemMainSqlEntityCRF imse = TempCode.MainSql.SingleOrDefault(t => t.Guid == sMainsqlGuid);
                if (imse != null)
                {
                    imse.SqlString = tbMainsql.Text;
                }
            }
            else
            {
                SrnprCommon.ReplaceFile.ItemMainSqlEntityCRF imse = new SrnprCommon.ReplaceFile.ItemMainSqlEntityCRF();
                imse.SqlString = tbMainsql.Text;
                imse.Guid = Guid.NewGuid().ToString();
                TempCode.MainSql.Add(imse);
            }
        }


        BindList(TempCode);
    }
    protected void btnListsql_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(tbListsql.Text.Trim()))
        {
            string sListsqlGuid = hfListsqlId.Value.Trim();
            if (!string.IsNullOrEmpty(sListsqlGuid))
            {
                SrnprCommon.ReplaceFile.ItemListSqlEntityCRF ilse = TempCode.ListSql.SingleOrDefault(t => t.Guid == sListsqlGuid);
                if (ilse != null)
                {
                    ilse.SqlString = tbListsql.Text;
                }
            }
            else
            {
                SrnprCommon.ReplaceFile.ItemListSqlEntityCRF ilse = new SrnprCommon.ReplaceFile.ItemListSqlEntityCRF();
                ilse.SqlString = tbListsql.Text;
                ilse.Guid = Guid.NewGuid().ToString();
                TempCode.ListSql.Add(ilse);
            }
        }

        BindList(TempCode);
    }


    protected void lbAdd_Click(object sender, EventArgs e)
    {
        switch (((LinkButton)sender).CommandName)
        {

            case "add_parm":
                pParmAdd.Visible = true;
                btnParm.Text = "确认添加";
                hfParmId.Value = "";
                tbParmName.Text = "";
                tbParmDescriptioon.Text = "";
                break;
            case "add_mainsql":
                pMainsql.Visible = true;
                btnMainsql.Text = "确认添加";
                tbMainsql.Text = "";
                hfMainsqlId.Value = "";

                break;
            case "add_listsql":
                pListSql.Visible = true;
                btnListsql.Text="确认添加";
                tbListsql.Text = "";
                hfListsqlId.Value = "";
                break;
        }
    }


  
   
}
