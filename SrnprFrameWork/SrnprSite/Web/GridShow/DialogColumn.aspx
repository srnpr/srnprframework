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
                <td>
                    <asp:TextBox ID="TBColumnData" runat="server"></asp:TextBox>
                </td><tr>
                 <td>
                    <asp:DropDownList ID="ddlColumnType" runat="server">
                    
                    </asp:DropDownList>
                </td><tr>
                <td>
                    <asp:TextBox ID="tbColumnShow" runat="server"></asp:TextBox>
                </td><tr>
                <td>
                    <asp:DropDownList ID="ddlShowDisplay" runat="server">
                    
                    </asp:DropDownList>
                </td><tr>
                <td>
                    <asp:DropDownList ID="ddlOrderType" runat="server">
                   
                    </asp:DropDownList>
                </td><tr>
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
            SWW.W.Dialog.Close();
        }

    
    </script>

</asp:Content>
