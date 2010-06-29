<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DcEmailCode.aspx.cs" Inherits="DcEmail_DcEmailCode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    标题：
                </td>
                <td>
                    <asp:TextBox ID="tbTitle" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    描述信息：
                </td>
                <td>
                    <asp:TextBox ID="tbDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    数据库链接：
                </td>
                <td>
                    <asp:DropDownList ID="ddlDataBase" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    邮件服务器：
                </td>
                <td>
                    <asp:DropDownList ID="ddlServerEmail" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    版本标识：
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem Text="2.0.0.0" Value="2.0.0.0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div>
            <table>
                <tr>
                    <th>
                        参数名称
                    </th>
                    <th>
                        参数描述
                    </th>
                </tr>
                <asp:Repeater ID="rpParmItem" runat="server" 
                    >
                    <ItemTemplate>
                        <tr>
                            <td>
                            <%#Eval("ParmName") %>
                            </td>
                            <td>
                            <asp:Button ID="btnChange" runat="server" CommandName="ParmName" CommandArgument=' <%#Eval("ParmName") %>' OnClick="btnParmChange_Click"/>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div>
            <table>
                <tr>
                    <th>
                        参数名称
                    </th>
                    <th>
                        参数描述
                    </th>
                </tr>
                <asp:Repeater ID="rpMainItem" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div>
            <table>
                <tr>
                    <th>
                        参数名称
                    </th>
                    <th>
                        参数描述
                    </th>
                </tr>
                <asp:Repeater ID="rpListItem" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
    
    
    
    <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="确认修改" />
    
    
    
    </form>
</body>
</html>
