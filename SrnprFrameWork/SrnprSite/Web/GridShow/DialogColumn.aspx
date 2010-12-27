<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="DialogColumn.aspx.cs" Inherits="SrnprSite.Web.GridShow.DialogColumn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>
                表头名称
            </td>
            <td>
                <asp:TextBox ID="TBHeaderText" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                数据列
            </td>
            <td>
                <asp:TextBox ID="TBColumnData" runat="server" Height="103px" TextMode="MultiLine"
                    Width="287px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                显示类型
            </td>
            <td>
                <asp:DropDownList ID="ddlColumnType" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                显示内容
            </td>
            <td>
                <asp:TextBox ID="tbColumnShow" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                显示方式
            </td>
            <td>
                <asp:DropDownList ID="ddlShowDisplay" runat="server">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>
                导出字段
            </td>
            <td>
                <asp:DropDownList ID="ddlExcelType" runat="server">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>
                排序方式
            </td>
            <td>
                <asp:DropDownList ID="ddlOrderType" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                宽度
            </td>
            <td>
                <asp:TextBox ID="tbStyle_Width" runat="server"></asp:TextBox>
                px
            </td>
        </tr>
         <tr>
            <td>
               对齐方式
            </td>
            <td>
                <asp:DropDownList ID="ddlStyle_TextAlign" runat="server">
               
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <br />
    <asp:Button ID="bSave" runat="server" OnClick="bSave_Click" Text="保存" />
    <script type="text/javascript">



        var strFlag = "<%=strFlag %>";
        if (strFlag == "ok")
        {
            SWW.W.Dialog.Source().SaveSubmit();
            SWW.W.Dialog.Close();
        }

    
    </script>
</asp:Content>
