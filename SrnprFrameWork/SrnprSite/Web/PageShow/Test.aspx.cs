using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SrnprSite.Web.PageShow
{
    public partial class Test : CPage
    {
        protected string sDemo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            PageShow1.XmlConfigName = Request["id"].ToString().Trim();

            if (Request["demo"] != null && Request["demo"].ToString() == "demo")
            {
                sDemo = "<script type=\"text/javascript\">SWJGSF.Demo();</script>";
            }
        }
    }
}
