using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Widget_GridShow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {



        SrnprWeb.WebEntity.GridShowWWE gsw = new SrnprWeb.WebEntity.GridShowWWE();
        gsw.ColumnList = new List<SrnprWeb.WebEntity.GridShowColumnWWE>();
        gsw.ParamList = new List<SrnprWeb.WebEntity.GridShowParamWWE>();
        gsw.TableInfo = new SrnprWeb.WebEntity.GridShowTableWWE();



        gsw.ColumnList.Add(new SrnprWeb.WebEntity.GridShowColumnWWE() { HeaderText = "表头1", ColumnData = "" });



       


        







        GSShow.GridShowEntity = gsw;

        

       

    }
}
