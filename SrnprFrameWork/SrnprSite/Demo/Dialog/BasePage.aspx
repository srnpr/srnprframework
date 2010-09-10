<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.Master" AutoEventWireup="true" CodeBehind="BasePage.aspx.cs" Inherits="SrnprSite.Demo.Dialog.BasePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterPage_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPage_Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterPage_Content" runat="server">




<input type="button" value="打开一号对话框" onclick="SWW.W.Dialog.Open({url:'/Demo/Dialog/DialogFirst.aspx',title:'一号窗口'})" />


<br /><br />
a字段：<input type="text" id="a" />


</asp:Content>
