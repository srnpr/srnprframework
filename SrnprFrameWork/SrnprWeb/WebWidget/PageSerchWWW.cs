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
    [ToolboxData("<{0}:PageSerchWW runat=server></{0}:PageSerchWW>")]
    public class PageSerchWWW : WebControl
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
            output.Write(Text);
        }


        private WebEntity.PageSerchConfigWWE pscw = new SrnprWeb.WebEntity.PageSerchConfigWWE();





        #region WebWeightBaseIF 成员

        public SrnprCommon.BaseEntity.WebWidgetConfigCBE WebWeightConfig
        {
            get
            {
                //throw new NotImplementedException();
                return pscw;
            }
            set
            {
                //throw new NotImplementedException();
                pscw = (WebEntity.PageSerchConfigWWE)value;
            }
        }

        #endregion
    }
}
