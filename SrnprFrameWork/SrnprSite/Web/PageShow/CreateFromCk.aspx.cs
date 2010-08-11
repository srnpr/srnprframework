using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace SrnprSite.Web.PageShow
{
    public partial class CreateFromCk : System.Web.UI.Page
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




            SrnprWeb.WebEntity.PageShowWWE psw = new SrnprWeb.WebEntity.PageShowWWE();

            psw.Id = sId;

            psw.Guid = Guid.NewGuid().ToString();
            psw.HtmlContent = sText;


            psw.Content = RecheckContent(psw.HtmlContent);


            SrnprWeb.WebProcess.PageShowWWP.SaveFileByEntity(psw);




        }


        protected static string RecheckContent(string sCont)
        {



            //开始判断检测控件
            if (sCont.IndexOf("srnpr_srnpr_ck_control_type_id") > -1)
            {

                Regex re = new Regex("<img.*?srnpr_srnpr_ck_control_type_id.*?/>");

                MatchCollection mc = re.Matches(sCont);

                for (int i = 0, j = mc.Count; i < j; i++)
                {
                    Dictionary<string, string> dKvp = GetElementProp(mc[i].Value.ToString());

                    if (dKvp["srnpr_srnpr_ck_control_type_id"] == "gridshow")
                    {

                        if (dKvp.ContainsKey("src") && dKvp.ContainsKey("id"))
                        {


                            sCont = sCont.Replace(mc[i].Value, SrnprWeb.WebProcess.GridShowWWP.GetShowHtml(dKvp["srnpr_ck_gridshow_xmlid"], dKvp["id"]));



                        }
                    }


                }
            }
            return sCont;
        }


        protected static Dictionary<string, string> GetElementProp(string sElm)
        {

            Dictionary<string, string> dkvp = new Dictionary<string, string>();

            if (sElm.IndexOf(' ') > -1)
            {
                dkvp.Add("srnpr_html_elment_name", sElm.Substring(1, sElm.IndexOf(' ')));
            }




            Regex re = new Regex(" .*?\\=\".*?\"");


            MatchCollection mc = re.Matches(sElm);

            for (int i = 0, j = mc.Count; i < j; i++)
            {
                dkvp[mc[i].Value.Substring(0, mc[i].Value.IndexOf('=')).ToString().ToLower().Trim()] = mc[i].Value.Substring(mc[i].Value.IndexOf('=') + 1).ToString().Trim().Trim('"');
            }




            return dkvp;


        }
    }
}
