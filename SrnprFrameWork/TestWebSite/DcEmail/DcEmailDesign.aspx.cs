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


        ReplaceXmlCRF rx = new ReplaceXmlCRF();

       TempleteXmlEntityCRF txe=rx.GetTempleteXml( new SrnprCommon.ReplaceFile.SendEmailCRF().GetListFileInfoByFilePath().SingleOrDefault(t => t.Id == sId).FilePath);



       StringBuilder sb = new StringBuilder();
       foreach (ItemMainSqlEntityCRF mainSql in txe.Code.MainSql)
       {
           sb.Append("<li>{$"+string.Join("}</li><li>{$",rx.RegexSqlStringParm(mainSql.SqlString))+"}</li>");
       }
       lbParmsShow.Text = sb.ToString().Trim();

       //string[] strSql=txe.Code.MainSql.

    }
}
