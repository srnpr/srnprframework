using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SrnprSite.Test
{
    public partial class Widget_ListShow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            SrnprWeb.WebEntity.ListShowRequestWWE lsr = new SrnprWeb.WebEntity.ListShowRequestWWE();



            lsr.Guid = "";




            SrnprWeb.WebEntity.WidgetRequestWWE wr = new SrnprWeb.WebEntity.WidgetRequestWWE();
            wr.RQ = new List<SrnprWeb.WebInterface.WidgetRequestWWI>();
            wr.RQ.Add(lsr);



            string sReq = SrnprWeb.CommonFunction.JsonHelper.Serialize<SrnprWeb.WebEntity.WidgetRequestWWE>(wr);

            string s = SrnprWeb.WebProcess.WidgetProcessWWP.Response(sReq);




        }
    }
}
