<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="SrnprSite.Web.PageShow.List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterPage_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPage_Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterPage_Content" runat="server">

   
    
     <div class="srdmaintable">
            <table cellpadding="0" cellspacing="1">
    <tr>
    <th>编号</th>
    <th>描述</th>
    <th>操作</th>
    </tr>
   
    <asp:Repeater ID="rpList" runat="server">
    <ItemTemplate>
    <tr><td><%#Eval("Id") %></td><td><%#Eval("Description") %></td><td><a href="Create.aspx?id=<%#Eval("Id") %>">修改</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href="Test.aspx?id=<%#Eval("Id") %>">测试</a></td></tr>
    
    </ItemTemplate>
    
    </asp:Repeater>
     </table>
     </div>
    </div>
</asp:Content>
