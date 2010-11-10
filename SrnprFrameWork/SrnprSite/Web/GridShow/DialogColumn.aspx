<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterDialog.Master" AutoEventWireup="true" CodeBehind="DialogColumn.aspx.cs" Inherits="SrnprSite.Web.GridShow.DialogColumn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<table>

<tr><td>表头名称</td>
                <td>
                    <asp:TextBox ID="TBHeaderText" runat="server"></asp:TextBox>
                </td>
                </tr>
                <tr>
                <td>数据列</td>
                <td>
                    <asp:TextBox ID="TBColumnData" runat="server"></asp:TextBox>
                </td><tr><td>显示类型</td>
                 <td>
                    <asp:DropDownList ID="ddlColumnType" runat="server">
                    
                    </asp:DropDownList>
                </td><tr><td>显示内容</td>
                <td>
                    <asp:TextBox ID="tbColumnShow" runat="server"></asp:TextBox>
                </td><tr><td>显示方式</td>
                <td>
                    <asp:DropDownList ID="ddlShowDisplay" runat="server">
                    
                    </asp:DropDownList>
                </td><tr><td>排序方式</td>
                <td>
                    <asp:DropDownList ID="ddlOrderType" runat="server">
                   
                    </asp:DropDownList>
                </td><tr><td>宽度</td>
                 <td>
                    <asp:TextBox ID="tbWidth" runat="server"></asp:TextBox>
                </td>
               
            </tr>

            </table>


    <br />
    <asp:Button ID="bSave" runat="server" onclick="bSave_Click" Text="保存" />


    <script type="text/javascript">

        

        var strFlag = "<%=strFlag %>";
        if (strFlag == "ok")
        {
            SWW.W.Dialog.Source().SaveSubmit();
            SWW.W.Dialog.Close();
        }

    
    </script>

</asp:Content>
