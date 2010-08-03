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
            output.Write(ShowHtml(GridShowEntity));
        }



        /// <summary>
        /// 
        /// Description: 显示最终生成的内容
        /// Author:Liudpc
        /// Create Date: 2010-8-3 9:10:03
        /// </summary>
        /// <param name="gsw"></param>
        /// <returns></returns>
        protected string ShowHtml(WebEntity.GridShowWWE gsw)
        {
            StringBuilder sb = new StringBuilder();

            //定义参数名称
            string sId = "SWJGSF_Obj_" + base.ClientID;


            //开始输出执行逻辑
            sb.Append("<div id=\"SWJGSF_Div_"+base.ClientID+"\"></div><script>var " + sId + "=" + SrnprWeb.WebProcess.GridShowWWP.WidgetRequestString(XmlConfigName,base.ClientID) + ";SWJGSF.Init(" + sId + ");</script>");
           

            return sb.ToString();


        }










    }
}
