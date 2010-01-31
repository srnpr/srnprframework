using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Xml_根据Xsd验证xml文件 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void BOk_Click(object sender, EventArgs e)
    {
        /*
        try
        {
            using (XmlTextReader txtreader = new XmlTextReader(TBXml.Text.Trim()))
            {

                using (XmlValidatingReader oreader = new XmlValidatingReader(txtreader))
                {
                    oreader.Schemas.Add(null, TBXsd.Text.Trim());
                    while (oreader.Read())
                    {
                    }
                }
            }
        }
        catch(Exception ex)
        {
            LMessage.Text = ex.Message.Trim();
        }
        */
    }
}
