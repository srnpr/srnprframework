﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SrnprSite.Demo.Dialog
{
    public partial class BasePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

           
            TD.Control_Dict.Add("MMM","aa");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            string sValue = TD.Control_DictGetValue("MMM");
           

        }
    }
}