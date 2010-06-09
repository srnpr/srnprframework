using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DcEmail_DcEmailTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        new SrnprCommon.ReplaceFile.ReplaceXmlCRF().GetTempleteXml(@"S:\AAAProject\SrnprFrameWork\SrnprFile\ReplaceFile\DcEmailCodeFRF.xml");


    }
}
