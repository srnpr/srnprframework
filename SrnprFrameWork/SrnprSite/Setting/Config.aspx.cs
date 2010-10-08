using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SrnprSite.System
{
    public partial class Config : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {

            SrnprCommon.CommonFunction.EntitySerializerCCF<SrnprWeb.WebConfig.ConfigEntityWWC>.EntityToXml(new SrnprWeb.WebConfig.TempConfigWWC().ConfigTest(), SrnprWeb.WebConfig.ConfigWWC.WebConfigPath());


        }
    }
}