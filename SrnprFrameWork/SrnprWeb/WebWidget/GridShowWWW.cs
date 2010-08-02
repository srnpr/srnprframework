using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

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


            
            output.Write(ShowHtml(GridShowEntity));
        }



        protected string ShowHtml(WebEntity.GridShowWWE gsw)
        {
            StringBuilder sb = new StringBuilder();


            string sId = "SWJGSF_Obj_" + base.ClientID;



            sb.Append("<div id=\"SWJGSF_Div_"+base.ClientID+"\"></div><script>var " + sId + "=" + SrnprWeb.WebProcess.GridShowWWP.WidgetRequestString(XmlConfigName,base.ClientID) + ";SWJGSF.Init(" + sId + ");</script>");
           

            return sb.ToString();


        }










    }
}
