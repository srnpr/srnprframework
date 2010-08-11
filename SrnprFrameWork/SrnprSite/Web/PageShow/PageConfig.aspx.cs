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


              

               



                if (!tbXmlId.Enabled)
                {
                    SrnprWeb.WebProcess.PageShowWWP.SaveList(t);
                    CPageMessage("修改配置成功！");
                }
                else
                {


                    if (t.ItemList.Count(x => x.Id == tbXmlId.Text.Trim()) == 0)
                    {
                        t.ItemList.Add(new SrnprWeb.WebEntity.ItemBaseWWE() { Id = psw.Id, Description = psw.Description, Guid = psw.Guid });

                        SrnprWeb.WebProcess.PageShowWWP.SaveList(t);
                        SrnprWeb.WebProcess.PageShowWWP.SaveFileByEntity(psw);



                        Response.Redirect("CreateFromCk.aspx?id=" + tbXmlId.Text.Trim());
                    }
                    else
                    {
                        CPageMessage("对不起，编号已存在！");
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
