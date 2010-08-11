using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SrnprSite.Web.GridShow
{
    public partial class List : CPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            rpList.DataSource = SrnprWeb.WebProcess.GridShowWWP.GetList().ItemList;

            rpList.DataBind();

        }
    }
}
