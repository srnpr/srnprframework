<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="SrnprSite.Web.GridShow.Test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterPage_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPage_Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterPage_Content" runat="server">

<div>
    
    
    
    
    <sw:GridShowWWW ID="GSShow" runat="server" />
    
     <br /><br /><br /><br />
     
     <a href="List.aspx">返回列表</a>
     <br /><br />
     
     <a href="javascript:ShowList()">显示返回Json内容</a>
     <div id="show" style="display:none;">
     <table id="ttt"></table>
    <textarea id="jsonshow" rows="20" cols="100"></textarea>
</div>


    </div>
    
    
    <script type="text/javascript">
        function ShowList()
        {
            document.getElementById("show").style.display = "";
        }

        function SetColor(t)
        {
            GetLength(t.ClientId);
            //alert(GetLength(t.ClientId));
        }

        function GetLength(g)
        {
            //alert(SWW.J('#GS_table_' + g).children().eq(0).children().eq(2).children().eq(0).html());

           

            return document.getElementById('GS_table_' + g).rows.length-1;
        }



        function GetTitleIndex(g,t)
        {

            var aIndex = [];
            SWW.J('#GS_table_' + g).children().eq(0).children().eq(0).children().each(function (e) { if (SWW.J(this).text() == t) { aIndex.push(e); } });

            if (aIndex.length == 0)
            {
                aIndex = -1;
            }
            else if (aIndex.length == 1)
            {
                aIndex = aIndex[0];
            }

            return aIndex;


        }


        function GetObj(g,c, r)
        {
            
            return SWW.J('#GS_table_' + g).children().eq(0).children().eq(c + 1).children().eq(r);
        }

        

        function SetTdCss(o)
        {


           

            for (var i = 0, j = GetLength(o.g); i < j; i++)
            {
                if (o.f(o.g,i))
                {
                   
                    GetObj(o.g, i, GetTitleIndex(o.g,o.h)).css(o.c,o.v);
                   
                }
            }



        }


        



        function RowBind(c,f)
        {

            var aTitle = [];
            SWW.J('#GS_table_' + c).children().eq(0).children().eq(0).children().each(function (e) { aTitle.push(SWW.J(this).text()); });

            



            for (var i = 0, j =  document.getElementById('GS_table_' + c).rows.length-1; i < j; i++)
            {
                var row = {};
                row.RowIndex = i;

                row.CellCount = aTitle.length;

                row.Cell = [];

                row.Cells = {};

                SWW.J('#GS_table_' + c).children().eq(0).children().eq(i + 1).children().each(function (e) { row.Cell.push(SWW.J(this).text()); row.Cells[aTitle[e]] = SWW.J(this); });

                

                f(row);
            }

            //f(c);
        }

        function RowIndex(e)
        {

            if (e.CellTitle['消费平台'].text() == 'SO201010110004')
                {
                    e.CellTitle['消费城市'].css('background-color', 'red');
                }

                if (e.CellTitle['消费平台'].text() == 'SO201010110002')
                {
                    for (var i = 0, j = e.CellCount; i < j; i++)
                    {
                        e.Cell[i].css('background-color', 'red');
                    }
                }

            
        }


        //SWW.A('GS', 'Success', 'test.citysee', function (e) { RowBind(e.s.Request.ClientId,RowIndex); });

        SWW.GS.OnDataRowBind('test.citysee', RowIndex);



       
    </script>

</asp:Content>
