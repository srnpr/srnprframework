﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="SrnprSite.Web.GridShow.Test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterPage_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPage_Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterPage_Content" runat="server">

<div>
    
    <input type="text" name="BFI_FoodName" paramid="BFI_FoodName" /><input type="button" onclick="SWW.GS.Query();" value="查询" />
    
    <div id="testshow" >
    <sw:GridShowWWW ID="GSShow" runat="server" />
    </div>
     <br /><br /><br /><br />
     
     <a href="List.aspx">返回列表</a>
     <br /><br />
     
     <a href="javascript:ShowList()">显示返回Json内容</a>
     <a href="javascript:TestGSShow()">测试展开</a>
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



        function TestGSShow() {
            document.getElementById("testshow").style.display = '';
        }



       
    </script>

</asp:Content>
