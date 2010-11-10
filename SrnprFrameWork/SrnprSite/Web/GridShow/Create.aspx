<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="SrnprSite.Web.GridShow.Create" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterPage_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPage_Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterPage_Content" runat="server">
<div>
    <asp:HiddenField ID="hfGuid" runat="server" />
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
                        分组名称：
                    </td>
                    <td>
                        <asp:TextBox ID="tbGroupColumn" runat="server"></asp:TextBox>(可为空，格式为：[group],[field],[sum])
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
         <br />
        <div>
        <input type="button" value="添加字段" onclick="ChangeColumn()" />
        </div>
        <div class="srdmaintable">
            <table cellpadding="0" cellspacing="1">
            <tr>
                <th>
                    表头名称
                </th>
                <th>
                    数据列
                </th><th width="60px">
                    显示类型
                </th>
                <th width="60px">
                    显示内容
                </th>
                <th width="80px">
                    显示方式
                </th>
                <th width="60px">
                    排序方式
                </th>
                <th width="60px">
                    宽度
                </th>
                <th width="60px">
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
                        
                        <%# SrnprWeb.WebEntity.GridShowColumnDictWWE.ColumnType[Eval("ColumnType").ToString().Trim()]  %>
                        </td>
                        <td>
                        <%#Eval("ColumnShow")%>
                        </td>
                         <td>
                        
                        <%#SrnprWeb.WebEntity.GridShowColumnDictWWE.ShowDisplay[Eval("ShowDisplay").ToString().Trim()]%>
                        </td>
                        <td>
                        
                        <%#SrnprWeb.WebEntity.GridShowColumnDictWWE.OrderType[Eval("OrderType").ToString().Trim()]%>
                        </td>
                        <td>
                        <%#Eval("Width")%>
                        </td>
                        <td>
                        <a href="javascript:ChangeColumn('<%#Eval("Guid") %>')">修改</a>
                        <a href="javascript:SubmitCheck('d_d','<%#Eval("Guid") %>')">删除</a>
                        
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:Repeater>
            
        </table>
        </div>
        <br />
      
        <div class="srdmaintable">
            <table cellpadding="0" cellspacing="1">
            <tr>
            <th>
                    连接操作符
                </th>
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
                    <tr>
                    
                    <td>
                            <%#GetTextByDDL(ddlParamQueryType, Eval("ParamQueryType"))%>
                        </td>
                    <td>
                            <%#Eval("ColumnField")%>
                        </td>
                       
                        <td>
                            <%#GetTextByDDL(ddlParamOperator, Eval("ParamOperator"))%>
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
                    <asp:DropDownList ID="ddlParamQueryType" runat="server">
                    <asp:ListItem Value="a" Text="并且"></asp:ListItem>
                    <asp:ListItem Value="o" Text="或者"></asp:ListItem>
                    <asp:ListItem Value="d" Text="自定义"></asp:ListItem>
                    </asp:DropDownList>
                </td><td>
                   <asp:TextBox ID="tbColumnField" runat="server"></asp:TextBox>
                </td>
                
                <td>
                    <asp:DropDownList ID="ddlParamOperator" runat="server">
                    <asp:ListItem Value="e" Text="等于"></asp:ListItem>
                    <asp:ListItem Value="b" Text="大于"></asp:ListItem>
                    <asp:ListItem Value="s" Text="小于"></asp:ListItem>
                    <asp:ListItem Value="l" Text="搜索"></asp:ListItem>
                    <asp:ListItem Value="d" Text="自定义"></asp:ListItem>
                    
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
    <br /><br />
    <a href="List.aspx">返回列表</a>&nbsp;&nbsp;&nbsp;&nbsp;<a target="_blank" href="Test.aspx">测试</a>
    
    <input type="hidden" name="submittype" id="submittype" />
    <input type="hidden" name="submitid" id="submitid" />
    <script type="text/javascript">
        function SubmitCheck(t,guid)
        {
            document.getElementById("submittype").value = t;
            document.getElementById("submitid").value = guid;
            document.getElementById("submitid").form.submit();
        }

        function ChangeColumn(guid)
        {
           

            var t = document.getElementById('<%=hfGuid.ClientID %>').value;


            SWW.W.Dialog.Open({url:'DialogColumn.aspx?t='+t+'&c='+guid,title:(guid?'修改字段':'添加字段')});
        }

        function SaveSubmit()
        {
            document.getElementById("<%=btnSave.ClientID %>").click();
        }

    
    </script>
</asp:Content>
