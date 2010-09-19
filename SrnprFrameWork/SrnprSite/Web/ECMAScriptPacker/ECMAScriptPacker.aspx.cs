using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using SrnprWeb.CommonFunction;

namespace SrnprSite.Web.ECMAScriptPacker
{
    public partial class ECMAScriptPacker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnChangeJS_Click(object sender, EventArgs e)
        {
            lbMessage.Text = Change(tbBaseDir.Text.Trim(), tbSourceJS.Text.Trim(), tbSaveJSSource.Text.Trim(), tbSaveJSMin.Text.Trim(), ECMAScriptPackerWCF.PackerEncoding.Normal);
        }

        protected void btnChangeCSS_Click(object sender, EventArgs e)
        {
            lbMessage.Text = Change(tbBaseDir.Text.Trim(), tbSourceCss.Text.Trim(), tbSaveCSSSource.Text.Trim(), tbSaveCSSMin.Text.Trim(), ECMAScriptPackerWCF.PackerEncoding.None);
        }



        protected string Change(string sBase, string sSource, string sSaveSource, string sSave, SrnprWeb.CommonFunction.ECMAScriptPackerWCF.PackerEncoding pe)
        {

            StringBuilder sb = new StringBuilder();

            foreach (string s in sSource.Split(','))
            {
                sb.Append(File.ReadAllText(sBase + s, System.Text.Encoding.Unicode));

            }


            File.WriteAllText(sBase + sSaveSource, sb.ToString(), System.Text.Encoding.Unicode);
            ECMAScriptPackerWCF p = new ECMAScriptPackerWCF(pe, true, false);
            string sReturn = p.Pack(sb.ToString()).Replace("\n", "\r\n");
            File.WriteAllText(sBase + sSave, sReturn, System.Text.Encoding.UTF8);


            return "成功";

        }


    }
}