using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SrnprCommon.ReplaceFile;

public partial class DcEmail_DcEmailDesign : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        string sId = Request["Id"].ToString().Trim();


        ReplaceXmlCRF rx = new ReplaceXmlCRF();

       TempleteXmlEntityCRF txe=rx.GetTempleteXml(   new SrnprCommon.ReplaceFile.SendEmailCRF().GetListFileInfoByFilePath().SingleOrDefault(t => t.Id == sId).FilePath);

       



    }
}
