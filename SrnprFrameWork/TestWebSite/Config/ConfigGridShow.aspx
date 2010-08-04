<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigGridShow.aspx.cs" Inherits="Config_ConfigGridShow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/WebFile/SrnprWebCSSGridShowFWF.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <div><%=sMessage %></div>
    
        <div class="SWCGSF_DIV_MAIN">
            <table cellpadding="0" cellspacing="1">
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
        
        
        <div class="SWCGSF_DIV_MAIN">
            <table cellpadding="0" cellspacing="1">
            <tr>
                <th>
                    表头名称
                </th>
                <th>
                    数据列
                </th><th>
                    数据显示类型
                </th>
                <th>
                    显示内容
                </th>
                <th>
                    显示方式
                </th>
                <th>
                    排序方式
                </th>
                <th>
                    宽度
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
                        
                        <%#GetTextByDDL(ddlColumnType,Eval("ColumnType"))%>
                        </td>
                        <td>
                        <%#Eval("ColumnShow")%>
                        </td>
                         <td>
                        
                        <%#GetTextByDDL(ddlShowDisplay, Eval("ShowDisplay"))%>
                        </td>
                        <td>
                        
                        <%#GetTextByDDL(ddlOrderType, Eval("OrderType"))%>
                        </td>
                        <td>
                        <%#Eval("Width")%>
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
                    <asp:DropDownList ID="ddlColumnType" runat="server">
                    <asp:ListItem Text="默认" Value="d"></asp:ListItem>
                    <asp:ListItem Text="单选框" Value="r"></asp:ListItem>
                    <asp:ListItem Text="复选框" Value="c"></asp:ListItem>
                    <asp:ListItem Text="超级链接" Value="l"></asp:ListItem>
                    <asp:ListItem Text="其他" Value="o"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="tbColumnShow" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList ID="ddlShowDisplay" runat="server">
                    <asp:ListItem Text="默认" Value="d"></asp:ListItem>
                    <asp:ListItem Text="不显示" Value="n"></asp:ListItem>
                    <asp:ListItem Text="永久隐藏" Value="h"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="ddlOrderType" runat="server">
                    <asp:ListItem Text="默认" Value="d"></asp:ListItem>
                    <asp:ListItem Text="默认正序" Value="a"></asp:ListItem>
                    <asp:ListItem Text="默认倒序" Value="e"></asp:ListItem>
                    <asp:ListItem Text="不排序" Value="n"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                 <td>
                    <asp:TextBox ID="tbWidth" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnAddDataColumn" runat="server" Text="添加" OnClick="btnAddDataColumn_Click" />
                </td>
            </tr>
        </table>
        </div>
        
        <br /><br /><br />以下为输入参数集合，暂未启用
        <div class="SWCGSF_DIV_MAIN">
            <table cellpadding="0" cellspacing="1">
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
