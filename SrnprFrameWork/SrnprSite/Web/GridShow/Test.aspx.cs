using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SrnprSite.Web.GridShow
{
    public partial class Test : CPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["id"] != null)
            {

                GSShow.XmlConfigName = Request["id"].Trim();
            }
        }
    }
}
