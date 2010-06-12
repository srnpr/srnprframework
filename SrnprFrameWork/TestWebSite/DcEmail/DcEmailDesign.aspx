<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DcEmailDesign.aspx.cs" Inherits="DcEmail_DcEmailDesign" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    邮件描述：这个邮件是给代理商发滴,涉及a模块b模块<br />
    <br />
    你可使用的参数有：<asp:Label ID="lbParmsShow" runat="server"></asp:Label>
    <br />
    <br />
    是否可用：<asp:CheckBox ID="CheckBox1" runat="server" Text="可用" />
    <br />
    <table style="width: 78%; height: 61px;">
        <tr>
            <td>
                条件表达式
            </td>
            <td>
                邮件标题
            </td>
            <td>
                操作
            </td>
        </tr>
        <asp:Repeater ID="rpList" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <%#Eval("RuleExpress")%>
                    </td>
                    <td>
                        <%#Eval("Title")%>
                    </td>
                    <td>
                        <input type="button" onclick="" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <input type="hidden" name="dev_dcemail_submit_type" id="dev_dcemail_submit_type" />
    
    <input type="hidden" name="dev_dcemail_submit_tempguid" id="dev_dcemail_submit_tempguid" />
    <asp:Panel ID="pShow" runat="server">
        <asp:HiddenField ID="hfTempId" runat="server" />
    
        <br />
        条件表达式：<asp:TextBox ID="tbRuleExpress" runat="server" Width="539px"></asp:TextBox>
        <br />
        <br />
        接收人：<asp:TextBox ID="tbToEmail" runat="server"></asp:TextBox>
        <br />
        <br />
        邮件标题：<asp:TextBox ID="tbTitle" runat="server" Width="557px"></asp:TextBox>
        <br />
        <br />
        邮件内容：<br />
        <asp:TextBox ID="tbContent" runat="server" Height="89px" Width="635px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnSave" runat="server" Text="提交" onclick="btnSave_Click" />
    </asp:Panel>
    <div>
    </div>
    </form>
    
    <script type="text/javascript">
        function Dev_Dcemail_Submit(iType,sGuid)
        {
            document.getElementById("dev_dcemail_submit_type").value = iType;
            document.getElementById("dev_dcemail_submit_tempguid").value = sGuid;

            document.getElementById("dev_dcemail_submit_type").form.submit();
        }
    
    
    </script>
    
</body>
</html>
