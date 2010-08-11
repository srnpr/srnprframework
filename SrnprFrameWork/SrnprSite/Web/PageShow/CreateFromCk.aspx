<%@ Page Title="" Language="C#" ValidateRequest="false"  MasterPageFile="~/Master/MasterPage.Master" AutoEventWireup="true" CodeBehind="CreateFromCk.aspx.cs" Inherits="SrnprSite.Web.PageShow.CreateFromCk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterPage_Head" runat="server">
<script type="text/javascript" src="/srnpr_ckeditor/ckeditor.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPage_Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterPage_Content" runat="server">
<div>
  
		
		
				
				
				<asp:TextBox ID="tbEditor" runat="server" TextMode="MultiLine"  cols="80"  rows="10"></asp:TextBox>
				
			
			<script type="text/javascript">
			    //<![CDATA[

			    var editor = CKEDITOR.replace('<%=tbEditor.ClientID %>');


			    //]]>
			</script>
		
			<asp:Button ID="btnOk" runat="server" onclick="btnOk_Click" Text="提交" />
			
			<br />
			<br />
			<a href="Test.aspx?demo=demo&id=<%=sId %>" target="_blank"> 查看静态demo</a>
			<a href="Test.aspx?id=<%=sId %>" target="_blank"> 查看带实际数据版</a>
		
    </div>
</asp:Content>
