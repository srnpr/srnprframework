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
        <asp:Button ID="btnTest" runat="server" OnClick="btnTest_Click" Text="进行测试" />
        <br />
        <br />
    </div>
    <div>
        <div>
            预计共有<asp:Label ID="lbCount" runat="server"></asp:Label>种邮件发送
        </div>
       
        <div>
            <asp:Repeater ID="rpList" runat="server">
                <ItemTemplate>
                
                <div style="margin-bottom:10px; border:solid 1px #999999; padding:5px">
                
                   <div> 收件人：
                    
                        <%#Eval("ToEmail")%></div>
                   <div>邮件标题：<%#Eval("Title") %></div>
                   <div> 邮件内容：<%#Eval("Content") %></div>
                   
                   </div>
                </ItemTemplate>
               
                
            </asp:Repeater>
        </div>
    </div>
    </form>

    

</body>
</html>
