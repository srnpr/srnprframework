﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widget_GridShowById : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["id"] != null)
        {

            GSShow.XmlConfigName = Request["id"].Trim();
        }
    }
}
