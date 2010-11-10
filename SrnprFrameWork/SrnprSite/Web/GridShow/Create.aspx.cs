using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SrnprWeb.WebEntity;

namespace SrnprSite.Web.GridShow
{
    public partial class Create : CPage
    {

        public SrnprWeb.WebEntity.GridShowWWE GSEntity
        {
            get
            {
                return (SrnprWeb.WebEntity.GridShowWWE)Session["Config_ConfigGridShow_" + hfGuid.Value];
            }
            set
            {
                Session["Config_ConfigGridShow_" + hfGuid.Value] = value;
            }
        }

        protected string sMessage = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(hfGuid.Value))
            {
                hfGuid.Value = Guid.NewGuid().ToString();
            }


            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["id"]))
                {
                    TBId.Enabled = false;
                    var x = SrnprWeb.WebProcess.GridShowWWP.GetEntityById(Request["id"].Trim());
                    TBTableName.Text = x.TableInfo.TableName;
                    TBDataBaseId.Text = x.TableInfo.DataBaseId;
                    TBId.Text = x.Id.Trim();
                    tbDescription.Text = x.Description.Trim();
                    tbGroupColumn.Text = x.TableInfo.GroupColumn;
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
                        Save();
                        break;
                    case "d_p":
                        GSEntity.ParamList.Remove(GSEntity.ParamList.SingleOrDefault(t => t.Guid == sId));
                        Save();
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


        


        protected void btnSave_Click(object sender, EventArgs e)
        {


            Save();
            
        }



        protected void Save()
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
                    tEntity.TableInfo.GroupColumn = tbGroupColumn.Text.Trim();


                    if (tE == null)
                    {

                        SrnprWeb.WebEntity.ItemBaseWWE gsli = new ItemBaseWWE();
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

                    CPageMessage("操作成功！");

                }
                else
                {
                    CPageMessage("编号已存在！");
                }



            }
            else
            {
                CPageMessage("编号不能空！");
            }

            BindDataColumn();
            BindParams();

        }


       

        protected void btnAddParams_Click(object sender, EventArgs e)
        {
            GridShowParamWWE gsp = new GridShowParamWWE();
            gsp.ColumnField = tbColumnField.Text.Trim();
            gsp.ParamName = tbParamName.Text.Trim();
            gsp.ParamOperator = ddlParamOperator.SelectedValue.Trim();
            gsp.ParamQueryType = ddlParamQueryType.SelectedValue.Trim();
            gsp.Guid = Guid.NewGuid().ToString();
            GSEntity.ParamList.Add(gsp);
            tbColumnField.Text = tbParamName.Text = "";
            Save();

        }



        public string GetTextByDDL(DropDownList ddl, object sValue)
        {


            if (sValue != null)
            {
                return ddl.Items.FindByValue(sValue.ToString().Trim()).Text.Trim();
            }
            else
            {
                return ddl.Items[0].Text.Trim();
            }
        }
    }
}
