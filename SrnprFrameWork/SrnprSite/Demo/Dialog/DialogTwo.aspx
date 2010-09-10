<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterDialog.Master" AutoEventWireup="true" CodeBehind="DialogTwo.aspx.cs" Inherits="SrnprSite.Demo.Dialog.DialogTwo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="/WebFile/SrnprWebCSSGridShowFWF.css" type="text/css" rel="Stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div style="width:90%;">
<input type="text" id="sa1" />
<input type="button" value="设置a1字段值" onclick="SWW.W.Dialog.SetValue('a1',SWW.F.DOM.Value('sa1'))" />


<input type="text" id="sb1" />
<input type="button" value="设置b1字段值" onclick="SWW.W.Dialog.SetValue('b1',SWW.F.DOM.Value('sb1'))" />


<sw:GridShowWWW runat="server" ID="aa" XmlConfigName="dff" />



<script>

    SWW.F.DOM.Value('sa1', SWW.W.Dialog.GetValue('a1'));
    SWW.F.DOM.Value('sb1', SWW.W.Dialog.GetValue('b1'));


   
</script>

</div>
</asp:Content>
