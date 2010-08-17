<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.Master" AutoEventWireup="true" CodeBehind="CreatePage.aspx.cs" Inherits="SrnprSite.Web.PageShow.CreatePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterPage_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPage_Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterPage_Content" runat="server">

    请选择模板类型：

<ul>


<li><a href="PageConfig.aspx?temp=query">查询页面</a></li>
<li><a href="PageConfig.aspx?temp=add">创建页面</a></li>

</ul>




</asp:Content>
