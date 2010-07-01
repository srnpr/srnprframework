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
                    文件名称：
                </td>
                <td>
                    <asp:TextBox ID="tbXmlId" runat="server"></asp:TextBox>
                </td>
            </tr>
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
                    <asp:DropDownList ID="ddlVersion" runat="server">
                        <asp:ListItem Text="2.0.0.0" Value="2.0.0.0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    状态Sql：
                </td>
                <td>
                   <asp:TextBox ID="tbStateSql" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div>
            <div>
                参数列表：<asp:LinkButton ID="lbParmAdd" runat="server" CommandName="add_parm" OnClick="lbAdd_Click">添加</asp:LinkButton>
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
                        <th>
                            操作
                        </th>
                    </tr>
                    <asp:Repeater ID="rpParmItem" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%#Eval("ParmName") %>
                                </td>
                                <td>
                                    <%#Eval("ParmText") %>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lbParmChange" runat="server" CommandName="upd_parm" CommandArgument=' <%#Eval("Guid") %>'
                                        OnClick="lbChange_Click">修改</asp:LinkButton>
                                    <asp:LinkButton ID="lbParmDel" runat="server" CommandName="del_parm" CommandArgument=' <%#Eval("Guid") %>'
                                        OnClick="lbChange_Click">删除</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <asp:Panel ID="pParmAdd" runat="server">
                <asp:HiddenField ID="hfParmId" runat="server" />
                参数名称：<asp:TextBox ID="tbParmName" runat="server"></asp:TextBox>
                参数中文描述：<asp:TextBox ID="tbParmDescriptioon" runat="server"></asp:TextBox>
                <asp:Button ID="btnParm" runat="server" Text="确认操作" OnClick="btnParm_Click" />
                &nbsp;
                <asp:Button ID="btnParmCancel" runat="server" Text="取消" CommandName="cancel_parm"
                    OnClick="btnCancel_Click" />
            </asp:Panel>
        </div>
        <div>
            <div>
                主Sql：<asp:LinkButton ID="lbMainsqlAdd" runat="server" CommandName="add_mainsql" OnClick="lbAdd_Click">添加</asp:LinkButton>
            </div>
            <div>
                <table>
                    <tr>
                        <th>
                            参数名称
                        </th>
                        <th>
                            操作
                        </th>
                    </tr>
                    <asp:Repeater ID="rpMainItem" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%#Eval("SqlString") %>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lbMainsqlChange" runat="server" CommandName="upd_mainsql" CommandArgument=' <%#Eval("Guid") %>'
                                        OnClick="lbChange_Click">修改</asp:LinkButton>
                                    <asp:LinkButton ID="lbMainsqlDel" runat="server" CommandName="del_mainsql" CommandArgument=' <%#Eval("Guid") %>'
                                        OnClick="lbChange_Click">删除</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <asp:Panel ID="pMainsql" runat="server">
                <asp:HiddenField ID="hfMainsqlId" runat="server" />
                主sql：<asp:TextBox ID="tbMainsql" runat="server"></asp:TextBox>
                <asp:Button ID="btnMainsql" runat="server" Text="确认操作" OnClick="btnMainsql_Click" />
                &nbsp;
                <asp:Button ID="Button2" runat="server" Text="取消" CommandName="cancel_mainsql" OnClick="btnCancel_Click" />
            </asp:Panel>
        </div>
        <div>
            <div>
                循环Sql：<asp:LinkButton ID="lbListsqlAdd" runat="server" CommandName="add_listsql"
                    OnClick="lbAdd_Click">添加</asp:LinkButton>
            </div>
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
                                <%#Eval("SqlString")%>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbListsqlChange" runat="server" CommandName="upd_listsql" CommandArgument=' <%#Eval("Guid") %>'
                                    OnClick="lbChange_Click">修改</asp:LinkButton>
                                <asp:LinkButton ID="lbListsqlDel" runat="server" CommandName="del_listsql" CommandArgument=' <%#Eval("Guid") %>'
                                    OnClick="lbChange_Click">删除</asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <asp:Panel ID="pListSql" runat="server">
                <asp:HiddenField ID="hfListsqlId" runat="server" />
                主sql：<asp:TextBox ID="tbListsql" runat="server"></asp:TextBox>
                <asp:Button ID="btnListsql" runat="server" Text="确认操作" OnClick="btnListsql_Click" />
                &nbsp;
                <asp:Button ID="btnListCancel" runat="server" Text="取消" CommandName="cancel_listsql"
                    OnClick="btnCancel_Click" />
            </asp:Panel>
        </div>
    </div>
    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="确认修改" />
    </form>
</body>
</html>
