﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DcEmailList.aspx.cs" Inherits="DcEmail_DcEmailList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="重新检测所有文件" />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        
        <table>
       
       
        <asp:Repeater ID="rpList" runat="server">       
        <ItemTemplate>
         <tr>
        <td><%#Eval("Id")%></td>
        <td><%#Eval("Title")%></td>
        <td><%#Eval("Description")%></td>
        </tr>
        
        
        
        </ItemTemplate>
        </asp:Repeater>
         </table>
        
        <br />
    
    </div>
    </form>
</body>
</html>
