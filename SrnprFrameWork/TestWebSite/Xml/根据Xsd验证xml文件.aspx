<%@ Page Language="C#" AutoEventWireup="true" CodeFile="根据Xsd验证xml文件.aspx.cs" Inherits="Xml_根据Xsd验证xml文件" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="TBXml" runat="server" Width="567px">S:\AAAProject\SrnprFrameWork\SrnprFile\ConfigFile\SrnprWebConfigCF.xml</asp:TextBox>
        <br />
        <asp:TextBox ID="TBXsd" runat="server" Width="572px">S:\AAAProject\SrnprFrameWork\SrnprFile\ConfigFile\SrnprWebConfigXsdCF.xsd</asp:TextBox>
        <br />
        <asp:Button ID="BOk" runat="server" onclick="BOk_Click" Text="Button" />
        <br />
        <br />
        <br />
        <asp:Label ID="LMessage" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
    
    </div>
    </form>
</body>
</html>
