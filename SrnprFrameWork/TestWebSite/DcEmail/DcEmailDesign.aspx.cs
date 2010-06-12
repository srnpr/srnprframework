using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SrnprCommon.ReplaceFile;
using System.Text;

public partial class DcEmail_DcEmailDesign : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        SendEmailCRF se = new SendEmailCRF();



        string sId = Request["Id"].ToString().Trim();





        ResultSendEmailDesignEntityCRF rse = se.GetDesign(sId);
        lbParmsShow.Text = rse.Parms;



      

       //string[] strSql=txe.Code.MainSql.

    }
}
