<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Excel.aspx.cs" Inherits="SrnprSite.Web.GridShow.Excel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <input type="hidden" name="json" id="json" />
    
        <asp:Button ID="btnExcel" runat="server" onclick="btnExcel_Click" 
            Text="导出到Excel" />
    
    </div>
    </form>
</body>
</html>
