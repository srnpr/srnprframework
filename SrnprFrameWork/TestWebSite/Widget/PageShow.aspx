<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PageShow.aspx.cs" Inherits="Widget_PageShow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script type="text/javascript" src="/WebFile/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="/WebFile/json2.js"></script>
    <script type="text/javascript" src="/WebFile/SrnprWebJsGridShowFWF.js"></script>
    
    <script type="text/javascript" src="http://f.xgou.com/AtGang/Common/js/SrnprNetJsAllAlpha.js"></script>
    <link href="/WebFile/SrnprWebCSSGridShowFWF.css" type="text/css" rel="Stylesheet" />
    <link href="http://f.xgou.com/AtGang/Manage/css/ManageAlpha.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <sw:PageShowWWW ID="PageShow1" runat="server" XmlConfigName="tttt" />
    </div>
    </form>
</body>
</html>
