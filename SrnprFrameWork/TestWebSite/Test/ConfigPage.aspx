<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigPage.aspx.cs" Inherits="Test_ConfigPage" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   <script type="text/javascript" src="/srnpr_ckeditor/ckeditor.js"></script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
  
		<p>
			<label for="tbEditor">
				Editor 1:</label><br />
				
				
				<asp:TextBox ID="tbEditor" runat="server" TextMode="MultiLine"  cols="80"  rows="10"></asp:TextBox>
				
			
			<script type="text/javascript">
			    //<![CDATA[

			    var editor = CKEDITOR.replace('<%=tbEditor.ClientID %>');




			    //]]>
			</script>
		</p>
        <p>
			<asp:Button ID="btnOk" runat="server" onclick="btnOk_Click" Text="提交" />
			
			<br />
			<br />
			
			<a href="../Widget/PageShow.aspx" target="_blank"> 测试查看</a>
		</p>
    </div>
    </form>
</body>
</html>
