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
                BindDDL(ddlExcelType, SrnprWeb.WebEntity.GridShowColumnDictWWE.ExcelType);
                BindStyle(ddlStyle_TextAlign, "text-align");
                
                if (!string.IsNullOrEmpty(sColumnGuid))
                {
                    var column = GSEntity.ColumnList.Single(t => t.Guid == sColumnGuid);
                    TBHeaderText.Text = column.HeaderText.Trim();
                    TBColumnData.Text = column.ColumnData.Trim();
                    ddlColumnType.Items.FindByValue(column.ColumnType).Selected=true;
                    tbColumnShow.Text = column.ColumnShow.Trim();
                    ddlShowDisplay.Items.FindByValue(column.ExcelType).Selected = true;
                    ddlOrderType.Items.FindByValue(column.OrderType).Selected = true;
                    ddlExcelType.Items.FindByValue(column.OrderType).Selected = true;
                    Dictionary<string, string> dictStyle = SrnprWeb.CommonFunction.StyleHelperWCF.GetDictByStyleString(column.Style);
                    if (dictStyle.ContainsKey("text-align")) ddlStyle_TextAlign.Items.FindByValue(dictStyle["text-align"].Trim()).Selected = true;

                    if (dictStyle.ContainsKey("width")) tbStyle_Width.Text = dictStyle["width"].Trim();
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

        protected void BindStyle(DropDownList ddl, string sStyleName)
        {
            var vStyle = SrnprWeb.CommonFunction.StyleHelperWCF.GetDescriptionEntity(sStyleName);
            if (vStyle != null)
            {
                ddl.DataSource = vStyle.Item;
                ddl.DataTextField = "Value";
                ddl.DataValueField = "Key";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("默认", ""));
            }

        }

       


        protected void bSave_Click(object sender, EventArgs e)
        {
            GridShowColumnWWE gsc = new GridShowColumnWWE();
            gsc.HeaderText = TBHeaderText.Text.Trim();
            gsc.ColumnData = TBColumnData.Text.Trim();
            gsc.ColumnType = ddlColumnType.SelectedValue.Trim();
            gsc.ExcelType = ddlExcelType.SelectedValue.Trim();
            gsc.ColumnShow = tbColumnShow.Text.Trim();

            gsc.ShowDisplay = ddlShowDisplay.SelectedValue.Trim();
            gsc.OrderType = ddlOrderType.SelectedValue.Trim();


            List<string> listStyle = new List<string>();
            if (!string.IsNullOrEmpty(tbStyle_Width.Text.Trim()))  listStyle.Add("width:"+tbStyle_Width.Text.Trim());
            if (!string.IsNullOrEmpty(ddlStyle_TextAlign.SelectedValue)) listStyle.Add("text-align:" + ddlStyle_TextAlign.SelectedValue.Trim());


            gsc.Style = string.Join(";",listStyle.ToArray());

            

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