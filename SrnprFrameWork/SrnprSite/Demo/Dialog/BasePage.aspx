﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.Master" AutoEventWireup="true" CodeBehind="BasePage.aspx.cs" Inherits="SrnprSite.Demo.Dialog.BasePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterPage_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPage_Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterPage_Content" runat="server">





<span id="aa_span_662babb2-7390-40cd-a453-a86731e35edb" ></span>
<script>

    SWW.I({ WidgetType: "TD", Id: "aa", url: "/Demo/Dialog/DialogFirst.aspx", SId: "aa_span_662babb2-7390-40cd-a453-a86731e35edb" });
</script>

<br /><br />

<input type="button" value="打开一号对话框" onclick="SWW.W.Dialog.Open({url:'/Demo/Dialog/DialogFirst.aspx?sww_td_parent_id=aa',title:'一号对话框',width:900,height:450,top:200})" />

</asp:Content>
