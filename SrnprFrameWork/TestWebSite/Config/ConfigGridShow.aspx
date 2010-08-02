<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigGridShow.aspx.cs" Inherits="Config_ConfigGridShow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <div><%=sMessage %></div>
    
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
                 <tr>
                    <td>
                        描述：
                    </td>
                    <td>
                        <asp:TextBox ID="tbDescription" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <table>
            <tr>
                <th>
                    表头名称
                </th>
                <th>
                    数据列
                </th>
                <th>
                    操作
                </th>
            </tr>
            <asp:Repeater ID="RPDataColumn" runat="server">
                <HeaderTemplate>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%#Eval("HeaderText")%>
                        </td>
                        <td>
                            <%#Eval("ColumnData")%>
                        </td>
                        <td>
                        
                        <a href="javascript:SubmitCheck('d_d','<%#Eval("Guid") %>')">删除</a>
                        
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:Repeater>
            <tr>
                <td>
                    <asp:TextBox ID="TBHeaderText" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TBColumnData" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnAddDataColumn" runat="server" Text="添加" OnClick="btnAddDataColumn_Click" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
            <th>
                    数据字段
                </th>
                
                <th>
                    参数查询类型
                </th>
                <th>
                    参数名称
                </th>
                <th>
                    操作
                </th>
            </tr>
            <asp:Repeater ID="RPParams" runat="server">
                <ItemTemplate>
                    <tr><td>
                            <%#Eval("ColumnField")%>
                        </td>
                       
                        <td>
                            <%#Eval("ParamOperator")%>
                        </td>
                         <td>
                            <%#Eval("ParamName")%>
                        </td>
                        <td>
                        <a href="javascript:SubmitCheck('d_p','<%#Eval("Guid") %>')">删除</a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr><td>
                   <asp:TextBox ID="tbColumnField" runat="server"></asp:TextBox>
                </td>
                
                <td>
                    <asp:DropDownList ID="ddlParamOperator" runat="server">
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="like" Value="like"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                   <asp:TextBox ID="tbParamName" runat="server"></asp:TextBox>
                </td>
                <td>
                
                    <asp:Button ID="btnAddParams" runat="server" onclick="btnAddParams_Click" 
                        Text="添加" />
                
                </td>
            </tr>
        </table>
    </div>
    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存设置" />
    <a href="ConfigGridShowList.aspx">返回列表</a>
    
    <input type="hidden" name="submittype" id="submittype" />
    <input type="hidden" name="submitid" id="submitid" />
    <script type="text/javascript">
        function SubmitCheck(t,guid)
        {
            document.getElementById("submittype").value = t;
            document.getElementById("submitid").value = guid;
            document.getElementById("submitid").form.submit();
        }
    
    </script>
    
    </form>
</body>
</html>
