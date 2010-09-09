<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/MasterPage.Master" CodeBehind="Default.aspx.cs" Inherits="SrnprSite._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MasterPage_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPage_Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterPage_Content" runat="server">

<script type="text/javascript">
  

</script>



<input type="button" onclick="SWW.W.Dialog.Open({height:500,width:400,url:'default.aspx',id:'aa'});" value="11111" />
<div style="height:700px;"><input type="text" id="testtext" />

<input type="button" value="Get" onclick="alert(SWW.W.Dialog.GetValue('testtext'))" />

<input type="button" value="Set" onclick="SWW.W.Dialog.SetValue('testtext',SWW.F.DOM.Value('testtext'))" />
</div>

<input type="button" onclick="SWW.W.Dialog.Open({});" value="11111" />
</asp:Content>
