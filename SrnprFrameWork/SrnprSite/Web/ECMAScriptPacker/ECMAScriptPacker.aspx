<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.Master" AutoEventWireup="true" CodeBehind="ECMAScriptPacker.aspx.cs" Inherits="SrnprSite.Web.ECMAScriptPacker.ECMAScriptPacker" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterPage_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPage_Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterPage_Content" runat="server">
    <p>
        <asp:DropDownList ID="DDLType" runat="server">
        </asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 提示消息： 
        <asp:Label ID="lbMessage" runat="server"></asp:Label>
    </p>
    <p>
        基本路径：<asp:TextBox ID="tbBaseDir" runat="server" Width="513px">S:\AAAProject\SrnprFrameWork\SrnprFrameWork\SrnprFile\WebFile\</asp:TextBox>
    </p>
    <p>
        <asp:TextBox ID="tbSourceJS" runat="server" Height="106px" TextMode="MultiLine" 
            Width="383px">SrnprWebJsWebWidgetFWF.js,SrnprWebJsToolDialogFWF.js,SrnprWebJsListShowFWF.js,SrnprWebJsGridShowFWF.js
</asp:TextBox>
&nbsp;
    </p>
    <p>
&nbsp;&nbsp;
        <asp:TextBox ID="tbSaveJSSource" runat="server">sww_source.js</asp:TextBox>
&nbsp;<asp:TextBox ID="tbSaveJSMin" runat="server">SWW-1.0.0.0.min.js</asp:TextBox>
        <asp:Button ID="btnChangeJS" runat="server" onclick="btnChangeJS_Click" 
            Text="btnChangeJS" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
    </p>
    <p>
        <asp:TextBox ID="tbSourceCss" runat="server" Height="71px" TextMode="MultiLine" 
            Width="380px">SrnprWebCssGridShowFWF.css,SrnprWebCssPageShowFWF.css,SrnprWebCssWebWidgetFWF.css</asp:TextBox>
&nbsp;</p>
    <p>
        <asp:TextBox ID="tbSaveCSSSource" runat="server" Text="sww_css_source.css"></asp:TextBox>
        <asp:TextBox ID="tbSaveCSSMin" runat="server" Text="SWW-CSS-1.0.0.0.min.css"></asp:TextBox>
        <asp:Button ID="btnChangeCSS" runat="server" onclick="btnChangeCSS_Click" 
            Text="btnChangeCSS" />
    </p>
    <p>
        &nbsp;</p>
    <p>
    </p>
</asp:Content>
