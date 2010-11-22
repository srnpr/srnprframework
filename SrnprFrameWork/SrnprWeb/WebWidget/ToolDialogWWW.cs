using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SrnprWeb.WebWidget
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ToolDialogWWW runat=server></{0}:ToolDialogWWW>")]
    public class ToolDialogWWW : WebControl
    {
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Text
        {
            get
            {
                String s = (String)ViewState["Text"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["Text"] = value;
            }
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            StringBuilder sb = new StringBuilder();
            


            output.Write(Text);
        }






        private string sControl_Text;
        private string sControl_Value;
        private string sControl_Description;

        public string Control_Text
        {
            get
            {
                if (string.IsNullOrEmpty(sControl_Text))
                {
                    sControl_Text = base.Page.Request[base.ClientID + "_Control_Text"];
                }

                return sControl_Text;
            }
            set
            {
                sControl_Text = value;
            }
        }


        public string Control_Value
        {
            get
            {
                if (string.IsNullOrEmpty(sControl_Value))
                {
                    sControl_Value = base.Page.Request[base.ClientID + "_Control_Value"];
                }
                return sControl_Value;
            }
            set
            {
                sControl_Value = value;
            }

        }

        public string Control_Description
        {
            get
            {
                if (string.IsNullOrEmpty(sControl_Description))
                {
                    sControl_Description = base.Page.Request[base.ClientID + "_Control_Description"];
                }
                return sControl_Description;
            }
            set
            {
                sControl_Description = value;
            }
        }




    }
}
