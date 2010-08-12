using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SrnprSite.Web.PageShow
{
    public partial class PageConfig : CPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    SrnprWeb.WebEntity.PageShowWWE psw = SrnprWeb.WebProcess.PageShowWWP.GetEntityById(Request.QueryString["id"].Trim());
                    tbXmlId.Text = psw.Id;
                    tbDescription.Text = psw.Description;

                    tbXmlId.Enabled = false;
                
                }
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(tbXmlId.Text.Trim()))
            {

                SrnprWeb.WebEntity.PageShowWWE psw = new SrnprWeb.WebEntity.PageShowWWE();

                if (Request.QueryString["temp"] != null)
                {
                    psw.Id = tbXmlId.Text.Trim();
                    psw.TempId = Request.QueryString["temp"].ToString().Trim();
                   
                    psw.Guid = Guid.NewGuid().ToString();
                }
                else
                {
                    psw = SrnprWeb.WebProcess.PageShowWWP.GetEntityById(tbXmlId.Text.Trim());


                }


                psw.Description = tbDescription.Text.Trim();

                var t = SrnprWeb.WebProcess.PageShowWWP.GetList();



                var oSingle = t.ItemList.SingleOrDefault(x => x.Id == psw.Id);




                if (tbXmlId.Enabled && oSingle != null)
                {
                    CPageMessage("对不起，编号已存在！");
                }
                else
                {

                    if (tbXmlId.Enabled)
                    {
                        oSingle = new SrnprWeb.WebEntity.ItemBaseWWE();
                    }

                    oSingle.Id = psw.Id;
                    oSingle.Guid = psw.Guid;
                    oSingle.Description = psw.Description;

                    if (tbXmlId.Enabled)
                    {
                        t.ItemList.Add(oSingle);
                    }

                    SrnprWeb.WebProcess.PageShowWWP.SaveFileByEntity(psw);
                    SrnprWeb.WebProcess.PageShowWWP.SaveList(t);


                    if (tbXmlId.Enabled)
                    {
                        Response.Redirect("CreateFromCk.aspx?id=" + tbXmlId.Text.Trim());
                    }
                    else
                    {
                        CPageMessage("修改配置成功！");
                    }

                }
            }
            else
            {
                CPageMessage("编号不能为空！");
            }
        }
    }
}
