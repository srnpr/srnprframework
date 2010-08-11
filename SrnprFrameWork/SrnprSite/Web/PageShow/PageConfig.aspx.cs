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

        }

        protected void btnNext_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(tbXmlId.Text.Trim()))
            {

                SrnprWeb.WebEntity.PageShowWWE psw = new SrnprWeb.WebEntity.PageShowWWE();

                if (Request["temp"] != null)
                {
                    psw.Id = tbXmlId.Text.Trim();
                    psw.TempId = Request["temp"].ToString().Trim();
                    psw.Description = tbDescription.Text.Trim();
                    psw.Guid = Guid.NewGuid().ToString();
                }


                SrnprWeb.WebProcess.PageShowWWP.SaveFileByEntity(psw);


                var t = SrnprWeb.WebProcess.PageShowWWP.GetList();
                t.ItemList.Add(new SrnprWeb.WebEntity.ItemBaseWWE() { Id = psw.Id, Description = psw.Description, Guid = psw.Guid });
                SrnprWeb.WebProcess.PageShowWWP.SaveList(t);



                Response.Redirect("CreateFromCk.aspx?id=" + tbXmlId.Text.Trim());
            }
            else
            {
                CPageMessage("编号不能为空！");
            }
        }
    }
}
