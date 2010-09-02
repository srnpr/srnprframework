using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace SrnprSite.Web.PageShow
{
    public partial class CreateFromCk : CPage
    {
        protected string sId = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            sId=Request["id"].ToString().Trim();

            if (!IsPostBack)
            {

                var t=SrnprWeb.WebProcess.PageShowWWP.GetEntityById(sId);

                tbEditor.Text = t.HtmlContent;

                if (string.IsNullOrEmpty(tbEditor.Text.Trim()))
                {
                    tbEditor.Text = SrnprWeb.WebProcess.PageShowWWP.GetTempletesById(t.TempId);
                }

            }



        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            string sText = tbEditor.Text.Trim();
            SrnprWeb.WebEntity.PageShowWWE psw = SrnprWeb.WebProcess.PageShowWWP.GetEntityById(sId);
            psw.HtmlContent = sText;
            psw.Content = SrnprWeb.WebProcess.WidgetProcessWWP. RecheckContent(psw.HtmlContent);
            SrnprWeb.WebProcess.PageShowWWP.SaveFileByEntity(psw);
            CPageMessage("保存成功！");

        }


        
    }
}
