<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigGridShowList.aspx.cs" Inherits="Config_ConfigGridShowList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <a href="ConfigGridShow.aspx">新建</a>
    
    <div>
    <table>
    <tr>
    <th>编号</th>
    <th>描述</th>
    <th>操作</th>
    </tr>
   
    <asp:Repeater ID="rpList" runat="server">
    <ItemTemplate>
    <tr><td><%#Eval("Id") %></td><td><%#Eval("Description") %></td><td><a href="ConfigGridShow.aspx?id=<%#Eval("Id") %>">修改</a></td></tr>
    
    </ItemTemplate>
    
    </asp:Repeater>
     </table>
     </div>
    </div>
    </form>
</body>
</html>
