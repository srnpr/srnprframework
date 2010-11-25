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


        Dictionary<string, string> dict = new Dictionary<string, string>();



        protected override void RenderContents(HtmlTextWriter output)
        {
            
            
            dict.Add("Text", Control_Text.Trim());
            dict.Add("Value", Control_Value.Trim());
            dict.Add("Description", Control_Description.Trim());


            output.Write(WebProcess.ToolDialogWWP.GetResponse(base.ClientID, Control_Url, base.ID, dict));
        }


        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Control_Url
        {
            get;
            set;
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
                    sControl_Text = Control_DictGetValue("Text");
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
                    sControl_Value = Control_DictGetValue("Value");
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
                    sControl_Description = Control_DictGetValue("Description");
                }
                return sControl_Description;
            }
            set
            {
                sControl_Description = value;
            }
        }

        public Dictionary<string, string> Control_Dict
        {
            get
            {
                return dict;
            }
        }



        public string Control_DictGetValue(string sKey)
        {
            string sReturn = "";


            if (Context.Request[base.ClientID + "_Control_" + sKey] != null)
            {
                sReturn = base.Context.Request[base.ClientID + "_Control_" + sKey].Trim();
            }


            return sReturn;

        }


    }
}
