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
    [ToolboxData("<{0}:PageShowWWW runat=server></{0}:PageShowWWW>")]
    public class PageShowWWW : WebControl
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




        private Dictionary<string, string> _requestContent = new Dictionary<string, string>();
        public Dictionary<string, string> RequestContent
        {
            get;
            set;
        }

        protected override void OnInit(EventArgs e)
        {

            if (HttpContext.Current.Request != null)
            {
               for(int i=0,j=HttpContext.Current.Request.Form.Count;i<j;i++)

                {
                    _requestContent[HttpContext.Current.Request.Form.Keys[i]]=HttpContext.Current.Request.Form[i];
                }

            }


            base.OnInit(e);

        }



        protected override void RenderContents(HtmlTextWriter output)
        {
            //output.Write(XmlConfigName);

            if (!string.IsNullOrEmpty(XmlConfigName))
            {

                WebEntity.PageShowWWE psw = WebProcess.PageShowWWP.GetEntityById(XmlConfigName);


                output.Write(WebProcess.PageShowWWP.GetShowHtml(psw,base.ClientID));


              
            }


        }



       



    }
}
