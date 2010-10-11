<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.Master" AutoEventWireup="true" CodeBehind="Func.aspx.cs" Inherits="SrnprSite.Web.GridShow.Func" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterPage_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPage_Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterPage_Content" runat="server">

<sw:GridShowWWW ID="GSShow" runat="server" XmlConfigName="test.citysee" />



<script type="text/javascript">


    /*行变色****************************************************************************************/
    //行变色
    function ChangeColorAll(e)
    {
        if (e.CellTitle['消费平台'].text() == 'SO201010110002')
        {
            for (var i = 0, j = e.CellCount; i < j; i++)
            {
                e.Cell[i].css('background-color', 'red');
            }
        }
    }
    //添加绑定函数
    SWW.GS.OnDataRowBind('test.citysee', ChangeColorAll);


    /*单元格变色****************************************************************************************/

    //单元格变色
    function ChangeColorOne(e)
    {
        if (e.CellTitle['消费平台'].text() == 'SO201010110004')
        {
            e.CellTitle['消费城市'].css('background-color', 'red');
        }
    }
    //添加绑定函数
    SWW.GS.OnDataRowBind('test.citysee',  ChangeColorOne);

    



    //SWW.A('GS', 'Success', 'test.citysee', function (e) { RowBind(e.s.Request.ClientId,RowIndex); });



    /*点击列扩展内容****************************************************************************************/
 
        //点击时执行的函数
        function FuncClick(e)
        {
            SWW.GS.ExtendSetHtml(e,'aaa'+e.RowIndex);
        }

      
      //添加扩展列的操作函数
    SWW.GS.OnDataRowBind('test.citysee', function (e) { SWW.GS.ExtendFunction(e, FuncClick); });

</script>




</asp:Content>
