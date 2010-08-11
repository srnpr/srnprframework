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
    [ToolboxData("<{0}:GridShowWWW runat=server></{0}:GridShowWWW>")]
    public class GridShowWWW : WebControl
    {
       
        


        private string _xmlConfigName = "";
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("配置文件名称，与xml文件保持一致")]
        public string XmlConfigName
        {
            get
            {
                return _xmlConfigName;
            }
            set
            {
                _xmlConfigName = value;
            }
        }


        public WebEntity.GridShowWWE GridShowEntity
        {
            get;
            set;
        }


        protected override void RenderContents(HtmlTextWriter output)
        {

            string sClientId = base.ClientID;
            string sContent = "";

            if (HttpContext.Current.Request["SWJGSF_Hidden_" + sClientId] != null)
            {
                sContent = HttpContext.Current.Request["SWJGSF_Hidden_" + sClientId].ToString().Trim();
            }


            output.Write(WebProcess.GridShowWWP.GetShowHtml(XmlConfigName,base.ClientID,sContent));
        }



    }
}
