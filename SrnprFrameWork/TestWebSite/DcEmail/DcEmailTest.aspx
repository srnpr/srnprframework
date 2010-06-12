<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DcEmailTest.aspx.cs" Inherits="DcEmail_DcEmailTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
        输入参数集合：
        <asp:Label ID="lbInput" runat="server"></asp:Label>
        <br />
        <asp:Button ID="btnTest" runat="server" onclick="btnTest_Click" Text="进行测试" />
        <br />
        <br />
    
    </div>
    </form>
</body>
</html>
