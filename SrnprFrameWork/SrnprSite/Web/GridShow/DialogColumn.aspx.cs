using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SrnprWeb.WebEntity;

namespace SrnprSite.Web.GridShow
{
    public partial class DialogColumn : System.Web.UI.Page
    {

        public SrnprWeb.WebEntity.GridShowWWE GSEntity
        {
            get
            {
                return (SrnprWeb.WebEntity.GridShowWWE)Session["Config_ConfigGridShow_" + sTableGuid];
            }
            set
            {
                Session["Config_ConfigGridShow_" + sTableGuid] = value;
            }
        }

        private string sTableGuid="";

        private string sColumnGuid = "";

        protected string strFlag = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            sTableGuid = Request["t"].ToString().Trim();

            if (!string.IsNullOrEmpty(Request["c"]))
            {
                sColumnGuid = Request["c"].ToString().Trim();
            }

            if (!IsPostBack)
            {


                BindDDL(ddlColumnType, SrnprWeb.WebEntity.GridShowColumnDictWWE.ColumnType);
                BindDDL(ddlOrderType, SrnprWeb.WebEntity.GridShowColumnDictWWE.OrderType);
                BindDDL(ddlShowDisplay, SrnprWeb.WebEntity.GridShowColumnDictWWE.ShowDisplay);


                if (!string.IsNullOrEmpty(sColumnGuid))
                {
                    var column = GSEntity.ColumnList.Single(t => t.Guid == sColumnGuid);
                    TBHeaderText.Text = column.HeaderText.Trim();
                    TBColumnData.Text = column.ColumnData.Trim();
                    ddlColumnType.SelectedIndex = ddlColumnType.Items.IndexOf(ddlColumnType.Items.FindByValue(column.ColumnType));
                    tbColumnShow.Text = column.ColumnShow.Trim();
                    ddlShowDisplay.SelectedIndex = ddlShowDisplay.Items.IndexOf(ddlShowDisplay.Items.FindByValue(column.ShowDisplay));
                    ddlOrderType.SelectedIndex = ddlOrderType.Items.IndexOf(ddlOrderType.Items.FindByValue(column.OrderType));
                    tbWidth.Text = column.Width;
                }
            }


        }

        protected void BindDDL(DropDownList ddl,Dictionary<string,string> dict)
        {
            ddl.DataSource = dict;
            ddl.DataTextField = "Value";
            ddl.DataValueField = "Key";
            ddl.DataBind();
        }


        protected void bSave_Click(object sender, EventArgs e)
        {
            GridShowColumnWWE gsc = new GridShowColumnWWE();
            gsc.HeaderText = TBHeaderText.Text.Trim();
            gsc.ColumnData = TBColumnData.Text.Trim();
            gsc.ColumnType = ddlColumnType.SelectedValue.Trim();
            gsc.ColumnShow = tbColumnShow.Text.Trim();

            gsc.ShowDisplay = ddlShowDisplay.SelectedValue.Trim();
            gsc.OrderType = ddlOrderType.SelectedValue.Trim();
            gsc.Width = tbWidth.Text.Trim();


            if (string.IsNullOrEmpty(sColumnGuid))
            {

                gsc.Guid = Guid.NewGuid().ToString();

                GSEntity.ColumnList.Add(gsc);
            }
            else
            {
                gsc.Guid = sColumnGuid;


                GSEntity.ColumnList[GSEntity.ColumnList.IndexOf(GSEntity.ColumnList.Single(t => t.Guid == sColumnGuid))]= gsc;

            }

            strFlag = "ok";
            
        }
    }
}