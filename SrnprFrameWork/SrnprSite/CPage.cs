using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SrnprSite
{
    public class CPage:System.Web.UI.Page
    {


        public void CPageMessage(string sMessage)
        {

            this.Page.ClientScript.RegisterStartupScript(typeof(string), "CPageScript:CPage_Message", "<script type=\"text/javascript\" defer=\"defer\">MasterPage.Message(\"" + sMessage + "\");</script>", false);
        }


    }
}
