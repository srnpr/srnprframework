<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterDialog.Master" AutoEventWireup="true" CodeBehind="DialogFirst.aspx.cs" Inherits="SrnprSite.Demo.Dialog.DialogMember" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<br /><br />

<input type="button" value="打开二号对话框" onclick="SWW.W.Dialog.Open({url:'/Demo/Dialog/DialogTwo.aspx',title:'二号窗口',width:900,height:450})" />

<br />
a1字段：<input type="text" id="a1" />
<br />
b1字段：<input type="text" id="b1" />
<br /><br />
<input type="text" id="sa" />
<input type="button" value="设置a字段值" onclick="SWW.W.Dialog.SetValue('a',SWW.F.DOM.Value('sa'))" />



</asp:Content>
