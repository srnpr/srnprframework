<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="SrnprSite.Web.GridShow.Test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterPage_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPage_Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterPage_Content" runat="server">

<div>
    
    
    
    
    <sw:GridShowWWW ID="GSShow" runat="server" />
    
     <br /><br /><br /><br />
     
     <a href="../Config/ConfigGridShowList.aspx">返回列表</a>
     <br /><br />
     
     <a href="javascript:ShowList()">显示返回Json内容</a>
     <div id="show" style="display:none;">
    <textarea id="jsonshow" rows="20" cols="100"></textarea>
</div>


    </div>
    
    
    <script type="text/javascript">
        function ShowList()
        {
            document.getElementById("show").style.display = "";
        }
    
    </script>

</asp:Content>
