<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="SrnprSite.Config.Create" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterPage_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPage_Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterPage_Content" runat="server">
    <asp:Button ID="btnCreate" runat="server" onclick="btnCreate_Click" 
    Text="生成配置文件" />
</asp:Content>
