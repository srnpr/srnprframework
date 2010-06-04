<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DcEmailDemo.aspx.cs" Inherits="DcEmail_DcEmailDemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    邮件描述：这个邮件是给代理商发滴,涉及a模块b模块<br />
    <br />
    你可使用的参数有：{$订单编号}&nbsp;&nbsp; {$订单金额}&nbsp;&nbsp; {$代理商名称}<br />
    <br />
    是否可用：<asp:CheckBox ID="CheckBox1" runat="server" Text="可用" />
    <br />
    <table style="width: 78%; height: 61px;">
        <tr>
            <td>
                条件表达式</td>
            <td>
                邮件标题</td>
            <td>
                操作</td>
        </tr>
        <tr>
            <td>
                {$订单金额}=1000</td>
            <td>
                {$代理商名称}您好，你下了订单。</td>
            <td>
                <asp:Button ID="Button2" runat="server" Text="修改" />
                <asp:Button ID="Button3" runat="server" Text="删除" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <br />
    条件表达式：<asp:TextBox ID="TextBox3" runat="server" Width="539px">{$订单金额}=1000</asp:TextBox>
    <br />
    <br />
    接收人：<asp:TextBox ID="TextBox4" runat="server">{$创建人}{$特殊接收人}</asp:TextBox>
    <br />
    <br />
    邮件标题：<asp:TextBox ID="TextBox2" runat="server" Width="557px">{$代理商名称}您好，你下了订单。</asp:TextBox>
    <br />
    <br />
    邮件内容：<br />
    <asp:TextBox ID="TextBox1" runat="server" Height="89px" Width="635px">{$代理商名称}您好，你下的订单{$订单编号}将于什么时候送到。</asp:TextBox>
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="提交" />
    <div>
    
    </div>
    </form>
</body>
</html>
