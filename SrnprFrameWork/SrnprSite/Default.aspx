<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/MasterPage.Master" CodeBehind="Default.aspx.cs" Inherits="SrnprSite._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MasterPage_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPage_Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterPage_Content" runat="server">



<script>


    var t = {};
    t.MyString = "aa";
    t.Guid = "ff";
    t.WidgetType = "LS";


    SWW.I("LS", t);
    //SWW.I("LS", { a: 'dddd' });
</script>



</asp:Content>
