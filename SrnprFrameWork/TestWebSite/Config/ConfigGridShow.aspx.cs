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


    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            if (Request["id"] != null)
            {


            }
            else
            {
                SrnprWeb.WebEntity.GridShowWWE gsw = new SrnprWeb.WebEntity.GridShowWWE();
                gsw.TableInfo = new SrnprWeb.WebEntity.GridShowDataWWE();
                gsw.ParamList = new List<SrnprWeb.WebEntity.GridShowParamWWE>();
                gsw.ColumnList = new List<SrnprWeb.WebEntity.GridShowColumnWWE>();

                GSEntity = gsw;
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



        gsc.Guid = Guid.NewGuid().ToString();

        GSEntity.ColumnList.Add(gsc);

        TBHeaderText.Text = "";
        TBColumnData.Text = "";
      
        BindDataColumn();
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {

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
}
