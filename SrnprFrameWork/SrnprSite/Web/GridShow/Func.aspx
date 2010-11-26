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
        
        if (e.RowIndex%4==0)
        {
            for (var i = 0, j = e.CellCount; i < j; i++)
            {
                e.Cell[i].css('background-color', '#ffff00');
            }
        }

       

    }


    //添加绑定函数
    SWW.GS.OnDataRowBind('test.citysee', ChangeColorAll);


    /*单元格变色****************************************************************************************/

    //单元格变色
    function ChangeColorOne(e)
    {
        if (e.CellTitle['消费平台'].text() == 'SO201010140001')
        {
            e.CellTitle['消费城市'].css('background-color', 'red');
            e.CellTitle['分级'].css('background-color', '#ffbb00');
            e.Cell[2].css('background-color', '#ffbbcc');
            e.Cell[5].css('background-color', '#11cccc');
            e.Cell[6].css('background-color', '#1111cc');
            e.Cell[7].css('background-color', '#bb1111');
           
        }

        //列变色
        e.Cell[1].css('background-color', '#bbbbbb');
    }
    //添加绑定函数
    SWW.GS.OnDataRowBind('test.citysee',  ChangeColorOne);

    



    //SWW.A('GS', 'Success', 'test.citysee', function (e) { RowBind(e.s.Request.ClientId,RowIndex); });

    /*单元格内容替换****************************************************************************************/
    //单元格内容替换
    function ChangeContent(e)
    {
        if (e.CellTitle['消费平台'].text() == 'SO201010110001')
        {
            e.CellTitle['消费城市'].html('测试变化单元格内容');
        }
    }
    //添加绑定函数
    SWW.GS.OnDataRowBind('test.citysee', ChangeContent);




   

    /*点击列扩展内容****************************************************************************************/

    

        //点击时执行的函数
        function FuncClick(e)
        {
            SWW.GS.ExtendSetHtml(e, '<table class="SWW_CSS_GS_TABLE_TD_EXTEND_Table"><tr><td>订单编号：</td><td><input type="text"/></td><td>订单商品 ：</td><td><input type="text"/></td></tr></table>此列编号：' + e.RowIndex);
            
            }



      
      //添加扩展列的操作函数
        SWW.GS.OnDataRowBind('test.citysee', function (e) { SWW.GS.ExtendFunction(e, FuncClick); });



        /*点击单元格内容列扩展内容****************************************************************************************/


        function TdClickFunc(e)
        {
            //绑定到单元格的内部内容上
            if (e.CellTitle['消费平台'].text() == 'SO201010080020')
            {
                var guid = SWW.F.SYS.GetGuid();
                e.CellTitle['消费城市'].html('<input id="' + guid + '" value="点击" type="button" />');
                SWW.GS.ExtendFunction(e, ButtonClickFunc, SWW.J('#' + guid));
            }

            //绑定到外部按钮上
            if (e.RowIndex == 2)
            {
               
                SWW.GS.ExtendFunction(e, AjaxClickFunc, SWW.J('#ajax_extend_button'));
            }



        }
        function ButtonClickFunc(e)
        {
            SWW.GS.ExtendSetHtml(e, '行内按钮点击时');

        }


        function AjaxClickFunc(e)
        {
           
            if (e.RowIndex == 2)
            {
                SWW.J.ajax(
                {
                    url: "/ASMX/TestRequest.ashx",
                    success: function (s) { AjaxSuccessFul(e, s); }
                });
            }
        }
        function AjaxSuccessFul(e, s)
        {
            SWW.GS.ExtendSetHtml(e, s);
        }


        //添加扩展列的操作函数
        SWW.GS.OnDataRowBind('test.citysee', TdClickFunc);




        
        


</script>





<input type="button" id="ajax_extend_button" value="测试Ajax扩展列" />


</asp:Content>
