<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DcEmailList.aspx.cs" Inherits="DcEmail_DcEmailList" %>

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
            
            <a href="DcEmailCode.aspx">新建</a>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        
        <table cellpadding="1" cellspacing="1" border="1">
       
       <tr>
       <th>编号</th>
       <th>邮件主描述</th>
       <th>邮件辅助描述</th>
       <th>操作</th>
       </tr>
        <asp:Repeater ID="rpList" runat="server">       
        <ItemTemplate>
         <tr>
        <td><%#Eval("Id")%></td>
        <td><%#Eval("Title")%></td>
        <td><%#Eval("Description")%></td>
        <td><a href="DcEmailCode.aspx?id=<%#Eval("ID") %>" target="_blank">修改Code</a>
        <a href="DcEmailDesign.aspx?id=<%#Eval("ID") %>" target="_blank">修改Design</a>
        <a href="DcEmailTest.aspx?id=<%#Eval("ID") %>" target="_blank">测试</a></td>
        </tr>
        
        
        
        </ItemTemplate>
        </asp:Repeater>
         </table>
        
        <br />
    
    </div>
    </form>
</body>
</html>
