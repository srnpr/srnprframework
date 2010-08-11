﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SrnprSite.Web.PageShow
{
    public partial class List :CPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            rpList.DataSource = SrnprWeb.WebProcess.PageShowWWP.GetList().ItemList;

            rpList.DataBind();
        }
    }
}
