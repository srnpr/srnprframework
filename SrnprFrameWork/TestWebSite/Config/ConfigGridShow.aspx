<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigGridShow.aspx.cs" Inherits="Config_ConfigGridShow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <table>
                <tr>
                    <td>
                        编号：
                    </td>
                    <td>
                        <asp:TextBox ID="TBId" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        数据库：
                    </td>
                    <td>
                        <asp:TextBox ID="TBDataBaseId" runat="server" Text="SO" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        表名称：
                    </td>
                    <td>
                        <asp:TextBox ID="TBTableName" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <asp:Repeater ID="RPDataColumn" runat="server">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            表头名称
                        </th>
                        <th>
                            数据列
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%#Eval("HeaderText")%>
                    </td>
                    <td>
                        <%#Eval("ColumnData")%>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
