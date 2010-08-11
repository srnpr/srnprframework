<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="PageConfig.aspx.cs" Inherits="SrnprSite.Web.PageShow.PageConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MasterPage_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPage_Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterPage_Content" runat="server">
    <div class="srdmaintable">
        <table>
            <tr>
                <td class="srtmaintdl">
                    页面编号
                </td>
                <td class="srtmaintdr">
                    <asp:TextBox ID="tbXmlId" runat="server"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td class="srtmaintdl">
                    页面描述
                </td>
                <td class="srtmaintdr">
                    <asp:TextBox ID="tbDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </table>
        <span class="srsmainbutton srsmainbuttonnext ">
            <asp:Button ID="btnNext" runat="server" Text="确认提交" 
            onclick="btnNext_Click" /></span>
    </div>
</asp:Content>
