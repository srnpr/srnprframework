using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SrnprWeb.WebEntity;

public partial class Config_ConfigGridShow : System.Web.UI.Page
{


    public SrnprWeb.WebEntity.GridShowWWE GSEntity
    {
        get
        {
            return (SrnprWeb.WebEntity.GridShowWWE)Session["Config_ConfigGridShow"];
        }
        set
        {
            Session["Config_ConfigGridShow"] = value;
        }
    }

    protected string sMessage = "";
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty( Request["id"]) )
            {
                TBId.Enabled = false;

                var x = SrnprWeb.WebProcess.GridShowWWP.GetEntityById(Request["id"].Trim());

                TBTableName.Text = x.TableInfo.TableName;
                TBDataBaseId.Text = x.TableInfo.DataBaseId;
                TBId.Text = x.Id.Trim();
                tbDescription.Text = x.Description.Trim();

                GSEntity = x;
                BindDataColumn();
                BindParams();


            }
            else
            {
                SrnprWeb.WebEntity.GridShowWWE gsw = new SrnprWeb.WebEntity.GridShowWWE();
                gsw.TableInfo = new SrnprWeb.WebEntity.GridShowDataWWE();
                gsw.ParamList = new List<SrnprWeb.WebEntity.GridShowParamWWE>();
                gsw.ColumnList = new List<SrnprWeb.WebEntity.GridShowColumnWWE>();
                gsw.Guid = Guid.NewGuid().ToString();


                GSEntity = gsw;
            }
        }


        if (!string.IsNullOrEmpty(Request["submittype"]))
        {
            string sId = Request["submitid"].Trim();
            switch (Request["submittype"].Trim())
            {
                case "d_d":
                    GSEntity.ColumnList.Remove(GSEntity.ColumnList.SingleOrDefault(t => t.Guid == sId));
                    BindDataColumn();
                    break;
                case "d_p":
                    GSEntity.ParamList.Remove(GSEntity.ParamList.SingleOrDefault(t => t.Guid == sId));
                    BindParams();
                    break;



            }


        }




    }



    protected void BindDataColumn()
    {

        RPDataColumn.DataSource = GSEntity.ColumnList;
        RPDataColumn.DataBind();

    }

    protected void BindParams()
    {
        RPParams.DataSource = GSEntity.ParamList;
        RPParams.DataBind();
    }


    protected void btnAddDataColumn_Click(object sender, EventArgs e)
    {
        GridShowColumnWWE gsc = new GridShowColumnWWE();
        gsc.HeaderText = TBHeaderText.Text.Trim();
        gsc.ColumnData = TBColumnData.Text.Trim();
        gsc.ColumnType = ddlColumnType.SelectedValue.Trim();
        gsc.ColumnShow = tbColumnShow.Text.Trim();

        gsc.ShowDisplay = ddlShowDisplay.SelectedValue.Trim();
        gsc.OrderType = ddlOrderType.SelectedValue.Trim();
        gsc.Width = tbWidth.Text.Trim();

        gsc.Guid = Guid.NewGuid().ToString();

        GSEntity.ColumnList.Add(gsc);

        TBHeaderText.Text = "";
        TBColumnData.Text = "";
      
        BindDataColumn();
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {


        if (TBId != null)
        {
            var tList = SrnprWeb.WebProcess.GridShowWWP.GetList();

            var tE = tList.ItemList.SingleOrDefault(t => t.Id == TBId.Text.Trim());

            if (!TBId.Enabled || tE == null)
            {
                var tEntity = GSEntity;

                tEntity.Id = TBId.Text.Trim();
                tEntity.TableInfo.TableName = TBTableName.Text.Trim();
                tEntity.TableInfo.DataBaseId = TBDataBaseId.Text.Trim();
                tEntity.TableInfo.KeyColumn = tEntity.ColumnList.FirstOrDefault(x => x.ColumnData != "").ColumnData;
                tEntity.TableInfo.WhereString = "";
                tEntity.Description = tbDescription.Text.Trim();



                if (tE == null)
                {

                    SrnprWeb.WebEntity.GridShowListItemWWE gsli = new GridShowListItemWWE();
                    gsli.Id = tEntity.Id;

                    gsli.Guid = tEntity.Guid;
                    gsli.Description = tEntity.Description;

                    tList.ItemList.Add(gsli);
                }
                else
                {
                    tE.Description = tEntity.Description;

                }


                SrnprWeb.WebProcess.GridShowWWP.SaveFileByEntity(tEntity);

                SrnprWeb.WebProcess.GridShowWWP.SaveList(tList);

                BindMessage("创建成功！");

            }
            else
            {
                BindMessage("编号已存在！");
            }



        }
        else
        {
            BindMessage("编号不能空！");
        }


    }

    protected void BindMessage(string sMsg)
    {
        sMessage = sMsg;
    }


    protected void btnAddParams_Click(object sender, EventArgs e)
    {
        GridShowParamWWE gsp = new GridShowParamWWE();
        gsp.ColumnField = tbColumnField.Text.Trim();
        gsp.ParamName = tbParamName.Text.Trim();
        gsp.ParamOperator = ddlParamOperator.SelectedValue.Trim();


        gsp.Guid = Guid.NewGuid().ToString();

        GSEntity.ParamList.Add(gsp);


        tbColumnField.Text = tbParamName.Text = "";

        BindParams();

    }



    public string GetTextByDDL(DropDownList ddl,object sValue)
    {


        if (sValue!=null)
        {
            return ddl.Items.FindByValue(sValue.ToString().Trim()).Text.Trim();
        }
        else
        {
            return ddl.Items[0].Text.Trim();
        }
    }

}
